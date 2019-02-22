using System.Windows;

namespace MyProgramJournal_DB.View.MainLoginWindow
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ViewModel(); // Передаем DataContext
        }

    }
}
