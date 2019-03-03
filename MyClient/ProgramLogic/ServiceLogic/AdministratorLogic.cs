using GalaSoft.MvvmLight;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace MyClient.ProgramLogic.ServiceLogic
{

    /// <summary>
    /// Класс предоставляет работу логики администратора с сервером
    /// </summary>

    public class AdministratorLogic
    {
        private MyService.TransferServiceClient client; // Ссылка на клиент
        private DialogService MyDialog; // Для работы с диалогами



        #region Методы для работы с сервером

        // Метод для создания аккаунта
        public bool CreateAccount(MyModelLibrary.accounts NewAccount, MyModelLibrary.accounts MyAccount)
        {
            // Делаем проверку: все ли данные переданы в аккаунте, которые необходимы для создания
            if (NewAccount != null)
            {
                try
                {
                    // Создаем канал связи с бд
                    using (client = new MyService.TransferServiceClient())
                    {

                        // Передаем новый и текущий аккаунт. Если аккаунт создастся на сервере, то выдаст true, иначе false
                        bool AccountCreated = client.AddAccount(NewAccount, MyAccount);

                        if (AccountCreated == true)
                        {
                            MyDialog.ShowMessage($"Аккаунт <<{NewAccount.login}>> успешно создан!\nДата создания: {DateTime.Now}");

                            return true; // Возвращаем true, если аккаунт успешно создан
                        }
                    }
                }
                catch (FaultException<AccountService.AccountConnectedException> ex)
                {
                    MyDialog.ShowMessage(ex.Message);
                }
                catch (Exception ex)
                {
                    MyDialog.ShowMessage(ex.Message);
                }

                return false; //Возвращаем false, если аккаунт не создан
            }

            return false;

        }

        // Метод для получения списка аккаунтов
        public List<MyModelLibrary.accounts> GetAccountsList(MyModelLibrary.accounts MyAcc)
        {
            // Создаем подключение к серверу
            using (client = new MyService.TransferServiceClient())
            {
                return client.GetAllAccountsList(MyAcc);
            }
        }

        // Метод, который получает всех пользователей
        public List<MyModelLibrary.Users> GetUsersList(MyModelLibrary.accounts MyAcc)
        {
            // Создаем подключение к серверу
            using (client = new MyService.TransferServiceClient())
            {
                    return client.GetAllUsersList(MyAcc).ToList(); // Возвращаем список пользователей
            }
        }

        #endregion

        public AdministratorLogic()
        {
            MyDialog = new DialogService();
        }



    }
}
