using MyProgramJournal_DB.MVVM;
using MyProgramJournal_DB.Program_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyProgramJournal_DB.ViewModel
{

    public class ViewModel : ObservableObject
    {


        #region Свойства

        #region Общие свойства

        MyModelLibrary.accounts MyAcc; // Ссылка на мой аккаунт

        UsersLogic MyUserLogic; // Для работы с пользователями
        private DialogService dialog; // Для работы с диалоговым сервисом  

        private string _page_tittle;
        public string page_tittle // Название страницы
        {
            get
            {
                return _page_tittle;
            }
            set
            {
                _page_tittle = value;
                DisplayPage = dialog.newPage(page_tittle); // Передаем в page название страницы
                RaisePropertyChangedEvent("page_tittle");
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
                this._displayPage = value;
                _displayPage.DataContext = this; // Присваиваем текущий datacontext к page
                RaisePropertyChangedEvent("DisplayPage");
            }
        }



        #endregion

        #region Свойства Users

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


        /// <summary>
        ///  Список свойств, которые представляют персональные данные юзера
        /// </summary>

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

        #region Свойства LoginView 


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

        #region Вспомогательные методы

        // Метод, который инициализирует персональные данные юзера
        private void InitializationUser()
        {
            Name = MyAcc.Users.Name;
            Family = MyAcc.Users.Family;
            Surname = MyAcc.Users.Surname;
            Gender = MyAcc.Users.GetGender;
            NumberPhone = MyAcc.Users.NumberPhone;
            GetDateOfBirthday = MyAcc.Users.GetDataTimeFormat;
        }

        #endregion


        #region Методы для работы с командами

        #region Методы для работы с учетными данными

        // Метод, который должен получать аккаунт в случае успешной авторизации
        private void Authorization_method()
        {
            // Получаем аккаунт в случае, если данные верны
            MyAcc = AccountLogic.AuthorizationUser(login, password);

            // Если учетные данные есть у юзера, то создай сервис для работы с юзерами и проинициаилируй
            if (MyAcc != null && MyAcc.Users != null) 
            {
                MyUserLogic = new UsersLogic(); // Инициализируем логику работы юзера
                InitializationUser(); // Инициализируем данные юзера
            }
        }

        // Метод дисконнекта юзера
        private void DisconnectUser()
        {

            // Если аккаунт не пустой, то можно вызвать метод дисконнекта
            if (MyAcc != null)
            {
                MyAcc = AccountLogic.DisconnectUser(MyAcc); // Дисконнектим юзера                
            }
        }

        #endregion


        #region Методы для открытия страниц

        // Метод открывает передает в page_tittle страницу 
        private void UsersListPage()
        {
            page_tittle = "Administrator.UsersPages.UsersListPage";
            UsersList = MyUserLogic.GetUsersList(MyAcc);
        }

        // Метод передает в page_tittle страницу
        private void ProfilePage()
        {
            page_tittle = "CommonPages.ProfilePage";
            profile_name = $"id {MyAcc.idAccount}) Профиль: {MyAcc.Users.Name} {MyAcc.Users.Family} - {MyAcc.Users.UserStatus.StatusUser}";
        }

        #endregion

        #endregion


        #region Команды 

        #region Команды для перехода по страницам

        // Команда передает страницу UserListPage в Page
        public ICommand OpenUsersListPage
        {
            get
            {
                return new DelegateCommand(UsersListPage);
            }
        }

        // Команда передает страницу ProfilePage в Page
        public ICommand OpenProfilePage
        {
            get
            {
                return new DelegateCommand(ProfilePage);
            }
        }


        #endregion


        #region Общие команды

        public ICommand Disconnect
        {
            get
            {
                return new DelegateCommand(DisconnectUser);
            }
        }

        #endregion


        #region Команды окна авторизации

        // Команда авторизации пользователя
        public ICommand Authorization 
        {
            get
            {
                return new DelegateCommand(Authorization_method);
            }
        }

        #endregion


        #endregion


        // Конструктор
        public ViewModel()
        {
            dialog = new DialogService(); // Инициализируем dialog для работы с окнами      
        }

    }

}
