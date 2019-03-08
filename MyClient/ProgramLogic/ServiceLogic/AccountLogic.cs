using MyClient.AccountService;
using MyClient.ProgramLogic.DialogServices;
using MyClient.ViewModel._Navigation;
using System;
using System.ServiceModel;

namespace MyClient.ProgramLogic.ServiceLogic
{
    /// <summary>
    /// Класс предоставляет логику работы аккаунта с сервером
    /// </summary>

    public class AccountLogic : IMyServiceAccountCallback
    {
        public AccountLogic(InstanceContext MyContext)
        {
            AccountProxy = new MyServiceAccountClient(new System.ServiceModel.InstanceContext(this)); // Передаем текущий контекст
            MyDialog = new DialogService(); // Инициализируем диалог для работы с диалогами
        }


        #region Свойства

        private AccountService.MyServiceAccountClient AccountProxy; // Ссылка на прокси для работы с аккаунтами
        private DialogService MyDialog; // Для работы с диалогом
        private NavigateViewModel navigation; // Для навигации

        #endregion

        #region Методы авторизации

        // Метод для дисконнекта юзера
        public void DisconnectUser(MyModelLibrary.accounts MyAcc)
        {
            try
            {
                AccountProxy.DisconnectUser(MyAcc); // Отправляем на сервер аккаунт, который отключаем
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }



        }

        // Метод, который открывает окна и диалоги, в зависимости от типа аккаунта
        private void OpenedWindow(MyModelLibrary.accounts MyAcc)
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


                    navigation = new NavigateViewModel();
                    // Выбираем какую страницу открывать по статусу юзера
                    switch (MyAcc.Users.idUserStatus)
                    {
                        case (1): // Если студент
                            navigation.NewWindow("MyClient.View.Student.StudentWindow");
                            break;
                        case (2): // Если преподаватель
                            navigation.NewWindow("MyClient.View.Teacher.TeacherWindow");
                            break;
                        case (3): // Если администратор
                            navigation.NewWindow("MyClient.View.Administrator.AdministratorWindow");
                            break;
                        default:
                            break;
                    }

                }
            }            
            else if (MyAcc == null) // Иначе, если аккаунт пустой, то выдай ошибку
            {
                MyDialog.ShowMessage("Аккаунт не найден!");
            }
        }

        public MyModelLibrary.accounts AuthorizationUser(string login, string password) // Метод для авторизации юзера
        {

            // Если хост запущен, то выведи об этом ошибку
            try
            {
                MyModelLibrary.accounts MyAcc = AccountProxy.ConnectUser(login, password); // Получаем аккаунт
                OpenedWindow(MyAcc); // Открываем окно
                return MyAcc;
            }
            catch (EndpointNotFoundException)
            {
                MyDialog.ShowMessage("Сервер не запущен!");
            }
            catch (FaultException<AccountService.AccountConnectedException> ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }


            return null;
        }

        #endregion


        #region call-back методы

        // Метод DoWork отправляет true на сервер
        // Это некая проверка на то, подключен ли пользователь к серверу
        public bool DoWork()
        {
            return true;
        }

        #endregion

    }
}
