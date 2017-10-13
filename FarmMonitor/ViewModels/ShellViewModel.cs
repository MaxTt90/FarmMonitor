using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PresentationModule.Services;
using FarmMonitor.DAL;

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
            
            Initialize();
        }

        public bool IsNavigationChecked
        {
            get { return _isNavigationChecked; }
            set { SetProperty(ref _isNavigationChecked, value); }
        }

        public string StaffName
        {
            get { return _staffName; }
            set { SetProperty(ref _staffName, value); }
        }

        private void Initialize()
        {
                _loginService.CurrentUser.Name;
        }
    }
}
