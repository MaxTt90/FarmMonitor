using System.Collections.Generic;
using System.Data.Entity;

namespace BulkExtensions
{
  internal static class DbContextBulkOperationExtensions
  {
    public const int DefaultBatchSize = 1000;

    public static void BulkInsert<T>(this DbContext context, IEnumerable<T> entities, int batchSize = DefaultBatchSize)
    {
      var provider = new BulkOperationProvider(context);
      provider.Insert(entities, batchSize);
    }

    public static void BulkUpdate<T>(this DbContext context, IEnumerable<T> entities, int batchSize = DefaultBatchSize)
    {
        var provider = new BulkOperationProvider(context);
        provider.Update(entities, batchSize);
    }
  }
}
