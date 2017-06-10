using DashboardModule.ViewModels;
using FarmMonitor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardModule.Services
{
    public class SensorPlotModelHandler : ISensorPlotModelHandler
    {
        private IEnumerable<SensorChartViewModel> _sensorChartViewModelSet;

        public SensorPlotModelHandler()
        {
            
        }

        public void GenerateSensorChartViewModelSet(IEnumerable<SensorDataModel> sensorDataModelSet)
        {
            _sensorChartViewModelSet = new List<SensorChartViewModel>();
            var singleSensorDataSet = from perOrchardId in sensorDataModelSet.GroupBy(x => x.OrchardId)
                                      from thenPerCollectorId in perOrchardId.GroupBy(x => x.CollectorId)
                                      from thenPerSensorId in thenPerCollectorId.GroupBy(x => x.SensorId)
                                      select thenPerSensorId.ToList();

            var groupedSensorSet = singleSensorDataSet.ToList();
            if(groupedSensorSet.Any())
            {
                _sensorChartViewModelSet = groupedSensorSet.Select(x => 
                    {
                        var sensorChartViewModel = new SensorChartViewModel(x[0]);
                        sensorChartViewModel.IntializeDataPoints(x);
                        return sensorChartViewModel;
                    }
                );
            }
        }

        public SensorChartViewModel GetChartViewModel(SensorDataModel sensorDataModel)
        {
            return _sensorChartViewModelSet.FirstOrDefault(x => x.IsMatchedWithSensorDataModel(sensorDataModel));
        }
    }
}
