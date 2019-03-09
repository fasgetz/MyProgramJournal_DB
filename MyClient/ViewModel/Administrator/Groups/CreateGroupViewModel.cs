using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM страницы создания группы
    /// </summary>

    public class CreateGroupViewModel : AdministratorViewModel
    {

        #region Свойства

        // Выбранная группу
        private MyModelLibrary.Groups _SelectedGroup;
        public MyModelLibrary.Groups SelectedGroup
        {
            get
            {
                return _SelectedGroup;
            }
            set
            {
                _SelectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }

        // Список кодов специальностей
        private List<MyModelLibrary.Speciality_codes> _speciality_list;
        public List<MyModelLibrary.Speciality_codes> speciality_list
        {
            get
            {
                return _speciality_list;
            }
            set
            {
                _speciality_list = value;
                RaisePropertyChanged("speciality_list");
            }
        }

        // Список существующих групп
        private List<MyModelLibrary.Groups> _groups;
        public List<MyModelLibrary.Groups> groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                RaisePropertyChanged("groups");
            }
        }

        private MyModelLibrary.Speciality_codes _SelectedSpeciality;
        public MyModelLibrary.Speciality_codes SelectedSpeciality
        {
            get
            {
                return _SelectedSpeciality;
            }
            set
            {
                _SelectedSpeciality = value;
                RaisePropertyChanged("SelectedSpeciality");
            }
        }

        // Название группы
        private string _GroupName;
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {                
                _GroupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        #endregion

        #region Команды

        // Команда на открытие страницы списка студентов выбранной группы
        public ICommand OpenStudentsPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали группу в списке групп, то открой страницу студентов выбранной группы
                    if (SelectedGroup != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.MyStudentsGroupVM == null)
                            locator.MyStudentsGroupVM = new StudentsGroupViewModel();

                        // Инициализируем группу
                        SelectedGroup = new MyModelLibrary.Groups(SelectedGroup.idGroup, SelectedGroup.GroupName, Convert.ToInt16(SelectedGroup.idSpeciality));

                        // Мессенджер: передай в StudentsGroupPage наш MyAcc и SelectedGroup
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.Groups>(SelectedGroup)); // Отправляем в следующий DataContext группу

                        // Перейди в Page главной страницы
                        navigation.Navigate("View/Administrator/Groups/StudentsGroupPage.xaml");
                    }
                });
            }
        }

        // Команда на создание группы
        public ICommand AddGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (GroupName != string.Empty && SelectedSpeciality != null)
                    {
                        // Создаем группу
                        bool GroupCreated = MyAdminLogic.CreateGroup(MyAcc, new MyModelLibrary.Groups(GroupName, SelectedSpeciality.idSpeciality));

                        // Если группа создана, то обнули свойства
                        if (GroupCreated == true)
                        {
                            DeInitialization();
                            groups = MyAdminLogic.GetGroups();
                        }                            
                    }
                    else
                    {
                        dialog.ShowMessage("Заполните все данные!");
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        // Метод для де инициализации свойств
        private void DeInitialization()
        {
            SelectedSpeciality = null;
            GroupName = string.Empty;
        }

        public CreateGroupViewModel()
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
            speciality_list = MyAdminLogic.GetSpecialityCodes(); // Загрузить список специальностей
            groups = MyAdminLogic.GetGroups(); // Загрузить список групп
            DeInitialization();
        }

        #endregion

    }
}
