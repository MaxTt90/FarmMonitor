using System;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Unity;
using PresentationModule.Services;
using System.Windows;
using Elekta.Desktop.GuiComponents.Controls;

namespace FarmMonitor.Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : DialogWindow
    {
        private readonly ILoginService _loginService;

        public LoginView(IUnityContainer container)
        {
            InitializeComponent();
            _loginService = container.Resolve<ILoginService>();
        }

        private void OnOkayClicked(object sender, RoutedEventArgs e)
        {
            ValidateUser();
            e.Handled = true;
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e)
        {
            Close();
            e.Handled = true;
        }

        private void ValidateUser()
        {
            if (_loginService != null)
            {
                var loginUser = _loginService.Login(UserNameTextBox.Text.Trim(), PasswordTextBox.Password.Trim());
                if (loginUser != null)
                {
                    DialogResult = true;
                    Close();
                }
                else
                {
                    //TODO handle invalid login
                    throw new Exception("Invalid login.");
                }
            }
        }
    }
}
