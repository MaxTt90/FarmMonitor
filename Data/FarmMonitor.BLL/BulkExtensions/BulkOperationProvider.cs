using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BulkExtensions
{
  internal class BulkOperationProvider
  {
    private DbContext _context;
    private string _connectionString;

    private string _updateSql="";
    private string _tempTable = string.Format("#temp_{0}", Guid.NewGuid()).Replace("-", "_");

    public BulkOperationProvider(DbContext context)
    {
      if (context == null)
        throw new ArgumentNullException("context");

      _context = context;

      ConnectionStringSettings cs = ConfigurationManager.ConnectionStrings["DefaultConnection"];
      _connectionString = cs.ConnectionString;
    }

    public void Insert<T>(IEnumerable<T> entities, int batchSize)
    {
      using (var dbConnection = new SqlConnection(_connectionString))
      {
        dbConnection.Open();

        using (var transaction = dbConnection.BeginTransaction())
        {
          try
          {
            Insert(entities, transaction, SqlBulkCopyOptions.Default, batchSize);
            transaction.Commit();
          }
          catch (Exception)
          {
            if (transaction.Connection != null)
            {
              transaction.Rollback();
            }
            throw;
          }
        }
      }
    }

    private void Insert<T>(IEnumerable<T> entities, SqlTransaction transaction, SqlBulkCopyOptions options, int batchSize)
    {
      TableMapping tableMapping = DbMapper.GetDbMapping(_context)[typeof(T)];
      using (DataTable dataTable = CreateDataTable(tableMapping, entities))
      {
        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, options, transaction))
        {
          sqlBulkCopy.BatchSize = batchSize;
          sqlBulkCopy.DestinationTableName = dataTable.TableName;
          sqlBulkCopy.WriteToServer(dataTable);
        }
      }
    }

    public void Update<T>(IEnumerable<T> entities, int batchSize)
    {
        using (var dbConnection = new SqlConnection(_connectionString))
        {
            dbConnection.Open();

            using (var transaction = dbConnection.BeginTransaction())
            {
                try
                {
                    Update(entities, transaction, SqlBulkCopyOptions.Default, batchSize);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }
                    throw;
                }
            }
        }
    }

    public void Update<T>(IEnumerable<T> entities, SqlTransaction transaction, SqlBulkCopyOptions options, int batchSize)
    {
        TableMapping tableMapping = DbMapper.GetDbMapping(_context)[typeof(T)];
        using (DataTable dataTable = CreateDataTable(tableMapping, entities, false))
        {
            var taleName = dataTable.TableName;
            string GetTempTable = string.Format("select top 0 * into {0} from {1}", _tempTable, taleName);

            var command = transaction.Connection.CreateCommand();
            command.CommandText = GetTempTable;
            command.Transaction = transaction;
            command.ExecuteNonQuery();

            string sql=string.Format(" update {0} set ",taleName);
            sql=sql+_updateSql;

            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(transaction.Connection, SqlBulkCopyOptions.KeepIdentity, transaction))
            {
                sqlBulkCopy.BatchSize = batchSize;
                sqlBulkCopy.DestinationTableName = _tempTable;
                sqlBulkCopy.WriteToServer(dataTable);
            }

            command.CommandText=sql;
            command.ExecuteNonQuery();
        }
          
    }

    ///// <summary> 
    ///// 批量更新数据(每批次5000) 
    ///// </summary> 
    ///// <param name="connString">数据库链接字符串</param> 
    ///// <param name="table"></param> 
    //private void Update(string connString, DataTable table, int batchSize)
    //{
    //    SqlConnection conn = new SqlConnection(connString);
    //    SqlCommand comm = conn.CreateCommand();
    //    comm.CommandTimeout = 300;
    //    comm.CommandType = CommandType.Text;
    //    SqlDataAdapter adapter = new SqlDataAdapter(comm);
    //    SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
    //    commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
    //    try
    //    {
    //        conn.Open();
    //        //设置批量更新的每次处理条数 
    //        adapter.UpdateBatchSize = batchSize;
    //        adapter.SelectCommand.Transaction = conn.BeginTransaction();/////////////////开始事务 
    //        if (table.ExtendedProperties["SQL"] != null)
    //        {
    //            adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
    //        }
    //        adapter.Update(table);
    //        adapter.SelectCommand.Transaction.Commit();/////提交事务 
    //    }
    //    catch (Exception ex)
    //    {
    //        if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
    //        {
    //            adapter.SelectCommand.Transaction.Rollback();
    //        }
    //        throw ex;
    //    }
    //    finally
    //    {
    //        conn.Close();
    //        conn.Dispose();
    //    }
    //}

    private DataTable CreateDataTable<T>(TableMapping tableMapping, IEnumerable<T> entities, bool isInsert = true)
    {
      var dataTable = BuildDataTable<T>(tableMapping);

      foreach (var entity in entities)
      {
        DataRow row = dataTable.NewRow();

        foreach (var columnMapping in tableMapping.Columns)
        {
          var @value = entity.GetPropertyValue(columnMapping.PropertyName);

          if (columnMapping.IsIdentity && isInsert) continue;

          if (@value == null)
          {
            row[columnMapping.ColumnName] = DBNull.Value;
          }
          else
          {
            row[columnMapping.ColumnName] = @value;
          }
        }

        dataTable.Rows.Add(row);
      }

      return dataTable;
    }

    private DataTable BuildDataTable<T>(TableMapping tableMapping)
    {
      var entityType = typeof(T);
      string tableName = string.Join(@".", tableMapping.SchemaName, tableMapping.TableName);

      var dataTable = new DataTable(tableName);
      var primaryKeys = new List<DataColumn>();

      foreach (var columnMapping in tableMapping.Columns)
      {
        var propertyInfo = entityType.GetProperty(columnMapping.PropertyName, '.');
        columnMapping.Type = propertyInfo.PropertyType;

        var dataColumn = new DataColumn(columnMapping.ColumnName);

        Type dataType;
        if (propertyInfo.PropertyType.IsNullable(out dataType))
        {
          dataColumn.DataType = dataType;
          dataColumn.AllowDBNull = true;
        }
        else
        {
          dataColumn.DataType = propertyInfo.PropertyType;
          dataColumn.AllowDBNull = columnMapping.Nullable;
        }

        if (columnMapping.IsIdentity)
        {
          dataColumn.Unique = true;
          if (propertyInfo.PropertyType == typeof(int)
            || propertyInfo.PropertyType == typeof(long))
          {
            dataColumn.AutoIncrement = true;
          }
          else continue;
        }
        else
        {
          dataColumn.DefaultValue = columnMapping.DefaultValue;
        }

        if (propertyInfo.PropertyType == typeof(string))
        {
          dataColumn.MaxLength = columnMapping.MaxLength;
        }

        if (columnMapping.IsPk)
        {
          primaryKeys.Add(dataColumn);
        }

        if (string.Compare("Id", columnMapping.ColumnName, true) != 0)
            _updateSql += string.Format(" {1}.{0}={2}.{0},", columnMapping.ColumnName, tableName, _tempTable);

        dataTable.Columns.Add(dataColumn);
      }

      _updateSql = _updateSql.TrimEnd(',');
      _updateSql += string.Format(" from {0} where {0}.Id ={1}.Id", _tempTable, tableName);

      dataTable.PrimaryKey = primaryKeys.ToArray();

      return dataTable;
    }
    //private static DataTable CreateDataTable2<T>(TableMapping tableMapping, IEnumerable<T> entities, out string updateSql, string temp = "", bool isInsert = true)
    //{
    //    var dataTable = BuildDataTable2<T>(tableMapping,out updateSql,temp);

    //    foreach (var entity in entities)
    //    {
    //        DataRow row = dataTable.NewRow();

    //        foreach (var columnMapping in tableMapping.Columns)
    //        {
    //            var @value = entity.GetPropertyValue(columnMapping.PropertyName);

    //            if (columnMapping.IsIdentity && isInsert) continue;

    //            if (@value == null)
    //            {
    //                row[columnMapping.ColumnName] = DBNull.Value;
    //            }
    //            else
    //            {
    //                row[columnMapping.ColumnName] = @value;
    //            }
    //        }

    //        dataTable.Rows.Add(row);
    //    }

    //    return dataTable;
    //}
    //private static DataTable BuildDataTable2<T>(TableMapping tableMapping, out string updateSql, string temp = "")
    //{
    //    updateSql = "";
    //    var entityType = typeof(T);
    //    string tableName = string.Join(@".", tableMapping.SchemaName, tableMapping.TableName);

    //    var dataTable = new DataTable(tableName);
    //    var primaryKeys = new List<DataColumn>();

    //    foreach (var columnMapping in tableMapping.Columns)
    //    {
    //        var propertyInfo = entityType.GetProperty(columnMapping.PropertyName, '.');
    //        columnMapping.Type = propertyInfo.PropertyType;

    //        var dataColumn = new DataColumn(columnMapping.ColumnName);

    //        Type dataType;
    //        if (propertyInfo.PropertyType.IsNullable(out dataType))
    //        {
    //            dataColumn.DataType = dataType;
    //            dataColumn.AllowDBNull = true;
    //        }
    //        else
    //        {
    //            dataColumn.DataType = propertyInfo.PropertyType;
    //            dataColumn.AllowDBNull = columnMapping.Nullable;
    //        }

    //        if (columnMapping.IsIdentity)
    //        {
    //            dataColumn.Unique = true;
    //            if (propertyInfo.PropertyType == typeof(int)
    //              || propertyInfo.PropertyType == typeof(long))
    //            {
    //                dataColumn.AutoIncrement = true;
    //            }
    //            else continue;
    //        }
    //        else
    //        {
    //            dataColumn.DefaultValue = columnMapping.DefaultValue;
    //        }

    //        if (propertyInfo.PropertyType == typeof(string))
    //        {
    //            dataColumn.MaxLength = columnMapping.MaxLength;
    //        }

    //        if (columnMapping.IsPk)
    //        {
    //            primaryKeys.Add(dataColumn);
    //        }

    //        if (string.Compare("Id", columnMapping.ColumnName, true) != 0)
    //            updateSql += string.Format(" {1}.{0}={2}.{0},", columnMapping.ColumnName, tableName, temp);
    //        dataTable.Columns.Add(dataColumn);
    //    }
    //    updateSql = updateSql.TrimEnd(',');
    //    updateSql+=string.Format(" from {0} where {0}.Id ={1}.Id",temp,tableName);
    //    dataTable.PrimaryKey = primaryKeys.ToArray();

    //    return dataTable;
    //}
  }
}
