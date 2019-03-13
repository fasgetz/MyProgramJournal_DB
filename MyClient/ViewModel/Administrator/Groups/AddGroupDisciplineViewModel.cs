using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;
using MyClient.ViewModel.Administrator.Users;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу AddGroupDisciplinesPage
    /// </summary>

    public class AddGroupDisciplineViewModel : CreateGroupViewModel
    {

        #region Команды

        public ICommand AddGroupDiscipline
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Отправляем данные на сервер
                    bool added = MyAdminLogic.AddDisciplineGroup(MyAcc, SelectedGroup, SelectedTeacher, SelectedDiscipline, SelectedSemestr);

                    // Если группа добавлена успешно, то обнови список
                    if (added == true)
                    {
                        // Список дисциплин группы в семестре
                        GroupDisciplines = MyAdminLogic.GetGroupDisciplines(MyAcc, SelectedGroup, SelectedSemestr);

                        // Список дисциплин, которые можно добавить в семестре (То есть которые еще не добавлены)
                        NotAddedDisciplines = MyAdminLogic.GetNotAddedGroupDisciplines(MyAcc, SelectedGroup, Convert.ToByte(SelectedSemestr));

                        SelectedDiscipline = null;                        
                        SelectedTeacher = null;
                        TeachersFromDiscipline = null;
                    }
                });
            }
        }

        #endregion

        #region Свойства

        // Выбранный учитель
        private MyModelLibrary.Users _SelectedTeacher;
        public new MyModelLibrary.Users SelectedTeacher
        {
            get
            {
                return _SelectedTeacher;
            }
            set
            {
                _SelectedTeacher = value;
                RaisePropertyChanged("SelectedTeacher");
            }
        }

        // Выбранная дисциплина(Необходимо прогрузить список учителей, которые ведут эту дисциплину)
        private MyModelLibrary.Discipline _SelectedDiscipline;       
        public new MyModelLibrary.Discipline SelectedDiscipline
        {
            get
            {
                return _SelectedDiscipline;
            }
            set
            {
                _SelectedDiscipline = value;

                // Вызвать метод, который прогрузит список учителей, которые ведут эту дисциплину
                if (value != null)
                    TeachersFromDiscipline = MyAdminLogic.GetUsersFromDiscipline(MyAcc, value);
                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        // Список учителей, которые ведут выбранную дисциплину
        private List<MyModelLibrary.Users> _TeachersFromDiscipline;
        public List<MyModelLibrary.Users> TeachersFromDiscipline
        {
            get
            {
                return _TeachersFromDiscipline;
            }
            set
            {
                _TeachersFromDiscipline = value;
                RaisePropertyChanged("TeachersFromDiscipline");
            }
        }

        // Список дисциплин учителя
        private List<MyModelLibrary.GroupDisciplines> _GroupDisciplines;
        public List<MyModelLibrary.GroupDisciplines> GroupDisciplines
        {
            get
            {
                return _GroupDisciplines;
            }
            set
            {
                _GroupDisciplines = value;                
                RaisePropertyChanged("GroupDisciplines");
            }
        }

        // Свойство выбранный семестр
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
                
                // Если выбрали семестр, то прогрузи данные по нему
                if (value != null)
                {
                    SelectedTeacher = null;

                    // Список дисциплин группы в семестре
                    GroupDisciplines = MyAdminLogic.GetGroupDisciplines(MyAcc, SelectedGroup, value);

                    // Список дисциплин, которые можно добавить в семестре (То есть которые еще не добавлены)
                    NotAddedDisciplines = MyAdminLogic.GetNotAddedGroupDisciplines(MyAcc, SelectedGroup, Convert.ToByte(value));
                }
                RaisePropertyChanged("SelectedSemestr");
            }
        }

        // Семестры
        private List<int?> _Semesters;
        public List<int?> Semester
        {
            get
            {
                return _Semesters;
            }
            set
            {
                _Semesters = value;
                RaisePropertyChanged("Semester");
            }
        }


        #endregion

        #region Конструктор

        public AddGroupDisciplineViewModel()
        {
            // Получаем аккаунт и группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.Groups>>(this, GetGroup);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new DialogService(); // Инициализируем диалоговые окна  
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content;
        }

        // Вспомогательный метод для мессенджера, который проинициализирует выбранную группу из прошлого vm при создании текущей vm
        private void GetGroup(GenericMessage<MyModelLibrary.Groups> GetGroup)
        {
            // Инициализируем свойства
            SelectedGroup = GetGroup.Content;
            MyGroupName = $"Дисциплины группы {SelectedGroup.GroupName}";
            SelectedSemestr = null;
            SelectedDiscipline = null;
            TeachersFromDiscipline = null;
            GroupDisciplines = null;
            NotAddedDisciplines = null;
            SelectedTeacher = null;

            Semester = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8 }; // Инициализируем список семестров            
        }

        #endregion

    }
}
