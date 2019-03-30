using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace MyClient.View.Teacher.Journal
{
    /// <summary>
    /// Логика взаимодействия для JournalPage.xaml
    /// </summary>
    /// 

    public partial class JournalPage : Page
    {
        public JournalPage()
        {
            InitializeComponent();
        }

        // Событие на скроллинг
        private void s4_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            s1.ScrollToVerticalOffset(e.VerticalOffset);
            s2.ScrollToVerticalOffset(e.VerticalOffset);
            s3.ScrollToVerticalOffset(e.VerticalOffset);


            //lv1.scr

            //lv1.ScrollIntoView()
        }
    }
}
