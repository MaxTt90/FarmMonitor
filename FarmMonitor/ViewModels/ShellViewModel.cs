using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PresentationModule.Services;
using FarmMonitor.DAL;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using System.Windows.Input;

namespace FarmMonitor.Desktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private bool _isApplicationMenuVisible;

        private bool _isNavigationChecked;

        private string _staffName;

        private readonly ILoginService _loginService;

        public ShellViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            _isNavigationChecked = true;

            ShowAboutFarmMonitorRequest = new InteractionRequest<INotification>();

            BackCommand = new DelegateCommand(HideApplicationMenu);
            ShowApplicationMenuCommand = new DelegateCommand(ShowApplicationMenu);
            _isApplicationMenuVisible = false;

            //Assembly assembly = Assembly.GetEntryAssembly();
            //AboutPanel = new AboutPanelModel
            //{
            //    BuildVersion = assembly.GetName().Version.ToString(),
            //    BuildDate = Resources.BuildDate,
            //    Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright
            //};

            Initialize();
        }

        public bool IsApplicationMenuVisible
        {
            get { return _isApplicationMenuVisible; }
            set { SetProperty(ref _isApplicationMenuVisible, value); }
        }

        public bool IsNavigationChecked
        {
            get { return _isNavigationChecked; }
            set { SetProperty(ref _isNavigationChecked, value); }
        }

        public InteractionRequest<INotification> ShowAboutFarmMonitorRequest { get; private set; }

        public ICommand ShowAboutFarmMonitorCommand
        {
            get { return new DelegateCommand(ShowAboutFarmMonitorView); }
        }

        public ICommand BackCommand { get; private set; }
        public ICommand ShowApplicationMenuCommand { get; private set; }

        public string StaffName
        {
            get { return _staffName; }
            set { SetProperty(ref _staffName, value); }
        }

        private void Initialize()
        {
            StaffName = _loginService?.CurrentUser == null ?
                "No User Logged in." :
                _loginService.CurrentUser.Name;
        }

        private void ShowAboutFarmMonitorView()
        {
            ShowAboutFarmMonitorRequest.Raise(new Notification
            {
                Title = "About"
            });
        }

        private void ShowApplicationMenu()
        {
            IsApplicationMenuVisible = true;
        }

        private void HideApplicationMenu()
        {
            IsApplicationMenuVisible = false;
        }
    }
}
