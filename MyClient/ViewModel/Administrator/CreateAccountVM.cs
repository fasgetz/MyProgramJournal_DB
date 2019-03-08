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
    public class CreateAccountVM : AccountsListPageViewModel
    {

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
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            DeInitialization(); // Обнуляем свойства
            Initialization(); // Инициализируем свойства
        }

        #endregion



    }
}
