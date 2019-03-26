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
    /// Логика взаимодействия для AttendanceWindow.xaml
    /// </summary>
    public partial class AttendanceWindow : Window
    {
        // Оценка, которую передали с прошлого контекста
        public MyModelLibrary.Attendance MyAttendance;

        // Передаем аккаунт в текущий контекст (Для подтверждения данных на сервере)
        public MyModelLibrary.accounts MyAcc;

        public AttendanceWindow(MyModelLibrary.Attendance Attendance, MyModelLibrary.accounts MyAcc)
        {
            InitializeComponent();

            // Если передали не пустую оценку, то проинициализируй входные данные
            if (Attendance != null)
                Initialization(Attendance, MyAcc);
        }

        // Метод для инициализации входных данных
        private void Initialization(MyModelLibrary.Attendance Attendance, MyModelLibrary.accounts MyAcc)
        {
            this.MyAcc = MyAcc; // Инициализируем аккаунт
            MyAttendance = Attendance; // Инициализируем оценку
            items.ItemsSource = new List<string>() { "1", "2", "3", "4", "5", "Н" }; // Заносим в ComboBoxe's список возможных вариантов оценки
            items.SelectedValue = MyAttendance.Mark; // Заносим в SelectedValue переданную оценку
        }

        // Событие на клик кнопки OK
        private void SetAttendance_Click(object sender, RoutedEventArgs e)
        {
            MyAttendance.Mark = items.SelectedValue.ToString();
            bool editable = new ProgramLogic.ServiceLogic.TeacherLogic().SetAttendance(MyAcc, MyAttendance);

            // Если редактирование успешно, то надо обновить в контексте журнала оценку
            if (editable == true)
                this.DialogResult = true;

        }
    }
}
