/**
* orchard.cs
*
* 功 能： N/A
* 类 名： orchard
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
	//orchard
    [Serializable]
    [Table("orchard")]
	public class Orchard
	{
		#region Model
		private int _orchard_id;  
		private string _address;  
		
		/// <summary>
        /// 果园编号
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("orchard_id")]
        public int OrchardId
        {
            get{ return _orchard_id; }
            set{ _orchard_id = value; }
        }    
        
		/// <summary>
		/// 果园地址
        /// </summary>		
        [Column("address")]
        public string Address
        {
            get{ return _address; }
            set{ _address = value; }
        }    
        
		#endregion
	}
}