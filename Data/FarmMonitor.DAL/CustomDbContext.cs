using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FarmMonitor.Model;

namespace FarmMonitor.DAL
{
    public class CustomDbContext : DbContext
    {
        #region 初始化DbContext
        /// <summary>
        /// 实例化新的DBContext
        /// </summary>
        public static CustomDbContext InstanceNew
        {
            get
            {
                CustomDbContext DbContextHelper = new CustomDbContext();
                return DbContextHelper;
            }
        }

        /// <summary>
        /// 同一线程中使用同一个DBContxt
        /// </summary>
        public static CustomDbContext Instance
        {

            get
            {
                CustomDbContext DbContextHelper = null;
                if (System.Runtime.Remoting.Messaging.CallContext.GetData("EFOB") == null)
                {
                    DbContextHelper = new CustomDbContext();
                    System.Runtime.Remoting.Messaging.CallContext.SetData("EFOB", DbContextHelper);
                }
                else
                {
                    return (CustomDbContext)System.Runtime.Remoting.Messaging.CallContext.GetData("EFOB");
                }
                return DbContextHelper;
            }
        }

        public CustomDbContext()
            : base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            #region  EF 默认decimal类型精度(14,6)

           // modelBuilder.Entity<CollectData>().Property(m => m.Data).HasPrecision(14, 6);
            modelBuilder.Entity<DataHeader>().Property(m => m.CollectPeriod).HasPrecision(2, 0);

            #endregion
        }
        #endregion

        #region log

        public DbSet<LogTrack> LogTracks { set; get; }

        #endregion

        #region 后台系统

        public DbSet<SysUserInfo> SysUserInfos { get; set; }
        public DbSet<SysRightInfo> SysRightInfos { get; set; }
        public DbSet<SysRoleInfo> SysRoleInfos { get; set; }
        public DbSet<SysRoleRight> SysRoleRights { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysCity> SysCitys { get; set; }

        #endregion
        #region 业务逻辑
        public DbSet<Orchard> Orchards { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<DataHeader> DataHeaders { get; set; }
        public DbSet<Collector> Collectors { get; set; }
        public DbSet<UserOrchard> UserOrchards { get; set; }
        //public DbSet<AlarmData> AlarmDatas { get; set; }
        //public DbSet<CollectData> CollectDatas { get; set; }
        #endregion

    }
}
