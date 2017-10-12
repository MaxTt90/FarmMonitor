﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMonitor.Desktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private bool _isNavigationChecked;

        public ShellViewModel()
        {
            _isNavigationChecked = true;
        }

        public bool IsNavigationChecked
        {
            get { return _isNavigationChecked; }
            set { SetProperty(ref _isNavigationChecked, value); }
        }
    }
}
