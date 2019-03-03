using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;

namespace MyClient.ViewModel.Administrator
{
    public class CreateAccountVM : AdministratorViewModel
    {

        #region Свойства, необходимые для создания аккаунта

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

        #region Команды

        // Команда создания аккаунта
        public ICommand CreateAccount
        {
            get
            {
                return new RelayCommand(() =>
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
                    if (AddTelephone == null)
                        NewAccount.Users.NumberPhone = null;
                    else
                        NewAccount.Users.NumberPhone = AddTelephone;
                    NewAccount.Users.DateOfBirthDay = AddDateOfBirthday;

                    // Создаем аккаунт
                    bool Created = MyAdminLogic.CreateAccount(NewAccount, MyAcc);

                    // Если аккаунт успешно создан, то перейди на главную страницу
                    if (Created == true)
                    {
                        // Перейди в Page главной страницы
                        navigation.Navigate("View/CommonPages/MainPage.xaml");
                    }
                });
            }
        }

        #endregion

        #region Вспомогательные методы

        // Метод который ДеИнициализирует данные (Чтобы они пропали с контекста)
        private void DeInitialization()
        {
            AddLogin = null;
            AddPassword = null;
            AddName = null;
            AddFamily = null;
            AddSurname = null;
            AddGender = null;
            AddStatus = null;
            AddAccountStatus = null;
            AddTelephone = null;
            AddDateOfBirthday = null;
        }

        #endregion



        #region Конструктор

        public CreateAccountVM()
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
            DeInitialization();


            // Инициализируем списки типа string для выбора итемов в ComboBox
            StatusList = new List<string>() { "Студент", "Преподаватель" };
            GenderList = new List<string>() { "Мужчина", "Женщина" };
            StatusAccountsList = new List<string>() { "Активный", "Неактивный" };
        }

        #endregion



    }
}
