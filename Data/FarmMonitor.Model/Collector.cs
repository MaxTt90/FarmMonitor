/**
* collector.cs
*
* 功 能： N/A
* 类 名： collector
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
    //collector 采集设备
    [Serializable]
    [Table("collector")]
	public class Collector
	{
		#region Model
		private int _collector_id;  
		private int _orchard_id;  
		private int _orchard_collector_id;  
		private string _sn;  
		
		/// <summary>
        /// collector_id 果园采集器ID
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("collector_id")]       
        public int CollectorId
        {
            get{ return _collector_id; }
            set{ _collector_id = value; }
        }    
        
		/// <summary>
        /// orchard_id 果园编号
        /// </summary>		
        [Column("orchard_id")]  
        public int OrchardId
        {
            get{ return _orchard_id; }
            set{ _orchard_id = value; }
        }    
        
		/// <summary>
		/// orchard_collector_id
        /// </summary>		
        [Column("orchard_collector_id")]  
        public int OrchardCollectorId
        {
            get{ return _orchard_collector_id; }
            set{ _orchard_collector_id = value; }
        }    
        
		/// <summary>
		/// sn
        /// </summary>		
        [Column("sn")]  
        public string Sn
        {
            get{ return _sn; }
            set{ _sn = value; }
        }    
        
		#endregion
	}
}