using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Collections.Generic;
using MyClient.ProgramLogic.ServiceLogic;
using System.Linq;

namespace MyClient.ViewModel.Student.Diary
{

    /// <summary>
    /// VM представляет страницу дневника студента
    /// </summary>

    public class DiaryViewModel : StudentViewModel
    {

        #region Свойства

        // Логика работы сервиса студента с сервером
        StudentLogic studentlogic = new StudentLogic();

        // Итоговая оценка
        private MyModelLibrary.FinalAttendances _FinalAttendance;
        public MyModelLibrary.FinalAttendances FinalAttendance
        {
            get => _FinalAttendance;
            set
            {
                _FinalAttendance = value;
                RaisePropertyChanged("FinalAttendance");
            }
        }

        // Дисциплины группы
        List<MyModelLibrary.GroupDisciplines> _GroupDisciplines;
        public List<MyModelLibrary.GroupDisciplines> GroupDisciplines
        {
            get => _GroupDisciplines;
            set
            {
                _GroupDisciplines = value;
                RaisePropertyChanged("GroupDisciplines");
            }
        }

        // Выбранная дисциплина
        private MyModelLibrary.GroupDisciplines _SelectedDiscipline;
        public MyModelLibrary.GroupDisciplines SelectedDiscipline
        {
            get => _SelectedDiscipline;
            set
            {
                _SelectedDiscipline = value;

                // Если выбрали семестр и дисциплину, то прогрузи занятия и оценки
                if (value != null && SelectedSemestr != null)
                {
                    lessons = studentlogic.GetAttendancesFromJournal(MyAcc, value).OrderBy(i => i.DateLesson).ThenBy(i => i.LessonNumber).ToList();
                    FinalAttendance = studentlogic.GetFinalAttendance(MyAcc, value);
                }                    
                else if (value == null)
                {
                    lessons = null;
                    FinalAttendance = null;
                }
                    

                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        // Список семестров
        private List<int> _semestr;
        public List<int> semestr
        {
            get => _semestr;
            set
            {
                _semestr = value;

                // Если выбрали, то необходимо обнулить свойства
                if (value == null)
                    SelectedDiscipline = null;

                RaisePropertyChanged("semestr");
            }
        }

        // Выбранный семестр
        private int? _SelectedSemestr;
        public int? SelectedSemestr
        {
            get => _SelectedSemestr;
            set
            {
                _SelectedSemestr = value;

                // Прогружаем список дисциплин группы
                if (value != null && value >= 1 && value <= 8)
                    GroupDisciplines = new StudentLogic().GetStudentsDiscipline(MyAcc, value);

                RaisePropertyChanged("SelectedSemestr");
            }
        }

        #endregion

        #region Конструктор

        public DiaryViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            semestr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion

    }
}
