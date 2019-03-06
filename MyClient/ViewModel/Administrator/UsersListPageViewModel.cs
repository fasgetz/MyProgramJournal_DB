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
    public class UsersListPageViewModel : AdministratorViewModel
    {

        #region Свойства

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
                RaisePropertyChanged("UsersCBData");
            }
        }

        #endregion


        #region Команды

        public ICommand SearchUsersCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если введенная строка в поиске не пустая, то выбери по какому параметру фильтровать
                    if (text != null)
                    {
                        ICollectionView cv = CollectionViewSource.GetDefaultView(UsersList);
                        switch (SelectedSearchItem)
                        {
                            case ("Айди"):             
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (Convert.ToString(u.idUser).StartsWith(text));
                                };
                                break;
                            case ("Имя"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.Name.StartsWith(text));
                                };
                                break;
                            case ("Фамилия"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.Family.StartsWith(text));
                                };
                                break;
                            case ("Отчество"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.Surname.StartsWith(text));
                                };
                                break;
                            case ("Пол"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.GetGender.StartsWith(text));
                                };
                                break;
                            case ("Статус"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.GetStatus.StartsWith(text));
                                };
                                break;
                            case ("Телефон"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
                                    return (u.GetNumberPhone.StartsWith(text));
                                };
                                break;
                            case ("Дата рождения"):
                                cv.Filter = o =>
                                {
                                    MyModelLibrary.Users u = o as MyModelLibrary.Users;
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

        public UsersListPageViewModel()
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

            UsersList = MyAdminLogic.GetUsersList(MyAcc); // Загружаем список анкет с бд

            // Передаем в Data элементы, по которым будем производить поиск
            UsersCBData = new List<string>() { "Айди", "Имя", "Фамилия", "Отчество", "Пол", "Статус", "Телефон", "Дата рождения" };
        }

        #endregion
    }
}
