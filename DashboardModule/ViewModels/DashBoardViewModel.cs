using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using FarmMonitor.BLL;
using FarmMonitor.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PresentationModule.Services;
using DashboardModule.Services;
using FarmMonitor.Infrastructure;
using Prism.Commands;

namespace DashboardModule.ViewModels
{
    public class DashboardViewModel : BindableBase, IDashboardViewModel
    {
        private readonly ILoginService _loginService;

        private readonly ISensorPlotModelHandler _sensorPlotModelHandler;

        private ObservableCollection<SensorDataModel> _sensorDataCollection;

        private SensorChartViewModel _sensorChartViewModel;

        private SensorDataModel _selectedSensorDataModel;

        private IChartProvider _chartProvider;

        private ICommand _switchTimeGapCommand;

        public DashboardViewModel(ILoginService loginService, ISensorPlotModelHandler sensorPlotModelHandler)
        {
            _loginService = loginService;
            _sensorPlotModelHandler = sensorPlotModelHandler;

            _switchTimeGapCommand = new DelegateCommand<string>(SwitchTimeGap);
            _chartProvider = AppContext.Current.Container.Resolve<IChartProvider>();

            if (_loginService != null && _loginService.CurrentUser != null && _sensorPlotModelHandler != null)
            {
                InitializeData();
            }
        }

        public IChartProvider ChartProvider
        {
            get { return _chartProvider; }

            set { SetProperty(ref _chartProvider, value); }
        }

        public SensorDataModel SelectedSensorDataModel
        {
            get { return _selectedSensorDataModel; }
            set
            {
                if (SetProperty(ref _selectedSensorDataModel, value))
                {
                    SensorChartViewModel = ChartProvider.GetChart(value);
                    //SensorChartViewModel = _sensorPlotModelHandler.GetChartViewModel(value);
                }
            }
        }

        public ObservableCollection<SensorDataModel> SensorDataCollection
        {
            get { return _sensorDataCollection; }
            set { SetProperty(ref _sensorDataCollection, value); }
        }

        public SensorChartViewModel SensorChartViewModel
        {
            get { return _sensorChartViewModel; }
            set
            {
                if (SetProperty(ref _sensorChartViewModel, value))
                {
                    SensorChartViewModel.UpdateChart();
                }
            }
        }

        private void InitializeData()
        {
            var collectDataMan = new CollectDataMan();
            var sensorDataList = collectDataMan.SearchList(_loginService.CurrentUser.ID, DateTime.Now.AddDays(-100), DateTime.Now);

            if (sensorDataList != null)
            {
                _sensorDataCollection = new ObservableCollection<SensorDataModel>(sensorDataList);
                //_sensorPlotModelHandler.GenerateSensorChartViewModelSet(sensorDataList);
                SelectedSensorDataModel = _sensorDataCollection[0];
            }
        }

        private void SwitchTimeGap(string timeGap)
        {
            switch (timeGap)
            {
                case "Year":

                    break;
                case "Month":

                    break;
                case "Day":

                    break;
            }
        }
    }
}
