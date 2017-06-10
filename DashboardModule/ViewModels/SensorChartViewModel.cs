using FarmMonitor.Model;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using PresentationModule.ViewModels;
using System.Collections.Generic;

namespace DashboardModule.ViewModels
{
    public class SensorChartViewModel : ChartViewModel
    {
        private readonly LineSeries _sensorSeries;

        private readonly SensorDataModel _sensorDataModel;

        public SensorChartViewModel(SensorDataModel sensorDataModel)
        {
            _sensorDataModel = sensorDataModel;

            var xAxis = new DateTimeAxis
            {
                Title = "Time",
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd"
            };

            var yAxis = new LinearAxis
            {
                Title = string.Format("{0}/{1}", sensorDataModel.SensorName, sensorDataModel.Unit),
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            };

            PlotModel.Axes.Add(xAxis);
            PlotModel.Axes.Add(yAxis);

            _sensorSeries = new LineSeries
            {
                Title = sensorDataModel.SensorName,
                StrokeThickness = 2,
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.Circle,
                MarkerFill = OxyColors.DarkRed,
                MarkerSize = 3,
                Color = OxyColors.Red,
                TrackerFormatString = "{0}\n{1}: {2:F}\n{3}: {4:0.####}"
            };

            PlotModel.Series.Add(_sensorSeries);
        }

        public SensorDataModel SensorDataModel
        {
            get { return _sensorDataModel; }
        } 

        public void IntializeDataPoints(IEnumerable<SensorDataModel> sensorDataModelSet)
        {
            _sensorSeries.IsVisible = true;
            _sensorSeries.Points.Clear();
            foreach (var sensorData in sensorDataModelSet)
            {
                _sensorSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(sensorData.Time), (double)sensorData.Data));
            }
        }

        public void UpdateChart()
        {
            this.PlotModel.InvalidatePlot(true);
            this.PlotModel.ResetAllAxes();
        }

        public bool IsMatchedWithSensorDataModel(SensorDataModel sensorDataModel)
        {
            return SensorDataModel.OrchardId == sensorDataModel.OrchardId 
                && SensorDataModel.CollectorId == sensorDataModel.CollectorId 
                && SensorDataModel.SensorId == sensorDataModel.SensorId;
        }
    }
}
