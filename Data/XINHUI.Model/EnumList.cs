using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMonitor.Model
{
    public enum DataStateEnum
    {
        正常 = 1,
        禁用 = 2,
        删除 = 3
    }
    public enum SexEnum
    {
        未知 = 0,
        男 = 1,
        女 = 2
    }

    public enum CarOwnerEnum
    {
        未知=0,
        车主=1,
        非车主=2
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStateEnum
    {
        在职 = 1,
        禁用 = 2,
        删除 = 3,
        离职 = 4
    }

    /// <summary>
    /// user验证类型
    /// </summary>	
    public enum ValidationTypeEnum
    {
        不用验证 = 0,
        短信验证 = 1,
        邮件验证 = 2
    }

    /// <summary>
    /// 公众账号类型
    /// </summary>	
    public enum AccountTypeEnum
    {
        订阅号 = 1,
        服务号 = 2,
        企业号 = 3
    }
    /// <summary>
    /// 企业号成员关注状态:1=已关注，2=已冻结，4=未关注
    /// </summary>
    public enum StatusEnum
    {
        已关注 = 1,
        已冻结 = 2,
        未关注 = 4
    }
    /// <summary>
    /// 公众号类型
    /// </summary>
    public enum AccountType
    {
        订阅号 = 1,
        服务号 = 2, 企业号 = 3
    }

    /// <summary>
    /// 表单提交成功后操作
    /// </summary>
    public enum SubmitTypeEnum
    {
        刷新表单 = 1,
        跳转指定Url = 2
    }
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        未审核=1,
        已审核=2, 
        已取消=3,
        已发货=4,
        已兑换=5
    }
    /// <summary>
    /// 会员等级
    /// </summary>
    public enum CustomerLevel
    {
        普通会员=1,
        铂金会员=2,
        钻石会员=3
    }
    public class EnumList
    {
        /// <summary>
        /// 操作类型枚举:1表示新增、2表示编辑，3表示查询、4表示删除、5表示导出
        /// </summary>
        public enum OperationType
        {
            新增 = 1,
            编辑 = 2,
            查询 = 3,
            删除 = 4,
            导出 = 5,
            导入 = 6
        }

        /// <summary>
        /// 消息类型枚举，请转小写后使用
        /// </summary>
        public enum MsgType
        {
            Event,
            text,
            image,
            news,
            voice,
            video,
            shortvideo,
            music,
            location,
            link,
            templet
        }
        /// <summary>
        /// Media Id类型
        /// </summary>
        /// Author:fredjiang
        /// Created:2015-06-11
        public enum MediaIdType
        {
            无 = 1,
            临时 = 2,
            永久 = 3
        }
        public enum MenuType
        {
            click = 1,
            view = 2,
            scancode_push = 3,
            menu = 4
        }
        /// <summary>
        /// 商品状态 1启用 2禁用 3删除
        /// </summary>
        public enum ProductStatus
        {
            启用 = 1,
            禁用 = 2,
            删除 = 3
        }

        
        /// <summary>
        /// 是否启用库存 0不启用 1启用
        /// </summary>
        public enum IsStockEnum
        {
            不启用 = 0,
            启用 = 1
        }
        /// <summary>
        /// 库存类型
        /// </summary>
        public enum StockType
        {
            入库 = 1,
            冻结 = 2,
            出库 = 3
        }
    }

}
