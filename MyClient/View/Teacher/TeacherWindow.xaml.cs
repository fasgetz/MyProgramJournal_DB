using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyClient.View.Teacher
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
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

        // Событие на кнопку закрыть меню
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonMenuOpen.Visibility = Visibility.Visible;
            ButtonMenuClose.Visibility = Visibility.Collapsed;
        }

        // Событие на кнопку показать меню
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonMenuClose.Visibility = Visibility.Visible;
            ButtonMenuOpen.Visibility = Visibility.Collapsed;
        }
    }
}
