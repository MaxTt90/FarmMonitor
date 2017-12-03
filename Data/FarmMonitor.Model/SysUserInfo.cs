/**
* SysUserInfo.cs
*
* 功 能： N/A
* 类 名： SysUserInfo
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
	//SysUserInfo
    [Serializable]
    [Table("SysUserInfo")]
	public class SysUserInfo
	{
		#region Model
		private int _id;  
		private string _name = "";  
		private string _password = "";  
		private string _usercode = "";  
		private int _sex = 0;  
		private string _tel = "";  
		private DateTime? _birthday;  
		private int _province = 0;  
		private int _city = 0;
        private int _zone = 0;
		private int _supervisior = 0;  
		private string _mobile = "";  
		private string _email = "";  
		private DateTime _workdate = DateTime.Now;  
		private int _position = 0;  
		private int _userstate = 1;  
		private DateTime _createdate = DateTime.Now;  
		private int _createuser = 0;  
		private DateTime _updatetime = DateTime.Now;  
		private int _updateid = 0;  
		private DateTime? _departuretime;  
		private string _remark = "";
        private DateTime _lastlogintime = DateTime.Now;   
		private string _lastloginip = "";  
		private int _loginfailures = 0;  
		private int _validationtype = 0;
        private int _accounttype = 0;    
        private string _headimgurl = "";
		/// <summary>
		/// 自动编号
		/// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }    
        
		/// <summary>
		/// 姓名
		/// </summary>		
		       
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }    
        
		/// <summary>
		/// 密码
		/// </summary>		
		       
        public string Password
        {
            get{ return _password; }
            set{ _password = value; }
        }    
        
		/// <summary>
		/// 用户代码
		/// </summary>		
		       
        public string UserCode
        {
            get{ return _usercode; }
            set{ _usercode = value; }
        }    
        
		/// <summary>
		/// 0表示未知,1表示男,2表示女
		/// </summary>		

        public SexEnum Sex
        {
            get { return (SexEnum)_sex; }
            set{ _sex = (int)value; }
        }    
        
		/// <summary>
		/// 电话
		/// </summary>		
		       
        public string Tel
        {
            get{ return _tel; }
            set{ _tel = value; }
        }    
        
		/// <summary>
		/// 生日
		/// </summary>		
		       
        public DateTime? Birthday
        {
            get{ return _birthday; }
            set{ _birthday = value; }
        }    
        
		/// <summary>
		/// 省份
		/// </summary>		
		       
        public int Province
        {
            get{ return _province; }
            set{ _province = value; }
        }    
        
		/// <summary>
		/// 城市
		/// </summary>		
		       
        public int City
        {
            get{ return _city; }
            set{ _city = value; }
        }

        /// <summary>
        /// 区
        /// </summary>		

        public int Zone
        {
            get { return _zone; }
            set { _zone = value; }
        }

        /// <summary>
        /// 上级姓名
        /// </summary>		

        public int Supervisior
        {
            get{ return _supervisior; }
            set{ _supervisior = value; }
        }    
        
		/// <summary>
		/// 手机
		/// </summary>		
		       
        public string Mobile
        {
            get{ return _mobile; }
            set{ _mobile = value; }
        }    
        
		/// <summary>
		/// 邮箱
		/// </summary>		
		       
        public string Email
        {
            get{ return _email; }
            set{ _email = value; }
        }    
        
		/// <summary>
		/// 入职时间
		/// </summary>		
		       
        public DateTime WorkDate
        {
            get{ return _workdate; }
            set{ _workdate = value; }
        }    
        
		/// <summary>
		/// 职位
		/// </summary>		
		       
        public int Position
        {
            get{ return _position; }
            set{ _position = value; }
        }    
        
		/// <summary>
		/// 用户状态:1表示在职,2表示禁用,3表示删除,4表示离职
		/// </summary>		

        public UserStateEnum UserState
        {
            get { return (UserStateEnum)_userstate; }
            set{ _userstate = (int)value; }
        }    
        
		/// <summary>
		/// 创建时间
		/// </summary>		
		       
        public DateTime CreateDate
        {
            get{ return _createdate; }
            set{ _createdate = value; }
        }    
        
		/// <summary>
		/// 创建者
		/// </summary>		
		       
        public int CreateUser
        {
            get{ return _createuser; }
            set{ _createuser = value; }
        }    
        
		/// <summary>
		/// 更新时间
		/// </summary>		
		       
        public DateTime UpdateTime
        {
            get{ return _updatetime; }
            set{ _updatetime = value; }
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
		/// 离职时间
		/// </summary>		
		       
        public DateTime? DepartureTime
        {
            get{ return _departuretime; }
            set{ _departuretime = value; }
        }    
        
		/// <summary>
		/// 人员备注
		/// </summary>		
		[MaxLength(200, ErrorMessage = "字符长度超过200")]       
        public string Remark
        {
            get{ return _remark; }
            set{ _remark = value; }
        }    
        
		/// <summary>
		/// 最后登录时间
		/// </summary>		
		       
        public DateTime LastLoginTime
        {
            get{ return _lastlogintime; }
            set{ _lastlogintime = value; }
        }    
        
		/// <summary>
		/// 最后登录IP
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string LastLoginIp
        {
            get{ return _lastloginip; }
            set{ _lastloginip = value; }
        }    
        
		/// <summary>
		/// 登录失败次数
		/// </summary>		
		       
        public int LoginFailures
        {
            get{ return _loginfailures; }
            set{ _loginfailures = value; }
        }    
        
		/// <summary>
		/// 验证类型:0=不用验证,1=短信验证,2=邮件验证
		/// </summary>		

        public ValidationTypeEnum ValidationType
        {
            get { return (ValidationTypeEnum)_validationtype; }
            set{ _validationtype = (int)value; }
        }

        /// <summary>
        /// 账号类型:1=后台账号
        /// </summary>
        public int AccountType
        {
            get { return _accounttype; }
            set { _accounttype = value; }
        }

        
        /// <summary>
        /// 用户头像
        /// </summary>
        public string HeadImgUrl {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }


        #endregion

        [ForeignKey( "City")]
        public virtual SysCity SysCity { get; set; }

        public virtual System.Collections.Generic.ICollection<SysUserRole> SysUserRoles { get; set; }
        public virtual System.Collections.Generic.ICollection<SysRoleRight> SysRoleRights { get; set; }
	}
}