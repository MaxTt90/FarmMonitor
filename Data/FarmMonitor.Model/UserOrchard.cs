/**
* SysUserRole.cs
*
* 功 能： N/A
* 类 名： SysUserRole
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/11/13 15:34:00   N/A    初版
*
* Copyright (c) 2015 WEDO Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海唯都企业管理咨询有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FarmMonitor.Model
{
    //SysUserRole
    [Serializable]
    [Table("user_orchard")]
    public class UserOrchard
    {
        #region Model
        private int _id;
        private int _userinfoid = 0;
        private int _orchardid = 0;

        /// <summary>
        /// Id
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 用户编号
        /// </summary>		

        public int UserInfoId
        {
            get { return _userinfoid; }
            set { _userinfoid = value; }
        }

        /// <summary>
        /// 果园Id
        /// </summary>		

        public int OrchardId
        {
            get { return _orchardid; }
            set { _orchardid = value; }
        }

       

        #endregion

        [ForeignKey("OrchardId")]
        public virtual Orchard Orchard { get; set; }

        [ForeignKey("UserInfoId")]
        public virtual SysUserInfo SysUserInfo { get; set; }
    }
}