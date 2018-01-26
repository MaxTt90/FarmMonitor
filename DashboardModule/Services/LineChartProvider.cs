using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashboardModule.ViewModels;
using FarmMonitor.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PresentationModule.ViewModels;

namespace DashboardModule.Services
{
    public class LineChartProvider : IChartProvider
    {
        public SensorChartViewModel GetChart(SensorDataModel sensorData)
        {
            var xAxis = new DateTimeAxis
            {
                Title = "Time",
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd"
            };

            var yAxis = new LinearAxis
            {
                Title = string.Format("{0}/{1}", sensorData.SensorName, sensorData.Unit),
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            var sensorSeries = new LineSeries
            {
                Title = sensorData.SensorName,
                StrokeThickness = 2,
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.DarkRed,
                MarkerSize = 3,
                Color = OxyColors.Green,
                TrackerFormatString = "{0}\n{1}: {2:F}\n{3}: {4:0.####}"
            };

            return new SensorChartViewModel(xAxis, yAxis, sensorSeries);
        }
    }
}
