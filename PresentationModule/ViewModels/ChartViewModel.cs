using OxyPlot;
using OxyPlot.Series;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace PresentationModule.ViewModels
{
    public class ChartViewModel : BindableBase, IChartViewModel
    {
        #region Fields

        private readonly PlotModel _plotModel;

        #endregion

        #region Constructors and Destructors

        public ChartViewModel()
        {
            _plotModel = new PlotModel();
        }

        #endregion

        #region Public Properties

        public PlotModel PlotModel
        {
            get
            {
                return _plotModel;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void SwitchOnLegend(bool switcher)
        {
            PlotModel.IsLegendVisible = switcher;
            PlotModel.InvalidatePlot(false);
        }

        public virtual void UpdateChart(IEnumerable<LineSeries> lines = null)
        {
            lock (PlotModel.SyncRoot)
            {
                PlotModel.Series.Clear();

                var lineList = new List<LineSeries>(lines ?? Enumerable.Empty<LineSeries>());

                foreach (var line in lineList)
                {
                    PlotModel.Series.Add(line);
                }
            }

            PlotModel.InvalidatePlot(true);
        }

        #endregion
    }
}
