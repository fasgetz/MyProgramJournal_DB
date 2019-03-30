using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Linq;
namespace MyClient.ViewModel.Teacher.Journal
{

    /// <summary>
    /// VM представляет страницу журнала выбранной группы
    /// </summary>

    public class JournalViewModel : GroupsJournalViewModel
    {

        #region Свойства

        // Итоговые отметки
        private List<MyModelLibrary.FinalAttendances> _FinalAttendances;
        public List<MyModelLibrary.FinalAttendances> FinalAttendances
        {
            get => _FinalAttendances;
            set
            {
                _FinalAttendances = value;
                RaisePropertyChanged("FinalAttendances");
            }
        }

        // Итоговая оценка
        private MyModelLibrary.FinalAttendances _SelectFinalAttendance;
        public MyModelLibrary.FinalAttendances SelectFinalAttendance
        {
            get => _SelectFinalAttendance;
            set
            {
                _SelectFinalAttendance = value;

                // Если выбрали значение, то открой окно выстановления итоговой оценки
                if (value != null)
                {
                    // Сформировать список всех выставленных оценок и передать средне арифметическое значение
                    int sum = 0; // Сумма всех оценок (По которым будет вычислять среднее арифметическое)
                    int marks = 0; // Количество нормальных оценок, по которым будет вычитываться среднее арифметическое число
                    int n_marks = 0; // Количество Н-ок
                    double avg; // Среднее арифметическое число

                    // Перебираем весь список занятий
                    foreach (var item in lessons)
                    {
                        // Получаем оценку
                        var mark = item.Attendance.FirstOrDefault(i => i.StudentId == value.idStudent);
                        int number;

                        // Если преобразование успешно, то добавь итем в список оценок
                        if (int.TryParse(mark.Mark, out number))
                        {
                            marks++; // Инкрементируем количество оценок
                            sum += number; // Сумма оценок
                        }
                        // Иначе, в список 
                        else
                            n_marks++; // Инкрементируем количество Н-ок
                    }

                    avg = (double)sum / marks; // Получаем среднее арифметическое число

                    // После того, как все свойства сформированы, открой окно с выставлением финальных оценок
                    if (value != null)
                        if (new View.Teacher.Journal.FinalAttendanceWindow(MyAcc, value, avg, marks, n_marks).ShowDialog() == true)
                            FinalAttendances = new ProgramLogic.ServiceLogic.TeacherLogic().GetFinalAttendances(MyAcc, SelectedGroup); // Получаем список итоговых оценок
                }

                RaisePropertyChanged("SelectFinalAttendance");
            }
        }

        // Выбранная оценка
        private MyModelLibrary.Attendance _select;
        public MyModelLibrary.Attendance select
        {
            get
            {
                return _select;
            }
            set
            {
                _select = value;

                // Если выбрали оценку, то открой окно с выбором оценки
                if (value != null)
                    if (new View.Teacher.Journal.AttendanceWindow(value, MyAcc).ShowDialog() == true)
                        lessons = new ProgramLogic.ServiceLogic.TeacherLogic().GetAttendancesFromJournal(MyAcc, SelectedGroup);

                RaisePropertyChanged("select");
            }
        }

        // Студенты
        private List<MyModelLibrary.Users> _Students;
        public List<MyModelLibrary.Users> Students
        {
            get
            {
                return _Students;
            }
            set
            {
                _Students = value;
                RaisePropertyChanged("Students");
            }
        }

        #endregion


        #region Команды



        #endregion


        #region Конструктор

        public JournalViewModel()
        {

            // Получаем выбранную группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.GroupDisciplines>>(this, GetActivity);

            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует деятельность учителя из прошлого vm при создании текущей vm
        private void GetActivity(GenericMessage<MyModelLibrary.GroupDisciplines> GetActivityes)
        {
            SelectedGroup = GetActivityes.Content;

            lessons = new ProgramLogic.ServiceLogic.TeacherLogic().GetAttendancesFromJournal(MyAcc, SelectedGroup);

            // Получает список студентов
            Students = new ProgramLogic.ServiceLogic.CommonLogic().GetStudentsInGroup(MyAcc, SelectedGroup.IdGroup).OrderBy(i => i.StudentsGroup.NumberInJournal).ToList();

            FinalAttendances = new ProgramLogic.ServiceLogic.TeacherLogic().GetFinalAttendances(MyAcc, SelectedGroup); // Прогружаем список итоговых оценок

        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion
    }
}
