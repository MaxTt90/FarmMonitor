using FarmMonitor.BLL;
using FarmMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FarmMonitor.Web.AppCode
{
    public class BaseMethod
    {
        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset)
        {
            return Regex.IsMatch(str_handset, @"^1[3,4,5,6,7,8]\d{9}$");
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$");
        }
    }
}