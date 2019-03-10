using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using MyClient.ViewModel.Administrator.Accounts;

namespace MyClient.ViewModel.Administrator.Accounts
{

    /// <summary>
    /// VM редактирования аккаунта
    /// </summary>


    public class EditAccountVM : AccountsListPageViewModel
    {

        #region Команды

        // Команда, которая изменит данные и отправит новые данные аккаунта на сервер
        public ICommand SaveAccount
        {
            get
            {
                return new RelayCommand(() =>
                {
                    bool edit = MyAdminLogic.EditAccount(SelectedAccount, MyAcc);

                    // Если редактирование прошло успешно, то перейди на страницу списка аккаунтов
                    if (edit == true)
                        navigation.Navigate("View/Administrator/AccountsPages/AccountsListPage.xaml");
                });
            }
        }

        #endregion


        #region Конструктор

        public EditAccountVM()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<List<MyModelLibrary.accounts>>>(this, GetAccount);


            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new DialogService(); // Инициализируем диалоговые окна
            navigation = new NavigateViewModel(); // Для навигации
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private void GetAccount(GenericMessage<List<MyModelLibrary.accounts>> GetAcc)
        {
            MyAcc = GetAcc.Content.ElementAt(0); // Мой аккаунт для манипулирования данными
            SelectedAccount = GetAcc.Content.ElementAt(1); // Выбранный аккаунт

            Initialization(); // Инициализируем свойства
        }

        #endregion

    }
}
