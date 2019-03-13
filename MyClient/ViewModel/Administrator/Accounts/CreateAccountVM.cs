using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;

namespace MyClient.ViewModel.Administrator.Accounts
{
    public class CreateAccountVM : AccountsListPageViewModel
    {

        #region Свойства


        #endregion

        #region Команды

        // Команда создания аккаунта
        public ICommand CreateAccount
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Создаем новый аккаунт и инициализируем данные
                    MyModelLibrary.accounts NewAccount = new MyModelLibrary.accounts
                    (idAccountStatus, AddLogin, AddPassword, 
                    new MyModelLibrary.Users(AddName, AddFamily, AddSurname, AddGender, UserStatus, AddTelephone, AddDateOfBirthday));

                    // Создаем аккаунт
                    bool Created = MyAdminLogic.CreateAccount(NewAccount, MyAcc);

                    // Если аккаунт успешно создан, то перейди на главную страницу
                    if (Created == true)
                        navigation.Navigate("View/CommonPages/MainPage.xaml");
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
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            DeInitialization(); // Обнуляем свойства
            Initialization(); // Инициализируем свойства
        }

        #endregion



    }
}
