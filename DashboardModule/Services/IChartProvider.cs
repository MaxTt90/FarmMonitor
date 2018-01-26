using DashboardModule.ViewModels;
using FarmMonitor.Model;
using OxyPlot;
using OxyPlot.Series;

namespace DashboardModule.Services
{
    public interface IChartProvider
    {
        SensorChartViewModel GetChart(SensorDataModel dataSource);
    }
}
