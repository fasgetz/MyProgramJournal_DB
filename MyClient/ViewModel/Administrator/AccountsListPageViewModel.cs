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

namespace MyClient.ViewModel.Administrator
{
    public class AccountsListPageViewModel : AdministratorViewModel
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

        #region Свойства страниц добавления и редактирования аккаунта

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

        #endregion

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

        #endregion


        #region Команды

        // Команда на кнопку в контекстном меню редактирование аккаунта
        public ICommand EditAccount
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если аккаунт в ListBox выбрали (кликнули на него), то можно перейти на страницу редактирования
                    if (SelectedAccount != null)
                    {
                        // Если выбранный аккаунт является администратором, то выдай ошибку
                        if (SelectedAccount.Users.idUserStatus == 3)
                        {
                            dialog.ShowMessage("Выбранный аккаунт администратор!\nВы не можете редактировать данные администратора");
                        }
                        // Иначе, если аккаунт не администратор, то перейди в редактирование
                        else
                        {
                            // Если наша vm = null, то проинициализируй ее
                            if (locator.MyEditAccountVM == null)
                                locator.MyEditAccountVM = new EditAccountVM();

                            // Создаем список, который передадим в следующий контекст (Необходимо передать 2 аккаунта:
                            // Мой аккаунт - аккаунт, который вызвал редактирование (Для логики администратора на следующей странице)
                            // И выбранный SelectedAccount в списке, котроый будем редактировать
                            List<MyModelLibrary.accounts> list = new List<MyModelLibrary.accounts>() { MyAcc, SelectedAccount };


                            // Мессенджер: передай в MyEditAccountVM наш список двух аккаунтов
                            Messenger.Default.Send(new GenericMessage<List<MyModelLibrary.accounts>>(list)); // Отправляем в следующий DataContext аккаунт

                            // Перейди в Page профиля
                            navigation.Navigate("View/Administrator/AccountsPages/EditAccountPage.xaml");
                        }
                    }
                });
            }
        }

        public ICommand SearchAccountCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если введенная строка в поиске не пустая, то выбери по какому параметру фильтровать
                    if (text != null)
                    {
                        ICollectionView cv = CollectionViewSource.GetDefaultView(AccountsList);
                        switch (SelectedSearchItem)
                        {
                            case ("Айди"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.accounts u = o as MyModelLibrary.accounts;
                                    return (Convert.ToString(u.idAccount).StartsWith(text));
                                };
                                break;
                            case ("Логин"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.accounts u = o as MyModelLibrary.accounts;
                                    return (u.login.StartsWith(text));
                                };
                                break;
                            case ("Пароль"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.accounts u = o as MyModelLibrary.accounts;
                                    return (u.password.StartsWith(text));
                                };
                                break;
                            case ("Статус"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.accounts u = o as MyModelLibrary.accounts;
                                    return (u.GetStatus.StartsWith(text));
                                };
                                break;
                            case ("Дата регистрации"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.accounts u = o as MyModelLibrary.accounts;
                                    return (u.GetDataTimeFormat.StartsWith(text));
                                };
                                break;
                            default:
                                break;
                        }

                    }
                });
            }
        }

        #endregion


        #region Конструктор

        public AccountsListPageViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);


            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна            
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;

            AccountsList = MyAdminLogic.GetAccountsList(MyAcc); // Загружаем список анкет с бд

            // Передаем в Data элементы, по которым будем производить поиск
            AccountCBData = new List<string>() { "Айди", "Логин", "Пароль", "Статус", "Дата регистрации" };
        }

        #endregion

      
    }
}
