using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace MyClient.ViewModel.Administrator.Disciplines
{

    /// <summary>
    /// VM представляет страницу TeachersDisciplinesPage
    /// </summary>

    public class TeacherDisciplinesViewModel : DisciplinesListViewModel
    {


        #region Свойства

        // Список дисциплин, которые можно добавить учителю
        private List<MyModelLibrary.Discipline> _NotAddedDisciplines;
        public List<MyModelLibrary.Discipline> NotAddedDisciplines
        {
            get
            {
                return _NotAddedDisciplines;
            }
            set
            {
                _NotAddedDisciplines = value;
                RaisePropertyChanged("NotAddedDisciplines");
            }
        }

        // Список дисциплин, которые ведет учитель
        private List<MyModelLibrary.Discipline> _TeacherDisciplines;
        public List<MyModelLibrary.Discipline> TeacherDisciplines
        {
            get
            {
                return _TeacherDisciplines;
            }
            set
            {
                _TeacherDisciplines = value;
                RaisePropertyChanged("TeacherDisciplines");
            }
        }

        // Выбранный учитель
        private MyModelLibrary.Users _SelectedTeacher;
        public MyModelLibrary.Users SelectedTeacher
        {
            get
            {
                return _SelectedTeacher;
            }
            set
            {
                _SelectedTeacher = value;

                // Если юзера выбрали, то прогрузи его дисциплины (которые он ведет, и которые можно добавить в качестве ведомых дисциплин)
                if (value != null)
                {
                    // Прогружаем список дисциплин, которые ведет учитель
                    TeacherDisciplines = MyAdminLogic.GetTeacherDisciplines(
                        MyAcc,
                        value,
                        1); // 1 значит, что загружаем все дисциплины, которые ведет преподаватель

                    //  Прогружаем список дисциплин, которые не ведет учитель (Для того, чтобы можно было их добавить)
                    NotAddedDisciplines = MyAdminLogic.GetTeacherDisciplines(
                        MyAcc,
                        value,
                        2); // 2 значит, что загружаем все дисциплины, которые можно добавить преподавателю
                }
                RaisePropertyChanged("SelectedTeacher");
            }
        }

        // Выбранная дисциплина
        private MyModelLibrary.Discipline _SelectedDiscipline;
        public MyModelLibrary.Discipline SelectedDiscipline
        {
            get
            {
                return _SelectedDiscipline;
            }
            set
            {
                _SelectedDiscipline = value;
                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        #endregion

        #region Команды

        // Команда добавления дисциплины преподавателю
        public new ICommand AddDiscipline
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали данные в ComboBoxe's, то можно отправить их на сервер
                    if (SelectedTeacher != null && SelectedDiscipline != null)
                    {
                        // Отправляем данные на сервер
                        bool Added = MyAdminLogic.AddTeacherDiscipline(MyAcc, SelectedTeacher, SelectedDiscipline);

                        // Если дисциплина добавлена успешно, то обнови списки
                        if (Added == true)
                        {
                            // Прогружаем список дисциплин, которые ведет учитель
                            TeacherDisciplines = MyAdminLogic.GetTeacherDisciplines(
                                MyAcc,
                                SelectedTeacher,
                                1); // 1 значит, что загружаем все дисциплины, которые ведет преподаватель

                            //  Прогружаем список дисциплин, которые не ведет учитель (Для того, чтобы можно было их добавить)
                            NotAddedDisciplines = MyAdminLogic.GetTeacherDisciplines(
                                MyAcc,
                                SelectedTeacher,
                                2); // 2 значит, что загружаем все дисциплины, которые можно добавить преподавателю
                        }
                    }
                });
            }
        }

        // Команда фильтрации списка поиска в ComboBoxe's
        public ICommand SearchedInCombobox
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если в поле ComboBox что-то ввели, то произведи поиск
                    if (text != null)
                    {
                        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(UsersList);
                        cv.Filter = s =>
                        {
                            MyModelLibrary.Users u = s as MyModelLibrary.Users;
                            return (u.GetFIO.StartsWith(text));
                        };
                    }

                });
            }
        }

        #endregion

        #region Свойства

        public TeacherDisciplinesViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна            
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            UsersList = MyAdminLogic.GetTeachersList(MyAcc); // Прогружаем список учителей
            TeacherDisciplines = null;
            NotAddedDisciplines = null;            
        }

        #endregion

    }
}
