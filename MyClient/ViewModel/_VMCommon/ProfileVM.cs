using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using MyClient.ViewModel._Navigation;
using MyClient.ViewModel.Administrator;
using System.Windows.Input;

namespace MyClient.ViewModel._VMCommon
{

    /// <summary>
    /// VM, которая предоставляет данные о профиле пользователя
    /// </summary>

    public class ProfileVM : AccountVM
    {
        #region Свойства

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
                RaisePropertyChanged("profile_name");
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
                RaisePropertyChanged("Name");
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
                RaisePropertyChanged("Family");
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
                RaisePropertyChanged("Surname");
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
                RaisePropertyChanged("Gender");
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
                RaisePropertyChanged("NumberPhone");
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
                RaisePropertyChanged("GetDateOfBirthday");
            }
        }

        #endregion

        #region Команды

        public ICommand ChangePassword
        {
            get
            {
                return new RelayCommand(() =>
                {


                });
            }
        }

        #endregion



        // Конструктор должен проинициализировать свойства
        public ProfileVM()
        {
            // Инициализируем диалог
            dialog = new DialogService();

            // Регистрируем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        public void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            login = MyAcc.login;
            password = MyAcc.password;
            Name = MyAcc.Users.Name;
            Family = MyAcc.Users.Family;
            Surname = MyAcc.Users.Surname;
            Gender = MyAcc.Users.GetGender;
            NumberPhone = MyAcc.Users.NumberPhone;
            GetDateOfBirthday = MyAcc.Users.GetDataTimeFormat;
            profile_name = $"id {MyAcc.idAccount}) Профиль: {MyAcc.Users.Name} {MyAcc.Users.Family} - {MyAcc.Users.UserStatus.StatusUser}";
            if (MyAcc.Users.idUserStatus == 1 && MyAcc.Users.StudentsGroup != null) // Если пользователь == студент и состоит в группе, то добавь название группы к profile_name
                profile_name += $" {MyAcc.Users.StudentsGroup.Groups.GroupName}";
        }

    }
}
