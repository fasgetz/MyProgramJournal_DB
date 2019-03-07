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

        // Метод для получения аккаунта по айди
        public MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int GetIdAcc)
        {
            // Создаем канал связи между клиентом и сервером
            using (client = new MyService.TransferServiceClient())
            {
                try
                {
                    // Возвращаем аккаунт
                    return client.GetAccount(MyAcc, GetIdAcc);
                }
                catch(Exception ex)
                {
                    MyDialog.ShowMessage(ex.Message);
                }
            }

            return null; // Если неудачно, то верни null
        }

        // Метод, который редактирует аккаунт
        public bool EditAccount(MyModelLibrary.accounts EditableAccount, MyModelLibrary.accounts MyAccount)
        {
            // Создаем подключение к серверу
            using (client = new MyService.TransferServiceClient())
            {
                try
                {
                    // Если edit = true, то редактирование успешно, иначе не успешно
                    bool edit = client.EditAccount(MyAccount, EditableAccount);

                    // Если редактирование прошло успешно, то верни true
                    if (edit == true)
                    {
                        MyDialog.ShowMessage("Редактирование прошло успешно!");
                        return true;
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

            }

            return false; // Верни false, если редактирование не удалось
        }

        #endregion

        public AdministratorLogic()
        {
            MyDialog = new DialogService();
        }



    }
}
