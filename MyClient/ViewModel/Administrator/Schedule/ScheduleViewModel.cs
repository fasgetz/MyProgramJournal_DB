using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Schedule
{

    /// <summary>
    /// VM представляет страницу SchedulePage
    /// </summary>

    public class ScheduleViewModel : AdministratorViewModel
    {

        #region Свойства

        // Список занятий
        private List<MyModelLibrary.LessonsDate> _lessons;
        public List<MyModelLibrary.LessonsDate> lessons
        {
            get
            {
                return _lessons;
            }
            set
            {
                _lessons = value;
                RaisePropertyChanged("lessons");
            }
        }

        // Выбранное занятие
        private MyModelLibrary.LessonsDate _SelectedLesson;
        public MyModelLibrary.LessonsDate SelectedLesson
        {
            get
            {
                return _SelectedLesson;
            }
            set
            {
                _SelectedLesson = value;
                RaisePropertyChanged("SelectedLesson");
            }
        }

        // Номер занятия, которое выбрали
        private int? _SelectNumber;
        public int? SelectNumber
        {
            get
            {
                return _SelectNumber;
            }
            set
            {
                _SelectNumber = value;
                RaisePropertyChanged("SelectNumber");
            }
        }

        // Список пар (занятий), которые еще не добавлены
        private List<int> _numberslessons;
        public List<int> numberslessons
        {
            get
            {
                return _numberslessons;
            }
            set
            {
                _numberslessons = value;
                RaisePropertyChanged("numberslessons");
            }
        }

        // Выбранная дисциплина
        private MyModelLibrary.GroupDisciplines _SelectedDiscipline;
        public new MyModelLibrary.GroupDisciplines SelectedDiscipline
        {
            get
            {
                return _SelectedDiscipline;
            }
            set
            {
                _SelectedDiscipline = value;

                // Прогружаем ФИО учителя, если value != null
                if (value != null)
                    text = value.TeacherFio;
                else
                    text = string.Empty;

                if (SelectNumber != null)
                    SelectNumber = null;

                RaisePropertyChanged($"SelectedDiscipline");
            }
        }

        // Список дисципин группы в семестре
        private List<MyModelLibrary.GroupDisciplines> _disciplines;
        public List<MyModelLibrary.GroupDisciplines> disciplines
        {
            get
            {
                return _disciplines;
            }
            set
            {
                _disciplines = value;
                RaisePropertyChanged("disciplines");
            }
        }


        // Список семестров
        private List<int> _Semesters;
        public List<int> Semesters
        {
            get
            {
                return _Semesters;
            }
            set
            {
                _Semesters = value;
                RaisePropertyChanged("Semesters");
            }
        }

        private int? _SelectedSemestr;
        public int? SelectedSemestr
        {
            get
            {
                return _SelectedSemestr;
            }
            set
            {
                _SelectedSemestr = value;
                if (value != null && SelectedGroup != null && SelectDate != null)
                {
                    SelectNumber = null;

                    // После выбранного семестра необходимо прогрузить список занятий по дате
                    lessons = MyAdminLogic.GetLessonsOnDate(MyAcc, SelectedGroup, SelectDate, value);

                    // Прогружаем список дисциплин группы
                    disciplines = MyAdminLogic.GetGroupDisciplines(MyAcc, SelectedGroup, SelectedSemestr).ToList();

                    // Прогружаем список занятий, которые еще не добавили
                    numberslessons = MyAdminLogic.GetLessonsNumbers(MyAcc, SelectedGroup, SelectDate, value);
                }
                RaisePropertyChanged($"SelectedSemestr");
            }
        }

        // Свойство выбранной группы
        private MyModelLibrary.Groups _SelectedGroup;
        public new MyModelLibrary.Groups SelectedGroup
        {
            get
            {
                return _SelectedGroup;
            }
            set
            {
                _SelectedGroup = value;

                DeInitialization(); // Сбрасываем свойства, т.к. мы выбрали новую дату

                RaisePropertyChanged("SelectedGroup");
            }
        }

        // Выбранная дата
        private DateTime _SelectDate;
        public DateTime SelectDate
        {
            get
            {
                return _SelectDate;
            }
            set
            {
                _SelectDate = value;

                SelectedGroup = null;
                DeInitialization(); // Сбрасываем свойства, т.к. мы выбрали новую дату

                RaisePropertyChanged($"SelectDate");
            }
        }

        #endregion

        #region Команды

        // Команда на удаление занятия
        public ICommand DeleteLesson
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если занятие выбрали, то отправь его на сервер (Чтобы не вылетал экзепшен лишний раз)
                    if (SelectedLesson != null)
                    {
                        // Отправляем данные на сервер и получаем от него ответ
                        bool DeleteLesson = MyAdminLogic.DeleteLessonGroup(MyAcc, SelectedLesson);

                        // Если занятие удалено (== true), то прогрузи списки
                        if (DeleteLesson == true)
                        {

                            // Прогружаем списки по новой
                            numberslessons = MyAdminLogic.GetLessonsNumbers(MyAcc, SelectedGroup, SelectDate, SelectedSemestr); // Прогружаем номера занятий
                            lessons = MyAdminLogic.GetLessonsOnDate(MyAcc, SelectedGroup, SelectDate, SelectedSemestr); // Список занятий
                        }
                    }
                });
            }
        }

        // Команда на добавление занятия
        public ICommand AddLesson
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Отправляем входные данные на сервер и получаем от него ответ
                    bool AddedLesson = MyAdminLogic.AddLessonGroup(MyAcc, SelectedDiscipline, SelectNumber, SelectDate);
                    
                    // Если занятие добавлено успешно, прогрузи списки и обнули некоторые свойства
                    if (AddedLesson == true)
                    {
                        SelectedDiscipline = null; // Обнуляем свойство выбранной дисциплины
                        SelectNumber = null; // Обнуляем свойство выбранного номера занятия

                        // Прогружаем списки по новой
                        numberslessons = MyAdminLogic.GetLessonsNumbers(MyAcc, SelectedGroup, SelectDate, SelectedSemestr); // Прогружаем номера занятий
                        lessons = MyAdminLogic.GetLessonsOnDate(MyAcc, SelectedGroup, SelectDate, SelectedSemestr); // Список занятий
                    }

                });
            }
        }

        #endregion

        #region Конструктор

        // Вспомогательный метод, который деинициализирует свойства
        private void DeInitialization()
        {
            //SelectedGroup = null;
            SelectedSemestr = null;
            NotAddedDisciplines = null;
            lessons = null;
            NotAddedDisciplines = null;
            SelectedDiscipline = null;
            disciplines = null;
            numberslessons = null;
            SelectNumber = null;
            SelectedLesson = null;
        }

        public ScheduleViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            dialog = new DialogService(); // Для диалогов

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Логика администратора
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            DeInitialization(); // Сбрасываем свойства

            MyAcc = GetAcc.Content;
            groups = MyAdminLogic.GetGroups(); // Получаем список групп
            SelectDate = DateTime.Now;
            Semesters = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

        }

        #endregion

    }
}
