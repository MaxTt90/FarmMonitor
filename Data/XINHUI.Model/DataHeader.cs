/**
* data_header.cs
*
* 功 能： N/A
* 类 名： data_header
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
	//data_header
    [Serializable]
    [Table("data_header")]
	public class DataHeader
	{
		#region Model
		private string _data_id;  
		private int? _collector_id;  
		private int _msg_type;  
		private int _msg_nbr;  
		private decimal _collect_period;  
		private DateTime _time;  
		
		/// <summary>
		/// 报文消息ID。orch_collector_id +time生成报文记录的唯一ID。
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Column("data_id")]
        public string DataId
        {
            get{ return _data_id; }
            set{ _data_id = value; }
        }    
        
		/// <summary>
		/// 果园采集器全局ID。
        /// </summary>		
        [Column("collector_id")]
        public int? CollectorId
        {
            get{ return _collector_id; }
            set{ _collector_id = value; }
        }    
        
		/// <summary>
		/// 消息类型 2:传感器到服务器;130:服务器到传感器
        /// </summary>		
        [Column("msg_type")]
        public int MsgType
        {
            get{ return _msg_type; }
            set{ _msg_type = value; }
        }    
        
		/// <summary>
		/// 消息顺序号，每次发送顺序号加1
        /// </summary>		
        [Column("msg_nbr")]
        public int MsgNbr
        {
            get{ return _msg_nbr; }
            set{ _msg_nbr = value; }
        }    
        
		/// <summary>
        /// 采样周期 分钟
        /// </summary>		
        [Column("collect_period")]
        public decimal CollectPeriod
        {
            get{ return _collect_period; }
            set{ _collect_period = value; }
        }    
        
		/// <summary>
		/// 上报时间
        /// </summary>		
        [Column("time")]
        public DateTime Time
        {
            get{ return _time; }
            set{ _time = value; }
        }    
        
		#endregion
	}
}