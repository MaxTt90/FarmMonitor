/**
* SysRoleInfo.cs
*
* 功 能： N/A
* 类 名： SysRoleInfo
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
	//SysRoleInfo
    [Serializable]
    [Table("SysRoleInfo")]
	public class SysRoleInfo
	{
		#region Model
		private int _id;  
		private string _name = "";  
		private string _code = "";  
		private string _abbreviation = "";  
		private string _description = "";
        private int _parentid = 0;
		private int _datastate = 1;  
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
		/// 角色名称
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }    
        
		/// <summary>
		/// 角色代码
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string Code
        {
            get{ return _code; }
            set{ _code = value; }
        }    
        
		/// <summary>
		/// 缩写
		/// </summary>		
		[MaxLength(20, ErrorMessage = "字符长度超过20")]       
        public string Abbreviation
        {
            get{ return _abbreviation; }
            set{ _abbreviation = value; }
        }    
        
		/// <summary>
		/// 角色描述
		/// </summary>		
		[MaxLength(200, ErrorMessage = "字符长度超过200")]       
        public string Description
        {
            get{ return _description; }
            set{ _description = value; }
        }    
        
		/// <summary>
		/// 上级编号
		/// </summary>		
		       
        public int ParentId
        {
            get{ return _parentid; }
            set{ _parentid = value; }
        }    
        
		/// <summary>
		/// 数据状态:1=正常,2=禁用,3=删除
		/// </summary>		

        public DataStateEnum DataState
        {
            get { return (DataStateEnum)_datastate; }
            set{ _datastate = (int)value; }
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

        public virtual System.Collections.Generic.ICollection<SysUserRole> SysUserRoles { get; set; }
        public virtual System.Collections.Generic.ICollection<SysRoleRight> SysRoleRights { get; set; }
	}
}