using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMonitor.Model
{
    public class SensorDataModel
    {
        private int _orchardId;

        private string _dataId;

        private decimal _data;

        private DateTime _time;

        private int _collectorId;

        private int _sensorId;

        private string _sensorName;

        private string _unit;

        private string _resolution;

        private string _scope;

        public int OrchardId
        {
            get{ return _orchardId; }
            set{ _orchardId = value; }
        }

        public string DataId
        {
            get
            {
                return _dataId;
            }

            set
            {
                _dataId = value;
            }
        }

        public decimal Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

        public int CollectorId
        {
            get
            {
                return _collectorId;
            }

            set
            {
                _collectorId = value;
            }
        }

        public int SensorId
        {
            get
            {
                return _sensorId;
            }

            set
            {
                _sensorId = value;
            }
        }

        public string SensorName
        {
            get
            {
                return _sensorName;
            }

            set
            {
                _sensorName = value;
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }

            set
            {
                _unit = value;
            }
        }

        public string Resolution
        {
            get
            {
                return _resolution;
            }

            set
            {
                _resolution = value;
            }
        }

        public string Scope
        {
            get
            {
                return _scope;
            }

            set
            {
                _scope = value;
            }
        }
    }
}
