/**
* SysCity.cs
*
* 功 能： N/A
* 类 名： SysCity
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
	//SysCity
    [Serializable]
    [Table("SysCity")]
	public class SysCity
	{
		#region Model
		private int _id;  
		private int _citylevel = 0;  
		private string _name = "";  
		private string _shortname = "";  
		private string _citycode = "";  
		private string _englishname = "";  
		private string _postcode = "";  
		private int _createid = 0;  
		private DateTime _createtime = DateTime.Now;  
		private int _updateid = 0;  
		private DateTime _updatetime = DateTime.Now;  
		private int _cityid = 0;  
		private int _parentid = 0;  
		private int _provinceid = 0;  
		private int _regionid = 0;  
		private int _countryid = 0;  
		private string _abbreviation = "";  
		private int _datastate = 1;  
		private int _sortid = 0;  
		
		/// <summary>
		/// 编号
		/// </summary>		
                [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id
        {
            get{ return _id; }
            set{ _id = value; }
        }    
        
		/// <summary>
		/// 城市级别
		/// </summary>		
		       
        public int CityLevel
        {
            get{ return _citylevel; }
            set{ _citylevel = value; }
        }    
        
		/// <summary>
		/// 名称
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string Name
        {
            get{ return _name; }
            set{ _name = value; }
        }    
        
		/// <summary>
		/// 缩写名称
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string ShortName
        {
            get{ return _shortname; }
            set{ _shortname = value; }
        }    
        
		/// <summary>
		/// 区位号
		/// </summary>		
		[MaxLength(20, ErrorMessage = "字符长度超过20")]       
        public string CityCode
        {
            get{ return _citycode; }
            set{ _citycode = value; }
        }    
        
		/// <summary>
		/// 拼音
		/// </summary>		
		[MaxLength(50, ErrorMessage = "字符长度超过50")]       
        public string EnglishName
        {
            get{ return _englishname; }
            set{ _englishname = value; }
        }    
        
		/// <summary>
		/// 邮编
		/// </summary>		
		[MaxLength(20, ErrorMessage = "字符长度超过20")]       
        public string PostCode
        {
            get{ return _postcode; }
            set{ _postcode = value; }
        }    
        
		/// <summary>
		/// 创建人员
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
		/// 更新人员
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
        
		/// <summary>
		/// 城市编号
		/// </summary>		
		       
        public int CityId
        {
            get{ return _cityid; }
            set{ _cityid = value; }
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
		/// 省份编号
		/// </summary>		
		       
        public int ProvinceId
        {
            get{ return _provinceid; }
            set{ _provinceid = value; }
        }    
        
		/// <summary>
		/// 区域编号
		/// </summary>		
		       
        public int RegionId
        {
            get{ return _regionid; }
            set{ _regionid = value; }
        }    
        
		/// <summary>
		/// 国家编号
		/// </summary>		
		       
        public int CountryId
        {
            get{ return _countryid; }
            set{ _countryid = value; }
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
		/// 数据状态:1=启用,2=禁用,3=删除
		/// </summary>		
		       
        public int DataState
        {
            get{ return _datastate; }
            set{ _datastate = value; }
        }    
        
		/// <summary>
		/// 排序字段
		/// </summary>		
		       
        public int SortId
        {
            get{ return _sortid; }
            set{ _sortid = value; }
        }    
        
		#endregion

        public virtual ICollection<SysUserInfo> SysUserInfos { get; set; } 
	}
}