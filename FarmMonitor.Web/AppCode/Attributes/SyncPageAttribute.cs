using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMonitor.Web.AppCode.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SyncPageAttribute : Attribute
    {
    }
}