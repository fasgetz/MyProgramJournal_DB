using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MyClient.ProgramLogic.ServiceLogic;
using MyClient.ViewModel.Administrator.News;
using MyClient.ViewModel.Administrator.Groups;
using MyClient.ViewModel.Administrator.Users;
using MyClient.ViewModel.Administrator.Accounts;

namespace MyClient.ViewModel.Administrator
{

    /// <summary>
    /// ViewModel Администратора
    /// </summary>


    public class AdministratorViewModel : AccountVM
    {

        #region Свойства

        // Вспомогательный метод для инициализации свойств
        protected void Initialization()
        {
            // Инициализируем списки типа string для выбора итемов в ComboBox

            // Статус пользователя с преподавателя на студента и наоборот меняеть не будем (т.к. не логично)
            StatusList = new List<string>() { "Студент", "Преподаватель" };
            GenderList = new List<string>() { "Мужчина", "Женщина" };
            StatusAccountsList = new List<string>() { "Активный", "Неактивный" };

            // Если аккаунт в списке выбрали, то проинициализируй все свойства
            if (SelectedAccount != null)
            {
                AddLogin = SelectedAccount.login;
                AddPassword = SelectedAccount.password;
                AddName = SelectedAccount.Users.Name;
                AddFamily = SelectedAccount.Users.Family;
                AddGender = SelectedAccount.Users.GetGender;
                AddStatus = SelectedAccount.Users.GetStatus;
                AddAccountStatus = SelectedAccount.GetStatus;
                AddDateOfBirthday = SelectedAccount.Users.DateOfBirthDay;
                AddTelephone = SelectedAccount.Users.NumberPhone;
                AddSurname = SelectedAccount.Users.Surname;
            }
        }

        #region Общие свойства новостей

        internal NewsLogic MyNewsLogic; // Логика работы с новостями

