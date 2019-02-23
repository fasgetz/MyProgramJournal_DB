using MyProgramJournal_DB.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyProgramJournal_DB.Program_logic
{
    /// <summary>
    /// Класс предназначен для организации логики работы с аккаунтом
    /// </summary>
    
    public class AccountLogic
    {


        private static MyService.TransferServiceClient client; // Ссылка на клиент
        private static DialogService MyDialog;


        // Метод для дисконнекта юзера
        public static MyModelLibrary.accounts DisconnectUser(MyModelLibrary.accounts MyAcc)
        {
            try
            {
                client = new MyService.TransferServiceClient(); // Создаем канал связи
                client.DisconnectUser(MyAcc.idAccount); // Отправляем на сервер айди аккаунта, которого отключаем
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return null; // Возвращаем пустой аккаунт в случае дисконнекта юзера

        }

        // Метод, который открывает окна и диалоги, в зависимости от типа аккаунта
        private static void OpenedWindow(MyModelLibrary.accounts MyAcc)
        {
            if (MyAcc != null && MyAcc.idStatus == 0) // Иначе, если аккаунт найден, но он неактивный, то выдай ему ошибку об этом
            {
                MyDialog.ShowMessage("Ваш аккаунт неактивный!");
            }
            // Если аккаунт не пустой и для него завели учетные данные и он активный, то перейди
            else if (MyAcc != null && MyAcc.idStatus != 0)
            {

                


                // Если учетных данных юзеру не завели, то выдай об этом
                if (MyAcc.Users == null)
                {
                    MyDialog.ShowMessage("Вам еще не завели учетных данных");
                }
                // Иначе, если у юзера есть учетные данные, то открой соответствующее окно
                else
                {
                    MyDialog.ShowMessage("Вы успешно авторизировались!");

                    if (MyAcc.Users.idUserStatus == 1) // Если статус юзера == студент, то открой соответствующее окно
                    {
                        MyDialog.ShowMessage("Авторизовался студент!");
                    }
                    else if (MyAcc.Users.idUserStatus == 2) // Если статус юзера == преподаватель, то открой соответствующее окно
                    {
                        MyDialog.ShowMessage("Авторизовался преподаватель!");
                    }
                    else if (MyAcc.Users.idUserStatus == 3) // Если статус юзера == администратор, то открой соответствующее окно
                    {
                        MyDialog.newWindow("Administrator.AdministratorWindow");
                    }

                }
            }
            else if (MyAcc == null) // Иначе, если аккаунт пустой, то выдай ошибку
            {
                MyDialog.ShowMessage("Аккаунт не найден!");
            }
        }

        public static MyModelLibrary.accounts AuthorizationUser(string login, string password) // Метод для авторизации юзера
        {
            // Делаем канал связи между клиентом и сервером
            client = new MyService.TransferServiceClient();
            MyDialog = new DialogService(); // Инициализируем диалог для работы с диалогами

            // Если хост запущен, то выведи об этом ошибку
            try
            {
                MyModelLibrary.accounts MyAcc = client.GetAccounts(login, password); // Получаем аккаунт
                OpenedWindow(MyAcc); // Открываем окно
                return MyAcc;
            }
            catch(System.ServiceModel.EndpointNotFoundException)
            {
                MyDialog.ShowMessage("Сервер не запущен!");
            }
            catch (FaultException<MyService.AccountConnectedException>)
            {
                MyDialog.ShowMessage($"Аккаунт уже подключен!");
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }


            return null;
        }
    }
}
