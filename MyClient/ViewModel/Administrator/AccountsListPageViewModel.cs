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

        #endregion


        #region Команды

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
