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

    class ViewModel : ObservableObject
    {
        

        #region Свойства

        #region Общие свойства

        MyModelLibrary.accounts MyAcc; // Ссылка на мой аккаунт

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
                DisplayPage = dialog.newPage(page_tittle); // Присваиваем название страницы page, которую будем открывать после того как изменилось page_tittle
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
                if (Equals(_displayPage, value))
                {
                    return;
                }
                this._displayPage = value;
                _displayPage.DataContext = this; // Присваиваем текущий datacontext к page
                RaisePropertyChangedEvent("DisplayPage");
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



        #region Методы для работы с командами

        // Метод, который должен получать аккаунт в случае успешной авторизации
        private void Authorization_method()
        {
            // Получаем аккаунт в случае, если данные верны
            MyAcc = AccountLogic.AuthorizationUser(login, password);
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


        #region Методы для открытия страниц

        // Метод открывает передает в page_tittle страницу 
        private void UsersListPage()
        {
            page_tittle = "Administrator.UsersListPage";
        }

        #endregion

        #endregion


        #region Команды 

        #region Команды для перехода по страницам

        // Команда передает страницу в Page
        public ICommand OpenUsersListPage
        {
            get
            {
                return new DelegateCommand(UsersListPage);
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
