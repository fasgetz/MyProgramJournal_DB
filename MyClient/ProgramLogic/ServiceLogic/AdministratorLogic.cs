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

    internal class AdministratorLogic : CommonLogic
    {
        #region Методы для работы с сервером

        // Метод добавления дисциплины
        public bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline Discipline)
        {
            // Если входные данные не пустые, то продолжи добавление
            if (Discipline != null && MyAcc != null)
            {
                try
                {
                    // Создаем канал связи с сервером
                    using (client = new MyService.TransferServiceClient())
                    {
                        // Отправляем аккаунт и дисциплину на сервер. На выходе получаем true, если дисциплина добавлена успешно
                        bool Add = client.AddDiscipline(MyAcc, Discipline); 

                        // Если дисциплина добавлена успешно, то выведи об этом и верни true
                        if (Add == true)
                        {
                            MyDialog.ShowMessage("Дисциплина добавлена успешно!");

                            return true;
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
            }

            return false; // Возвращаем false, если добавление прошло неудачно
        }

        // Метод для получения списка дисциплин
        public List<MyModelLibrary.Discipline> GetDisciplines(MyModelLibrary.accounts MyAcc)
        {
            // Делаем проверку на то, не пустые ли входные данные аккаунта
            if (MyAcc != null)
            {
                try
                {
                    // Создаем канал связи с сервером
                    using (client = new MyService.TransferServiceClient())
                    {
                        // Передаем текущий аккаунт и получает список дисциплин, если он администратор
                        var list = client.GetDisciplinesList(MyAcc);

                        // Если список не пустой, то возвращаем его
                        if (list != null)
                            return list;
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

            return null; // Возвращаем null, если неудалось получить список
        }

        // Метод удаления студенты из группы
        public bool DeleteStudentFromGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Делаем проверку на то, не пустые ли входные данные студента
            if (Student != null)
            {
                try
                {
                    // Создаем канал связи с сервером
                    using (client = new MyService.TransferServiceClient())
                    {
                        // Передаем текущий аккаунт(На сервере пройдет проверка на администратора), а также студента, которого необходимо удалить из группы
                        bool DeleteStudent = client.RemoveStudentInGroup(MyAcc, Student);

                        // Если студент удален успешно, то выведи об этом пользователю, пославшему запрос
                        if (DeleteStudent == true)
                        {
                            MyDialog.ShowMessage($"Вы успешно удалили студента из группы");

                            return true; // Возвраем true, т.к. студент удален из группы
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
            }

            return false; // Вернет fasle, если удаление прошло неудачно
        }

        // Метод добавления студента в группу
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Делаем проверку на то, не пустые ли входные данные студента
            if (Student != null)
            {
                try
                {
                    // Создаем канал связи с сервером
                    using (client = new MyService.TransferServiceClient())
                    {
                        // Передаем текущий аккаунт (На сервере пройдет проверка на администратора) и студента, которого необходимо добавить в группу
                        bool AddedStudent = client.AddStudentInGroup(MyAcc, Student);

                        // Делаем проверку на добавление студента в группу. Если студент добавлен, то верни true
                        if (AddedStudent == true)
                        {
                            MyDialog.ShowMessage($"Вы успешно добавили студента в группу!");

                            return true; // Возвращаем true, т.к. студент успешно добавлен в группу
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
            }

            return false; // Вернуть false, если добавление студента неудачно
        }

        // Метод для создания группы
        public bool CreateGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup)
        {
            // Делаем проверку на то, не пустая ли группа
            if (NewGroup != null)
            {
                try
                {
                    // Создаем канал связи с сервером
                    using (client = new MyService.TransferServiceClient())
                    {
                        // Передаем текущий аккаунт (идет проверка на администратора) и группу, которую создаем
                        bool GroupCreated = client.AddGroup(MyAcc, NewGroup);

                        // Делаем проверку на создание группы.Если создалась, то верни true
                        if (GroupCreated == true)
                        {
                            MyDialog.ShowMessage($"Вы успешно создали группу {NewGroup.GroupName}");

                            return true; // возвращаем true, если группа успешно создана
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
            }

            return false;
        }

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
                return client.GetAllAccountsList(MyAcc).ToList();
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
