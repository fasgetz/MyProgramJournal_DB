using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using MyClient.ProgramLogic.ServiceLogic;
using MyClient.ViewModel._Navigation;

namespace MyClient.ViewModel._VMCommon
{

    /// <summary>
    /// Общая ViewModel для всех типов (Администраторов, Учителей, Студентов)
    /// </summary>

    public class AccountVM : ViewModelBase
    {
        #region Свойства       

        #region Свойства навигации и диалогов

        protected ViewModelLocator locator; // Локатор для управления вью моделями
        protected NavigateViewModel navigation; // Навигация
        protected DialogService dialog; // Для диалогов

        #endregion

        #region Свойства для работы с логикой

        internal AdministratorLogic MyAdminLogic;
        internal StudentLogic MyUserLogic;
        protected internal AccountLogic MyAccountLogic;

        #endregion

        #region Учетные данные
        
        // Аккаунт
        protected internal MyModelLibrary.accounts MyAcc; 

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
                RaisePropertyChanged("login");
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
                RaisePropertyChanged("password");
            }
        }

        #endregion

        #region Новости

        // Список новостей
        private ObservableCollection<MyModelLibrary.News> _NewsList;
        public ObservableCollection<MyModelLibrary.News> NewsList
        {
            get
            {
                return _NewsList;
            }
            set
            {
                _NewsList = value;
                RaisePropertyChanged("NewsList");
            }
        }

        // Список картинок
        private ObservableCollection<MyModelLibrary.Images> _ImagesList;
        public ObservableCollection<MyModelLibrary.Images> ImagesList
        {
            get
            {
                return _ImagesList;
            }
            set
            {
                _ImagesList = value;
                RaisePropertyChanged("ImagesList");
            }
        }

        #endregion

        #endregion

        #region Общие команды

        #region Команды для аккаунта

        // Вспомогательный метод для ДеИнициализации свойств
        private void DeInitialization()
        {
            MyAdminLogic = null;
            MyUserLogic = null;
            MyAdminLogic = null;

            MyAcc = null;

            ViewModelLocator.Cleanup();
        }

        // Команда авторизации пользователя
        public ICommand Authorization
        {
            get
            {
                return new RelayCommand(() =>
                {

                    // Создаем канал связи между клиентом и сервером
                    MyAccountLogic = new AccountLogic(new System.ServiceModel.InstanceContext(this));

                    // Получаем аккаунт
                    MyAcc = MyAccountLogic.AuthorizationUser(login, password);

                    // Если учетные данные есть у юзера, то создай сервис для работы с юзерами и проинициаилируй
                    if (MyAcc != null && MyAcc.Users != null)
                    {
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт                            
                    }
                });
            }
        }

        // Команда для своей кнопки закрыть. Нам надо сперва отключить юзера, затем закрыть окно
        public ICommand WinClose
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MyAccountLogic.DisconnectUser(MyAcc); // Дисконнект аккаунта
                    navigation.CloseWindow(); // Закрываем текущее окно                    
                });
            }
        }

        // Команда на дисконнект юзера
        public ICommand Disconnect
        {
            get
            {
                return new RelayCommand(() =>
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
                return new RelayCommand(() =>
                {
                    // Если аккаунт не пустой, то вызываем метод дисконнекта
                    if (MyAcc != null)
                    {
                        MyAccountLogic.DisconnectUser(MyAcc); // Дисконнект аккаунта
                        navigation.NewWindow("MyClient.View.MainLoginWindow.LoginWindow"); // Переход в страницу авторизации
                        DeInitialization();
                    }

                });
            }
        }

        #endregion

        #region Команды для перехода по страницам

        // Команда, которая перейдет на страницу профиля
        public ICommand OpenProfilePage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyProfileVM == null)
                        locator.MyProfileVM = new ProfileVM();

                    // Мессенджер: передай MyProfileVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page профиля
                    navigation.Navigate("View/CommonPages/ProfilePage.xaml");

                });
            }
        }

        // Команда, которая перейдет на MainPage
        public ICommand OpenMainPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyMainPageVM == null)
                        locator.MyMainPageVM = new MainPageVM();

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/CommonPages/MainPage.xaml");
                });
            }
        }

        #endregion

        #endregion


        // Конструктор класса
        public AccountVM()
        {            
            dialog = new DialogService(); // Инициализируем диалоги              
        }

    }
}
