using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private bool _isNavigationChecked;

        private string _staffName;

        private readonly ILoginService _loginService;

        public ShellViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            _isNavigationChecked = true;

            ShowAboutFarmMonitorRequest = new InteractionRequest<INotification>();

            Initialize();
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
    }
}
