using MyProgramJournal_DB.MVVM;
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




        private void gona()
        {
            MyService.TransferServiceClient client = new MyService.TransferServiceClient();
            client.login();

        }


        #region Команды 

        #region Команды окна авторизации

        // Команда авторизации пользователя
        public ICommand Authorization 
        {
            get
            {
                return new DelegateCommand(gona);
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
