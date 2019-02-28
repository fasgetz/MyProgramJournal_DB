using MyProgramJournal_DB.MVVM;
using MyProgramJournal_DB.Program_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProgramJournal_DB.ViewModel.Administrator
{
    class AddAcountViewModel : ObservableObject
    {
        #region Свойства

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

        #region Команды
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

                    //// Создаем аккаунт
                    //bool Created = new AdministratorLogic().CreateAccount(NewAccount, MyAcc);

                    //// Если аккаунт создан успешно, то перейди на главную страницу
                    //if (Created == true)
                    //    page_tittle = "CommonPages.MainPage"; // Переходим на главную страницу

                });
            }
        }

        #endregion

        // Конструктор, который проинициализирует данные
        public AddAcountViewModel()
        {
            //page_tittle = "Administrator.AccountsPages.CreateAccountPage";

            // Инициализируем списки типа string для выбора итемов в ComboBox
            StatusList = new List<string>() { "Студент", "Преподаватель" };
            GenderList = new List<string>() { "Мужчина", "Женщина" };
            StatusAccountsList = new List<string>() { "Активный", "Неактивный" };
        }
    }
}
