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
using MyClient.ViewModel.Administrator.Users;

namespace MyClient.ViewModel.Administrator.Accounts
{
    public class AccountsListPageViewModel : AdministratorViewModel
    {

        #region Команды

        // Команда на кнопку в контекстном меню редактирование (В списке аккаунтов и персональных данных)
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

                            // Перейди в Page редактирования аккаунта
                            navigation.Navigate("View/Administrator/AccountsPages/EditAccountPage.xaml");
                        }
                    }
                });
            }
        }

        // Команда на кнопку в контекстном меню на просмотр персональных данных
        public ICommand PersonalData
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если аккаунт в ListBox выбрали (кликнули на него), то можно перейти на страницу просмотра профиля
                    if (SelectedAccount != null)
                    {
                        // Если VM не инициализирована, то проинициализируй
                        if (locator.MyPersonalDataVM == null)
                            locator.MyPersonalDataVM = new PersonalDataViewModel();


                        // Создаем список, который передадим в следующий контекст (Необходимо передать 2 аккаунта:
                        // Мой аккаунт - аккаунт, который вызвал редактирование (Для логики администратора на следующей странице)
                        // И выбранный SelectedAccount в списке, котроый будем редактировать
                        List<MyModelLibrary.accounts> list = new List<MyModelLibrary.accounts>() { MyAcc, SelectedAccount };


                        // Мессенджер: передай в MyEditAccountVM наш список двух аккаунтов
                        Messenger.Default.Send(new GenericMessage<List<MyModelLibrary.accounts>>(list)); // Отправляем в следующий DataContext аккаунт

                        // Перейди в Page просмотря профиля
                        navigation.Navigate("View/Administrator/AccountsPages/UsersDataPge.xaml");
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
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;

            AccountsList = MyAdminLogic.GetAccountsList(MyAcc); // Загружаем список анкет с бд

            // Передаем в Data элементы, по которым будем производить поиск
            AccountCBData = new List<string>() { "Айди", "Логин", "Пароль", "Статус", "Дата регистрации" };
        }

        #endregion

      
    }
}
