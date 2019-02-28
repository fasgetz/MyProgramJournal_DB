using MyProgramJournal_DB.MVVM;
using MyProgramJournal_DB.Program_logic;
using MyProgramJournal_DB.Program_logic.FilterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyProgramJournal_DB.ViewModel
{

    public class ViewModel : ObservableObject
    {


        #region Свойства

        #region Свойства, присущие администратору



        #region Списки, вывода данных

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
                RaisePropertyChangedEvent("UsersList");
            }
        }

        // Временный Список пользователей
        private List<MyModelLibrary.Users> _TempUsersList;
        public List<MyModelLibrary.Users> TempUsersList
        {
            get
            {
                return _TempUsersList;
            }
            set
            {
                _TempUsersList = value;
                RaisePropertyChangedEvent("TempUsersList");
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
                RaisePropertyChangedEvent("AccountsList");
            }
        }

        // Список аккаунтов
        private List<MyModelLibrary.accounts> _TempAccountList;
        public List<MyModelLibrary.accounts> TempAccountList
        {
            get
            {
                return _TempAccountList;
            }
            set
            {
                _TempAccountList = value;
                RaisePropertyChangedEvent("TempAccountList");
            }
        }

        #endregion

        #region Новые свойства для передачи методу создания нового аккаунта

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
                RaisePropertyChangedEvent("AddLogin");
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
                RaisePropertyChangedEvent("AddPassword");
            }
        }

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
                RaisePropertyChangedEvent("AddName");
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
                RaisePropertyChangedEvent("AddFamily");
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
                RaisePropertyChangedEvent("AddSurname");
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
                RaisePropertyChangedEvent("AddGender");
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
                RaisePropertyChangedEvent("AddStatus");
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
                RaisePropertyChangedEvent("AddAccountStatus");
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
                RaisePropertyChangedEvent("AddTelephone");
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
                RaisePropertyChangedEvent("AddDateOfBirthday");
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
                RaisePropertyChangedEvent("GenderList");
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
                RaisePropertyChangedEvent("StatusAccountsList");
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
                RaisePropertyChangedEvent("StatusList");
            }
        }

        #endregion

        #region Свойства для поиска и выбора ключа поиска

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
                RaisePropertyChangedEvent("SelectedSearchItem");
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
                RaisePropertyChangedEvent("text");
            }
        }


        // Для хранения элементов, которые выводятся в Users ComboBox
        private List<string> _UsersCBData;
        public List<string> UsersCBData
        {
            get
            {
                return _UsersCBData;
            }
            set
            {
                _UsersCBData = value;
                RaisePropertyChangedEvent("UsersCBData");
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
                RaisePropertyChangedEvent("AccountsCBData");
            }
        }

        #endregion

        #endregion

        #region Общие свойства

        private DialogService dialog; // Для работы с диалоговым сервисом  

        MyModelLibrary.accounts MyAcc; // Мой аккаунт

        private string _page_tittle;
        public string page_tittle // Название страницы
        {
            get
            {
                return _page_tittle;
            }
            set
            {
                if (value != string.Empty)
                {
                    _page_tittle = value;
                    DisplayPage = dialog.newPage(page_tittle); // Передаем в page название страницы
                    RaisePropertyChangedEvent("page_tittle");
                }


            }
        }

        private Page _displayPage;
        public Page DisplayPage // Страница, которую будем открывать присваиваем page_tittle
        {
            get
            {
                return _displayPage;
            }
            set
            {
                if (value == null)
                {
                    _displayPage = null;
                    page_tittle = string.Empty;
                    RaisePropertyChangedEvent("DisplayPage");
                }
                else
                {
                    this._displayPage = value;
                    _displayPage.DataContext = this; // Присваиваем текущий datacontext к page
                    RaisePropertyChangedEvent("DisplayPage");
                }



            }
        }


        #endregion

        #region Логика

        UsersLogic MyUserLogic; // Для работы с пользователями
        AccountLogic MyAccountLogic; // Для работы с аккаунтами
        AdministratorLogic MyAdminLogic; // Для работы с сервисом администратора        

        #endregion

        #region Персональные свойства пользователя

        // Для хранения названия профиля
        private string _profile_name;
        public string profile_name // Вывод профиля
        {
            get
            {
                return _profile_name;
            }
            set
            {
                _profile_name = value;
                RaisePropertyChangedEvent("profile_name");
            }
        }

        //  Список свойств, которые представляют персональные данные юзера
        private string _Name;
        private string _Family;
        private string _Surname;
        private string _Gender;
        private string _NumberPhone;
        private string _GetDateOfBirthday;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChangedEvent("Name");
            }
        }
        public string Family
        {
            get
            {
                return _Family;
            }
            set
            {
                _Family = value;
                RaisePropertyChangedEvent("Family");
            }
        }
        public string Surname
        {
            get
            {
                return _Surname;
            }
            set
            {
                _Surname = value;
                RaisePropertyChangedEvent("Surname");
            }
        }
        public string Gender
        {
            get
            {
                return _Gender;
            }
            set
            {
                _Gender = value;
                RaisePropertyChangedEvent("Gender");
            }
        }
        public string NumberPhone
        {
            get
            {
                return _NumberPhone;
            }
            set
            {
                _NumberPhone = value;
                RaisePropertyChangedEvent("NumberPhone");
            }
        }
        public string GetDateOfBirthday
        {
            get
            {
                return _GetDateOfBirthday;
            }
            set
            {
                _GetDateOfBirthday = value;
                RaisePropertyChangedEvent("GetDateOfBirthday");
            }
        }


        #endregion

        #region Свойства авторизации 


        private string _login;
        public string login // Логин пользователя
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                RaisePropertyChangedEvent("login");
            }
        }

        private string _password;
        public string password // Пароль пользователя
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChangedEvent("password");
            }
        }

        #endregion


        #endregion



        #region Команды 

        #region Команды администратора

        #region Команды поиска данных в DataGrid

        // Команда для поиска по выбранному ключу SelectedItem
        public ICommand SearchUsersCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    // Если введенная строка в поиске не пустая, то выбери по какому параметру фильтровать
                    if (text != null)
                    {
                        // Если мы выбрали UsersListPage, то выбери метод фильтрации по SelectedSearchItem
                        if (page_tittle == "Administrator.UsersPages.UsersListPage")
                        {
                            switch (SelectedSearchItem)
                            {
                                case ("Айди"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => Convert.ToString(i.idUser).StartsWith(text)).ToList();
                                    break;
                                case ("Имя"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.Name.StartsWith(text)).ToList();
                                    break;
                                case ("Фамилия"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.Family.StartsWith(text)).ToList();
                                    break;
                                case ("Отчество"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.Surname.StartsWith(text)).ToList();
                                    break;
                                case ("Пол"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.GetGender.StartsWith(text)).ToList();
                                    break;
                                case ("Статус"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.GetStatus.StartsWith(text)).ToList();
                                    break;
                                case ("Телефон"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.NumberPhone.StartsWith(text)).ToList();
                                    break;
                                case ("Дата рождения"):
                                    TempUsersList = FilterDataGrid<MyModelLibrary.Users>.GetFilteredList(UsersList, i => i.GetDataTimeFormat.StartsWith(text)).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }


                    }
                });
            }
        }

        // Команда для поиска по выбранному ключу SelectedItem
        public ICommand SearchAccountsCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    // Если введенная строка в поиске не пустая, то выбери по какому параметру фильтровать
                    if (text != null)
                    {
                        // Если мы выбрали AccountsListPage, то выбери метод фильтрации по SelectedSearchItem
                        if (page_tittle == "Administrator.AccountsPages.AccountsListPage")
                        {
                            switch (SelectedSearchItem)
                            {
                                case ("Айди"):
                                    TempAccountList = FilterDataGrid<MyModelLibrary.accounts>.GetFilteredList(AccountsList, i => Convert.ToString(i.idAccount).StartsWith(text)).ToList();
                                    break;
                                case ("Логин"):
                                    TempAccountList = FilterDataGrid<MyModelLibrary.accounts>.GetFilteredList(AccountsList, i => i.login.StartsWith(text)).ToList();
                                    break;
                                case ("Пароль"):
                                    TempAccountList = FilterDataGrid<MyModelLibrary.accounts>.GetFilteredList(AccountsList, i => i.password.StartsWith(text)).ToList();
                                    break;
                                case ("Статус"):
                                    TempAccountList = FilterDataGrid<MyModelLibrary.accounts>.GetFilteredList(AccountsList, i => i.GetStatus.StartsWith(text)).ToList();
                                    break;
                                case ("Дата регистрации"):
                                    TempAccountList = FilterDataGrid<MyModelLibrary.accounts>.GetFilteredList(AccountsList, i => i.GetDataTimeFormat.StartsWith(text)).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                });
            }
        }
        #endregion

        #region Команды для манипуляции данными

        // Команда на создание аккаунта
        public ICommand CreateAccount
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    // Создаем новый аккаунт и инициализируем его данные
                    MyModelLibrary.accounts NewAccount = new MyModelLibrary.accounts();
                    NewAccount.login = AddLogin;
                    NewAccount.password = AddPassword;
                    if (AddAccountStatus == "Активный")
                        NewAccount.idStatus = 1;
                    else if (AddAccountStatus == "Неактивный")
                        NewAccount.idStatus = 0;

                    // Инициализируем теперь анкетные данные
                    NewAccount.Users = new MyModelLibrary.Users();
                    NewAccount.Users.Name = AddName;
                    NewAccount.Users.Family = AddFamily;
                    NewAccount.Users.Surname = AddSurname;
                    if (AddGender == "Мужчина")
                        NewAccount.Users.Gender = "М";
                    else if (AddGender == "Женщина")
                        NewAccount.Users.Gender = "Ж";
                    if (AddStatus == "Студент")
                        NewAccount.Users.idUserStatus = 1;
                    else if (AddStatus == "Преподаватель")
                        NewAccount.Users.idUserStatus = 2;
                    NewAccount.Users.NumberPhone = AddTelephone;
                    NewAccount.Users.DateOfBirthDay = AddDateOfBirthday;

                    // Создаем аккаунт
                    bool Created = MyAdminLogic.CreateAccount(NewAccount, MyAcc);

                    // Если аккаунт создан успешно, то перейди на главную страницу
                    if (Created == true)
                        page_tittle = "CommonPages.MainPage"; // Переходим на главную страницу

                });                
            }
        }

        #endregion

        #endregion

        #region Команды для перехода по страницам

        #region Страницы администратора

        // Команда для открытия страницы CreateAccountPage
        public ICommand OpenCreateAccountPage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    page_tittle = "Administrator.AccountsPages.CreateAccountPage";

                    // Инициализируем списки типа string для выбора итемов в ComboBox
                    StatusList = new List<string>() { "Студент", "Преподаватель" };
                    GenderList = new List<string>() { "Мужчина", "Женщина" };
                    StatusAccountsList = new List<string>() { "Активный", "Неактивный" };
                });
            }

        }

        // Команда для открытия страницы UsersListPage
        public ICommand OpenAccountsListPage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    page_tittle = "Administrator.AccountsPages.AccountsListPage";
                    AccountsList = MyAdminLogic.GetAccountsList(MyAcc);
                    TempAccountList = AccountsList;
                    text = null;

                    // Передаем в Data элементы, по которым будем производить поиск
                    AccountCBData = new List<string>() { "Айди", "Логин", "Пароль", "Статус", "Дата регистрации" };
                });
            }
        }

        // Команда передает страницу UserListPage в Page
        public ICommand OpenUsersListPage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    page_tittle = "Administrator.UsersPages.UsersListPage";
                    UsersList = MyAdminLogic.GetUsersList(MyAcc);
                    TempUsersList = UsersList;
                    text = null;

                    // Передаем в Data элементы, по которым будем производить поиск
                    UsersCBData = new List<string>() { "Айди", "Имя", "Фамилия", "Отчество", "Пол", "Статус", "Телефон", "Дата рождения" };
                });
            }
        }

        #endregion

        #region Общие страницы

        // Команда для откртыия главной страницы MainPage
        public ICommand OpenMainPage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    page_tittle = "CommonPages.MainPage";
                });
            }
        }

        // Команда передает страницу ProfilePage в Page
        public ICommand OpenProfilePage
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    page_tittle = "CommonPages.ProfilePage";
                    profile_name = $"id {MyAcc.idAccount}) Профиль: {MyAcc.Users.Name} {MyAcc.Users.Family} - {MyAcc.Users.UserStatus.StatusUser}";
                    if (MyAcc.Users.idUserStatus == 1 && MyAcc.Users.StudentsGroup != null) // Если пользователь == студент и состоит в группе, то добавь название группы к profile_name
                        profile_name += $" {MyAcc.Users.StudentsGroup.Groups.GroupName}";
                });
            }
        }

        #endregion

        #endregion

        #region Команды окна авторизации

        // Команда авторизации пользователя
        public ICommand Authorization 
        {
            get
            {
                return new DelegateCommand(obj =>
                    {
                        // Если объект для работы с логикой аккаунта пуст, то создай и передай контекст
                        // А также подпишись на события
                        if (MyAccountLogic == null)
                        {
                            // Передаем контекст для работы с Call-Back методами
                            MyAccountLogic = new AccountLogic(new System.ServiceModel.InstanceContext(this));

                            // Таким образом пробрасываем изменившиеся свойства модели во View в Логику
                            // тем самым, у нас есть возможность реализовывать методы, которые будут управлять view, в логике
                            MyAccountLogic.PropertyChanged += (s, e) => { RaisePropertyChangedEvent(e.PropertyName); };
                        }

                        // Получаем аккаунт
                        MyAcc = MyAccountLogic.AuthorizationUser(login, password); 


                        // Если учетные данные есть у юзера, то создай сервис для работы с юзерами и проинициаилируй
                        if (MyAcc != null && MyAcc.Users != null)
                        {
                            MyUserLogic = new UsersLogic(); // Инициализируем логику работы юзера
                            MyUserLogic.PropertyChanged += (s, e) => { RaisePropertyChangedEvent(e.PropertyName); }; // Пробрасываем изменения в логику

                            MyAdminLogic = new AdministratorLogic(); // Инициализируем административную логику
                            MyAdminLogic.PropertyChanged += (s, e) => { RaisePropertyChangedEvent(e.PropertyName); }; // Пробрасываем изменения в логику

                            page_tittle = "CommonPages.MainPage"; // Открываем главную страницу

                            // Инициализируем свойства, по данным аккаунта
                            Name = MyAcc.Users.Name;
                            Family = MyAcc.Users.Family;
                            Surname = MyAcc.Users.Surname;
                            Gender = MyAcc.Users.GetGender;
                            NumberPhone = MyAcc.Users.NumberPhone;
                            GetDateOfBirthday = MyAcc.Users.GetDataTimeFormat;
                        }
                    });
            }
        }

        // Команда на кнопку закрыть окно (В меню), т.к. мы закрываем меню без встроенных функций 
        public ICommand WinClose
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    MyAccountLogic.DisconnectUser(MyAcc); // Дисконнект аккаунта
                    dialog.CloseWindow(); // Закрываем окно
                });
            }
        }

        // Команда на кнопку закрыть
        public ICommand Disconnect
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    MyAccountLogic.DisconnectUser(MyAcc); // Дисконнект аккаунта
                });
            }
        }

        // Комманда для смены юзера
        public ICommand ChangeUser
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    // Если аккаунт не пустой, то вызываем метод дисконнекта
                    if (MyAcc != null)
                    {
                        MyAccountLogic.DisconnectUser(MyAcc); // Дисконнект аккаунта
                        dialog.newWindow("MainLoginWindow.LoginWindow"); // Переходим в окно авторизации

                        // Далее обнуляем ВСЕ свойства, т.к. они не должны быть в кеше, при авторизации
                        MyAcc = null;
                        login = null;
                        password = null;
                        Name = null;
                        Family = null;
                        Surname = null;
                        Gender = null;
                        NumberPhone = null;
                        GetDateOfBirthday = null;
                        SelectedSearchItem = null;
                        text = null;
                        UsersCBData = null;
                        MyUserLogic = null;
                        MyAccountLogic = null;
                        MyAdminLogic = null;
                        UsersList = null;
                        TempUsersList = null;
                        AccountsList = null;
                    }


                });
            }
        }
        #endregion

        #endregion


        // Конструктор
        public ViewModel()
        {
            dialog = new DialogService(); // Инициализируем dialog при создании vm для работы с окнами                 
        }

    }

}
