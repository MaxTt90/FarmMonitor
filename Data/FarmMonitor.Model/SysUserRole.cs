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
    [Table("SysUserRole")]
	public class SysUserRole
	{
		#region Model
		private int _id;  
		private int _roleid = 0;  
		private int _userid = 0;  
		private int _createid = 0;  
		private DateTime _createtime = DateTime.Now;
        private int _updateid = 0;
        private DateTime _updatetime = DateTime.Now;
		
		/// <summary>
		/// Id
		/// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]      
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }    
        
		/// <summary>
		/// 角色编号
		/// </summary>		
		       
        public int RoleId
        {
            get{ return _roleid; }
            set{ _roleid = value; }
        }    
        
		/// <summary>
		/// 用户编号
		/// </summary>		
		       
        public int UserId
        {
            get{ return _userid; }
            set{ _userid = value; }
        }    
        
		/// <summary>
		/// 创建者
		/// </summary>		
		       
        public int CreateId
        {
            get{ return _createid; }
            set{ _createid = value; }
        }    
        
		/// <summary>
		/// 创建时间
		/// </summary>		
		       
        public DateTime CreateTime
        {
            get{ return _createtime; }
            set{ _createtime = value; }
        }    
        
		/// <summary>
		/// 更新者
		/// </summary>		
		       
        public int UpdateId
        {
            get{ return _updateid; }
            set{ _updateid = value; }
        }    
        
		/// <summary>
		/// 更新时间
		/// </summary>		
		       
        public DateTime UpdateTime
        {
            get{ return _updatetime; }
            set{ _updatetime = value; }
        }    
        
		#endregion

        [ForeignKey("RoleId")]
        public virtual SysRoleInfo SysRoleInfo { get; set; }

        [ForeignKey("UserId")]
        public virtual SysUserInfo SysUserInfo { get; set; }
	}
}