        // Заголовок новости
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("title");
            }
        }

        #endregion

        #region Свойства страниц добавления и редактирования аккаунта

        private string _AddName;
        public string AddName
        {
            get
            {
                return _AddName;
            }
            set
            {
                _AddName = value;
                if (SelectedAccount != null)
                    SelectedAccount.Users.Name = value;
                RaisePropertyChanged("AddName");
            }
        }

        private string _AddFamily;
        public string AddFamily
        {
            get
            {
                return _AddFamily;
            }
            set
            {
                _AddFamily = value;
                if (SelectedAccount != null)
                    SelectedAccount.Users.Family = value;
                RaisePropertyChanged("AddFamily");
            }
        }

        private string _AddSurname;
        public string AddSurname
        {
            get
            {
                return _AddSurname;
            }
            set
            {
                _AddSurname = value;
                if (SelectedAccount != null)
                    SelectedAccount.Users.Surname = value;
                RaisePropertyChanged("AddSurname");
            }
        }

        private string _AddGender;
        public string AddGender
        {
            get
            {
                return _AddGender;
            }
            set
            {
                _AddGender = value;
                if (SelectedAccount != null)
                {
                    if (value == "Мужчина")
                        SelectedAccount.Users.Gender = "М";
                    else
                        SelectedAccount.Users.Gender = "Ж";
                }


                RaisePropertyChanged("AddGender");
            }
        }

        private string _AddStatus;
        public string AddStatus
        {
            get
            {
                return _AddStatus;
            }
            set
            {
                _AddStatus = value;
                if (SelectedAccount != null)
                {
                    if (value == "Студент")
                        SelectedAccount.Users.idUserStatus = 1;
                    else if (value == "Преподаватель")
                        SelectedAccount.Users.idUserStatus = 2;
                    else if (value == "Администратор")
                        SelectedAccount.Users.idUserStatus = 3;
                }
                RaisePropertyChanged("AddStatus");
            }
        }

        private string _AddAccountStatus;
        public string AddAccountStatus
        {
            get
            {
                return _AddAccountStatus;
            }
            set
            {
                _AddAccountStatus = value;
                if (SelectedAccount != null)
                {
                    if (value == "Неактивный")
                        SelectedAccount.idStatus = 0;
                    else
                        SelectedAccount.idStatus = 1;
                }

                RaisePropertyChanged("AddAccountStatus");
            }
        }

        private string _AddTelephone;
        public string AddTelephone
        {
            get
            {
                return _AddTelephone;
            }
            set
            {
                _AddTelephone = value;
                if (SelectedAccount != null)
                {
                    SelectedAccount.Users.NumberPhone = value;
                }
                RaisePropertyChanged("AddTelephone");
            }
        }

        private DateTime? _AddDateOfBirthday;
        public DateTime? AddDateOfBirthday
        {
            get
            {
                return _AddDateOfBirthday;
            }
            set
            {
                _AddDateOfBirthday = value;
                if (SelectedAccount != null)
                {
                    SelectedAccount.Users.DateOfBirthDay = value;
                }
                RaisePropertyChanged("AddDateOfBirthday");
            }
        }

        // Для вывода всех гендеров в ComboBox
        private List<string> _GenderList;
        public List<string> GenderList
        {
            get
            {
                return _GenderList;
            }
            set
            {
                _GenderList = value;
                RaisePropertyChanged("GenderList");
            }
        }

        // Для вывода всех статусов пользователя в ComboBox
        private List<string> _StatusList;
        public List<string> StatusList
        {
            get
            {
                return _StatusList;
            }
            set
            {
                _StatusList = value;
                RaisePropertyChanged("StatusList");
            }
        }

        private string _AddLogin;
        public string AddLogin
        {
            get
            {
                return _AddLogin;
            }
            set
            {
                _AddLogin = value;
                if (SelectedAccount != null)
                    SelectedAccount.login = value;                    
                RaisePropertyChanged("AddLogin");
            }
        }

        private string _AddPassword;
        public string AddPassword
        {
            get
            {
                return _AddPassword;
            }
            set
            {
                _AddPassword = value;
                if (SelectedAccount != null)
                    SelectedAccount.password = value;
                RaisePropertyChanged("AddPassword");
            }
        }

        #endregion

        // Список пользователей
        private List<MyModelLibrary.Users> _UsersList;
        public List<MyModelLibrary.Users> UsersList
        {
            get
            {
                return _UsersList;
            }
            set
            {
                _UsersList = value;
                RaisePropertyChanged("UsersList");
            }
        }

        // Выбранный аккаунт в списке
        private MyModelLibrary.accounts _SelectedAccount;
        public MyModelLibrary.accounts SelectedAccount
        {
            get
            {
                return _SelectedAccount;
            }
            set
            {
                _SelectedAccount = value;
                RaisePropertyChanged("SelectedAccount");
            }
        }

        // Выбранный пользователь в списке
        private MyModelLibrary.Users _SelectedUser;
        public MyModelLibrary.Users SelectedUser
        {
            get
            {
                return _SelectedUser;
            }
            set
            {
                _SelectedUser = value;
                if (SelectedUser != null)
                    SelectedAccount = MyAdminLogic.GetAccount(MyAcc, value.idUser);
                RaisePropertyChanged("SelectedUser");
            }
        }

        private DateTime? _DateRegistration;
        public DateTime? DateRegistration
        {
            get
            {
                return _DateRegistration;
            }
            set
            {
                _DateRegistration = value;
                if (SelectedAccount != null)
                {
                    SelectedAccount.DateRegistration = value;
                }
                RaisePropertyChanged("DateRegistration");
            }
        }

        // Для вывода всех статусов аккаунта в ComboBox
        private List<string> _StatusAccountsList;
        public List<string> StatusAccountsList
        {
            get
            {
                return _StatusAccountsList;
            }
            set
            {
                _StatusAccountsList = value;
                RaisePropertyChanged("StatusAccountsList");
            }
        }

        // Список аккаунтов
        private List<MyModelLibrary.accounts> _AccountList;
        public List<MyModelLibrary.accounts> AccountsList
        {
            get
            {
                return _AccountList;
            }
            set
            {
                _AccountList = value;
                RaisePropertyChanged("AccountsList");
            }
        }

        // Для хранения ключа, по которому будем искать в поиске
        private string _SelectedSearchItem;
        public string SelectedSearchItem
        {
            get
            {
                return _SelectedSearchItem;
            }
            set
            {
                _SelectedSearchItem = value;
                text = string.Empty; // Обнуляем текст в строке поиска
                RaisePropertyChanged("SelectedSearchItem");
            }
        }

        // Строка поиска
        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RaisePropertyChanged("text");
            }
        }

        // Для хранения элементов, которые выводятся в Accounts ComboBox
        private List<string> _AccountsCBData;
        public List<string> AccountCBData
        {
            get
            {
                return _AccountsCBData;
            }
            set
            {
                _AccountsCBData = value;
                RaisePropertyChanged("AccountsCBData");
            }
        }

        #endregion

        #region Команды перехода по страницам (Только страницы администратора)

        // Команда перехода на страницу добавления дисциплины учителю
        public ICommand OpenAddTeacherDisciplines
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.TeacherDisciplinesVM == null)
                        locator.TeacherDisciplinesVM = new Disciplines.TeacherDisciplinesViewModel();


                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page страницы дисциплин
                    navigation.Navigate("View/Administrator/Disciplines/TeachersDisciplinesPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на страницу списка дисциплин
        public ICommand OpenDisciplinesList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyDisciplinesListVM == null)
                        locator.MyDisciplinesListVM = new Disciplines.DisciplinesListViewModel();


                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page страницы дисциплин
                    navigation.Navigate("View/Administrator/Disciplines/DisciplineListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на страницу новостей
        public ICommand OpenNewsList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.NewsListVM == null)
                        locator.NewsListVM = new NewsListViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page страницы новостей
                    navigation.Navigate("View/Administrator/News/NewsListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на UsersListPage
        public ICommand OpenUsersList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.UsersListVM == null)
                        locator.UsersListVM = new UsersListPageViewModel();


                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/UsersPages/UsersListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на AccountsListPage
        public ICommand OpenAccountsList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.AccountsListVM == null)
                        locator.AccountsListVM = new AccountsListPageViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/AccountsPages/AccountsListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на CreateAccountPage
        public ICommand OpenCreateAccountPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyCreateAccountVM == null)
                        locator.MyCreateAccountVM = new CreateAccountVM();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/AccountsPages/CreateAccountPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на CreateNewsPage
        public ICommand OpenCreateNewsPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyCreateNewsVM == null)
                        locator.MyCreateNewsVM = new CreateNewsViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/News/AddNewsPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на GroupsListPage
        public ICommand OpenGroups
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyCreateGroupVM == null)
                        locator.MyCreateGroupVM = new CreateGroupViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/Groups/GroupsListPage.xaml");
                    
                });
            }
        }

        #endregion

        #region Конструктор

        // Конструктор класса
        public AdministratorViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            
            locator = new ViewModelLocator(); // Инициализируем локатор VM
            navigation = new NavigateViewModel(); // Инициализируем навигацию
            dialog = new DialogService(); // Для диалогов

            // Задаем текущий Instance для общения клиента с сервером
            MyAccountLogic = new ProgramLogic.ServiceLogic.AccountLogic(new System.ServiceModel.InstanceContext(this));
        }


        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion

    }
}
