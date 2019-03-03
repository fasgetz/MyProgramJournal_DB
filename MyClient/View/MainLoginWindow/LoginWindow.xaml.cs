using System.Windows;

namespace MyClient.View.MainLoginWindow
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel._VMCommon.AccountVM();
        }

    }
}
