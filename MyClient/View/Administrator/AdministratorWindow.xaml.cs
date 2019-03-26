using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._Navigation;
using MyClient.ViewModel.Administrator;
using System;
using System.Windows;
using System.Windows.Input;

namespace MyClient.View.Administrator
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        public AdministratorWindow()
        {
            InitializeComponent();
            DataContext = new AdministratorViewModel();
            NavigationSetup(); // Для того, чтобы можно было делать переход по страницам            
        }

        void NavigationSetup()
        {
            Messenger.Default.Register<NavigateArgs>(this, (x) =>
            {
                frame.Navigate(new Uri(x.Url, UriKind.Relative));
            });
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
