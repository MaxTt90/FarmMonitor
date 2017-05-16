/**
* SysRightInfo.cs
*
* 功 能： N/A
* 类 名： SysRightInfo
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

namespace FarmMonitor.Model
{
    //SysRightInfo
    [Serializable]
    [Table("SysRightInfo")]
    public class SysRightInfo
    {
        #region Model
        private int _id;
        private int _parentid = 0;
        private string _rightname = "";
        private int _righttype = 1;
        private string _filepath = "";
        private string _description = "";
        private int _datastate = 1;
        private int _createid = 0;
        private DateTime _createtime = DateTime.Now;
        private int _updateid = 0;
        private DateTime _updatetime = DateTime.Now;
        private bool _ispublic = false;
        private int _sortid = 0;
        private int _operatetypeenum = 0;
        private bool _isview = true;
        private string _picpath = "";
        private string _picpathchange = "";

        /// <summary>
        /// 自动编号
        /// </summary>		
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 父级编号
        /// </summary>		

        public int ParentId
        {
            get { return _parentid; }
            set { _parentid = value; }
        }

        /// <summary>
        /// 权限名称
        /// </summary>		
        [MaxLength(50, ErrorMessage = "字符长度超过50")]
        public string RightName
        {
            get { return _rightname; }
            set { _rightname = value; }
        }

        /// <summary>
        /// 权限类型:1=菜单;2=权限
        /// </summary>		

        public int RightType
        {
            get { return _righttype; }
            set { _righttype = value; }
        }

        /// <summary>
        /// 文件路径
        /// </summary>		
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string FilePath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>		
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 数据状态:1=启用,2=禁用,3=删除
        /// </summary>		

        public DataStateEnum DataState
        {
            get { return (DataStateEnum)_datastate; }
            set { _datastate = (int)value; }
        }

        /// <summary>
        /// 创建者
        /// </summary>		

        public int CreateId
        {
            get { return _createid; }
            set { _createid = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>		

        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }

        /// <summary>
        /// 更新者
        /// </summary>		

        public int UpdateId
        {
            get { return _updateid; }
            set { _updateid = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>		

        public DateTime UpdateTime
        {
            get { return _updatetime; }
            set { _updatetime = value; }
        }

        /// <summary>
        /// 是否是公用
        /// </summary>		

        public bool IsPublic
        {
            get { return _ispublic; }
            set { _ispublic = value; }
        }

        /// <summary>
        /// 排序编号
        /// </summary>		

        public int SortId
        {
            get { return _sortid; }
            set { _sortid = value; }
        }

        /// <summary>
        /// 操作类型
        /// </summary>		

        public int OperateTypeEnum
        {
            get { return _operatetypeenum; }
            set { _operatetypeenum = value; }
        }

        /// <summary>
        /// 是否显示菜单
        /// </summary>		

        public bool IsView
        {
            get { return _isview; }
            set { _isview = value; }
        }

        /// <summary>
        /// 图片路径
        /// </summary>		
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string PicPath
        {
            get { return _picpath; }
            set { _picpath = value; }
        }

        /// <summary>
        /// 切换图片路径
        /// </summary>
        [MaxLength(200, ErrorMessage = "字符长度超过200")]
        public string PicPathChange
        {
            get { return _picpathchange; }
            set { _picpathchange = value; }
        }

        #endregion

        public virtual System.Collections.Generic.ICollection<SysRoleRight> SysRoleRights { get; set; }
    }
}