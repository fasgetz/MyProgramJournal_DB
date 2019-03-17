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

        // Метод, который удаляет занятие у группы (С проверкой на администратора)
        public bool DeleteLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.LessonsDate lessons)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные с удалением на сервер
                    return client.DeleteLessonGroup(MyAcc, lessons); // Возвращаем результат удаления
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

            return false; // Возвращаем false, если удаление неудачно
        }

        // Метод, который добавляет занятие (С проверкой на администратора) группе в семестре, по выбранной дате, по номеру пары и возвращает true, если занятие добавлено успешно
        public bool AddLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines discipline, int? numberlesson, System.DateTime date)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные на сервер. Если получили true, то добавлено успешно
                    bool AddedLesson = client.AddLessonGroup(MyAcc, discipline, numberlesson, date);

                    if (AddedLesson == true)
                    {
                        MyDialog.ShowMessage($"Занятие добавлено успешно!");

                        return true; // Возвращаем true, т.к. добавили успешно
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

            return false; // Возвращаем false, если добавление неудачно
        }

        // Метод, который возвращает пользователю с проверкой на администратора, список пар (от 1 до 8), которые можно еще добавить группе в семестре, по дате
        public List<int> GetLessonsNumbers(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups SelectedGroup, DateTime Date, int? Semestr)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetLessonsNumbers(MyAcc, SelectedGroup, Date, Semestr);
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

            return null;
        }

        // Метод, который получает список занятий за выбранный период (Дата) группы в семестре с проверкой на администратора
        public List<MyModelLibrary.LessonsDate> GetLessonsOnDate(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, System.DateTime date, int? semestr)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetLessonsOnDate(MyAcc, group, date, semestr);
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

            return null; // Возвращаем null, если неудалось получить список
        }

        // Метод редактирования названия дисциплины
        public bool EditDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline discipline)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные с клиента на сервер
                    bool edit = client.EditDisciplineName(MyAcc, discipline);

                    // Если редактирование прошло успешно (== true), то выведи об этом и верни true
                    if (edit == true)
                    {
                        MyDialog.ShowMessage($"Название дисциплины редактировано успешно!");

                        return true; // Вовзрщаем true, т.к. редактирование прошло успешно
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

            return false; // Вовзращаем false, если рендактиорвание неудачно
        }

        // Метод добавления дисциплины группе
        public bool AddDisciplineGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline discipline, int? NumbSem)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные на сервер и получаем true, если дисциплина группе добавлена успешно
                    bool Added = client.AddDisciplineGroup(MyAcc, group, Teacher, discipline, NumbSem);

                    // Если дисциплина группе добавлена успешно, то выведи об этом
                    if (Added == true)
                    {
                        MyDialog.ShowMessage($"Дисциплина группе добавлена успешно!");

                        return true; // Возвращаем true, т.к. дисциплина группе добавлена успешно
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

            return false; // false, если неудачное добавление
        }

        // Метод, который возвращает список преподавателей, которые ведут дисциплину
        public List<MyModelLibrary.Users> GetUsersFromDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline SelectedDiscipline)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetUsersFromDiscipline(MyAcc, SelectedDiscipline);
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

            return null; // Если не удалось ничего получить, то возвращаем null
        }

        // Метод получения списка дисциплин группы
        public List<MyModelLibrary.GroupDisciplines> GetGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, int? semestr)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetGroupDisciplines(MyAcc, group, semestr); // Возвращаем с сервера
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

            return null; // Если не удалось получить группы
        }

        // Метод, который выдаст список дисциплин учителя / дисциплин, которых еще нет у учителя
        // (param = 1, выдаст дисциплины учителя / param = 2, выдаст дисциплины, которых еще нет у учителя)
        public List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetTeacherDisciplines(MyAcc, Teacher, param);
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

            return null; // Если неудачно
        }

        // Метод редактирования группы
        public bool EditGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups EditGroup)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные на сервер. Выдаст true, если удаление успешно
                    bool GroupEdit = client.EditGroup(MyAcc, EditGroup);

                    // Если группа отредактирована успешно, то выведи об этом и верни true
                    if (GroupEdit == true)
                    {
                        MyDialog.ShowMessage($"Группа редактирована успешно");

                        return true;
                    }
                    else
                    {
                        MyDialog.ShowMessage($"Не удалось редактировать группу");                        
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

            return false; // Возвращаем false, если редактирование неудачно
        }

        // Метод удаления группа
        public bool RemoveGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups Group)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные на сервер. Выдаст true, если удаление успешно
                    bool GroupRemoved = client.RemoveGroup(MyAcc, Group);

                    // Если группа удалена успешно, то выведи об этом и верни true
                    if (GroupRemoved == true)
                    {
                        MyDialog.ShowMessage($"Группа удалена успешно");

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

            return false; // Возвращаем false, если удаление неудачно
        }

        // Метод добавления дисциплины преподавателю
        public bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Отправляем данные на сервер. Если выдаст true, то дисциплина успешно добавлена
                    bool Added = client.AddTeacherDiscipline(MyAcc, Teacher, Discipline);

                    // Если дисциплина добавлена, то выведи об этом и верни истину
                    if (Added == true)
                    {
                        MyDialog.ShowMessage($"Вы успешно добавили дисциплину преподавателю!");

                        return true; // Вовзращаем true, т.к. дисциплина успешно добавлена
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


            return false; // Если добавить не удалось, то false 
        }

        // Метод, который выдаст список дисциплин учителя / дисциплин, которых еще нет у учителя
        // (param = 1, выдаст дисциплины учителя / param = 2, выдаст дисциплины, которых еще нет у учителя)
        public List<MyModelLibrary.Discipline> GetNotAddedGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, byte param)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetNotAddedGroupDisciplines(MyAcc, group, param);
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

            return null; // Если неудачно
        }

        // Метод получения списка учителей
        public List<MyModelLibrary.Users> GetTeachersList(MyModelLibrary.accounts MyAcc)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetTeacherList(MyAcc); // Возвращаем список пользователей
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

            return null; // Если неудачно
        }

        // Метод добавления дисциплины
        public bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline Discipline)
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
            try
            {
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

        // Метод для получения списка аккаунтов
        public List<MyModelLibrary.accounts> GetAccountsList(MyModelLibrary.accounts MyAcc)
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetAllAccountsList(MyAcc).ToList();
                }
            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return null;
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
