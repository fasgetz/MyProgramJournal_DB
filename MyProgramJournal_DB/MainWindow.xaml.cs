using System;
using System.Windows;

namespace MyProgramJournal_DB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void butt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyService.TransferServiceClient client = new MyService.TransferServiceClient();
                lb.Content = client.login();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
