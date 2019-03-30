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

namespace MyClient.View.Teacher.Journal
{
    /// <summary>
    /// Логика взаимодействия для FinalAttendanceWindow.xaml
    /// </summary>
    public partial class FinalAttendanceWindow : Window
    {
        // Свойства
        public MyModelLibrary.accounts MyAcc; // Мой аккаунт
        public MyModelLibrary.FinalAttendances Attendance; // Оценка

        public FinalAttendanceWindow(MyModelLibrary.accounts MyAcc, MyModelLibrary.FinalAttendances attendance, double AVG, int count, int n_count)
        {
            InitializeComponent();

            // Инициализируем свойства после загрузки компонентов окна
            Initialization(MyAcc, attendance, AVG, count, n_count);
        }

        // Метод для инициализации
        private void Initialization(MyModelLibrary.accounts MyAcc, MyModelLibrary.FinalAttendances attendance, double AVG, int count, int n_count)
        {
            items.ItemsSource = new List<string>() { "1", "2", "3", "4", "5" }; // Заносим в ComboBoxe's список возможных вариантов оценки
            this.MyAcc = MyAcc; // Получаем переданный аккаунт
            this.Attendance = attendance; // Инициализируем оценку

            // Инициализируем свойства лейблов
            CountLabel.Content = count;
            NCountLabel.Content = n_count;
            AVGCountLabel.Content = AVG.ToString("0.00");
        }

        // Событие на клик кнопки OK
        private void SetAttendance_Click(object sender, RoutedEventArgs e)
        {
            Attendance.Mark = Convert.ToInt16(items.SelectedValue); // Получаем оценку, которую выбрали
            bool editable = new ProgramLogic.ServiceLogic.TeacherLogic().SetFinalAttendance(MyAcc, Attendance); // Ставим оценку

            // Если редактирование успешно, то надо передать в прошлый контекст, что действие выполнено успешно
            if (editable == true)
                this.DialogResult = true; // Возвращаем true
        }
    }
}
