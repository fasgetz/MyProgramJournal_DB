using MyClient.Password;
using System.Windows;
using System.Windows.Controls;

namespace MyClient.View.MainLoginWindow
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IHavePassword
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel._VMCommon.AccountVM();            
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }
    }
}
