using DashboardModule.ViewModels;
using FarmMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardModule.Services
{
    public interface ISensorPlotModelHandler
    {
        void GenerateSensorChartViewModelSet(IEnumerable<SensorDataModel> sensorDataModelSet);

        SensorChartViewModel GetChartViewModel(SensorDataModel sensorDataModel);
    }
}
