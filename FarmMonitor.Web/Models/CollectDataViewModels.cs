using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmMonitor.Web.Models
{
    public class CollectDataViewModels
    {
        public int OrchardId { get; set; }
        public int CollectorId { get; set; }
        public string DataId { get; set; }
        public DateTime Time { get; set; }
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public decimal Data { get; set; }
        public string Unit { get; set; }
        public string Resolution { get; set; }
        public string Scope { get; set; }

    }
}