using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Unity;
using PresentationModule.Services;
using System.Windows;

namespace FarmMonitor.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        private ILoginService _loginService;

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

        private async void ValidateUser()
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
                    var metroDialogSettings = new MetroDialogSettings
                            {
                                MaximumBodyHeight = 70,
                                DialogMessageFontSize = 12,
                                DialogTitleFontSize = 20
                            };

                    await this.ShowMessageAsync("Error", "Invalid user name and password!", MessageDialogStyle.Affirmative, metroDialogSettings);
                }
            }
        }
    }
}
