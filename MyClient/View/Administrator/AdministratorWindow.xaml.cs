﻿using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._Navigation;
using MyClient.ViewModel.Administrator;
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
    }
}
