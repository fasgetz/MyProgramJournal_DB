using MyClient.Password;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        // Событие на перемещение формы окна
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // Событие на кнопку свернуть в трей
        private void HideTray_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
