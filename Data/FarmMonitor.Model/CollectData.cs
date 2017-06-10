/**
* collect_data.cs
*
* 功 能： N/A
* 类 名： collect_data
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
	//collect_data
    [Serializable]
    [Table("collect_data")]
	public class CollectData
	{
		#region Model
		private string _data_id;  
		private int _sensor_id;  
		private decimal _data;  
		
		/// <summary>
		/// 报文消息ID。
		/// </summary>		
        [Column("data_id")]       
        public string DataId
        {
            get{ return _data_id; }
            set{ _data_id = value; }
        }    
        
		/// <summary>
		/// 传感器Id
        /// </summary>	
        [Column("sensor_id")]  	
        public int SensorId
        {
            get{ return _sensor_id; }
            set{ _sensor_id = value; }
        }    
        
		/// <summary>
		/// 采集数值
        /// </summary>		
        [Column("data")]  	
        public decimal Data
        {
            get{ return _data; }
            set{ _data = value; }
        }

        #endregion

        //[ForeignKey("DataId")]
        //public virtual DataHeader DataHeader { get; set; }
    }
}