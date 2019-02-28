using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.DataBase.DTO;
using WCF_Service.DataBase.Repositories;
using WCF_Service.Exceptions;
using WCF_Service.ServiceLogic;

namespace WCF_Service.MyService.AccountsService.Service
{


    // Тип сервиса - постоянный. Т.к. на нем размещаются подключенные юзеры
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyServiceAccount : IMyServiceAccount, ICallBack
    {
        // Конструктор класса
        public MyServiceAccount()
        {
            ConnectedAccounts = new List<accounts>();

            Task MyTask = new Task(DisconnectedDisabledAccounts);
            MyTask.Start(); // Запускаем поток
        }
        

        #region Свойства

        // Список подключенных аккаунтов
        public List<accounts> ConnectedAccounts;

        #endregion

        #region Вспомогательные методы для работы с контрактами

        // Метод, который выводит список подключенных аккаунтов в консоль для хоста
        private void AccountsConnectedWatch()
        {
            Console.WriteLine($"Всего юзеров подключено: {ConnectedAccounts.Count()}");
            foreach (var item in ConnectedAccounts)
            {
                Console.WriteLine($"Юзер: {item.login}");
            }
            Console.WriteLine();
        }


        // Метод для подключения аккаунта к списку
        public bool ConnectAccount(accounts MyAcc)
        {


            // Делаем проверку: если этого юзера нет в списке, то добавить его в список
            if (ConnectedAccounts.FirstOrDefault(i => i.idAccount == MyAcc.idAccount) == null)
            {
                // Добавляем сессию аккаунту и инициализируем данные
                SessionsAccounts MySession = new SessionsAccounts();
                MySession.idAccount = MyAcc.idAccount; // Присваиваем сессии айди аккаунта
                MySession.StartLogin = DateTime.Now;  // Присваиваем начало сессии текущую дату
                MyAcc.SessionsAccounts.Add(MySession); // Добавляем сессию аккаунту
                MyAcc.operationContext = OperationContext.Current; // Добавляем аккаунт к текущим сессиям

                // Далее заносим аккаунт в список подключенных аккаунтов
                ConnectedAccounts.Add(MyAcc);

                // Выводим в консоль надпись о подключении клиента
                Console.WriteLine($"Аккаунт: <<{MyAcc.login}>> подключился в {System.DateTime.Now}");
                AccountsConnectedWatch(); // Показываем список подключенных аккаунтов

                return true; // Возвращаем true - значит, что аккаунт успешно подключился
            }
            // Иначе, аккаунт подключен к сервису - верни false
            else
            {
                Console.WriteLine($"Невозможно авторизироваться, т.к. юзер <<{MyAcc.login}>> уже подключен");
                return false; // Верни false
            }

        }


        // Метод для отключения аккаунта из списка
        public bool DeleteAccount(int idAccount)
        {
            // Необходимо отключить аккаунт от подключенных, а так-же добавить сессию и отправить ее

            // Ищем аккаунт в списке подключенных
            var a = ConnectedAccounts.FirstOrDefault(i => i.idAccount == idAccount);

            // Если нашли аккаунт в списке подключенных, то отключи его из списка подключенных аккаунтов,
            // а также закончи его сессию и отправь в базу данных
            if (a != null)
            {

                // Добавляем сессию в бд
                EFGenericRepository<SessionsAccounts> kek = new EFGenericRepository<SessionsAccounts>(new MyDB()); // Создаем подключение к бд и передаем контекст
                SessionsAccounts MySession = new SessionsAccounts();
                MySession.idAccount = a.idAccount; // Айди аккаунта - айди аккаунта
                MySession.StartLogin = a.SessionsAccounts.LastOrDefault().StartLogin; // Начало сессии - последняя авторизация юзера
                MySession.EndLogin = System.DateTime.Now; // Конец сессии - текущее время
                kek.Add(MySession);  // Добавляем сессию в базу данных

                ConnectedAccounts.Remove(a); // Удаляем юзера из списка подключенных аккаунтов

                Console.WriteLine($"Аккаунт: <<{a.login}>> отключился в {System.DateTime.Now}");
                AccountsConnectedWatch(); // Показываем список подключенных аккаунтов

                return true; // true - значит, что аккаунт отключили от сервиса

            }
            // Иначе, если не нашли аккаунт в списке подключенных, то верни false
            else
            {
                return false; // false - значит, что аккаунта нет в списке подключенных
            }
        }

        #endregion

        #region Для call-back методов

        // Метод, который дисконнектит юзеров, если они не отвечают на метод DoWork()
        // Это значит, что они отключены от сети. Значит юзера надо убрать из списка подключенных
        public void DisconnectedDisabledAccounts()
        {
            // Пока сервис работает делай проверку на отключенных юзеров
            while (true)
            {
                Thread.Sleep(30000); // Отключение метода на 30 секунд
                Console.WriteLine($"\n{DateTime.Now}) Проверка на неактивных юзеров");

                // Если в списке подключенных аккаунтов есть кто-то, то проверь, подключен ли он к серверу
                if (ConnectedAccounts.Count() != 0)
                {

                    // Далее перебираем всех подключенных юзеров и вызываем метод проверки
                    foreach (var item in new List<accounts>(ConnectedAccounts))
                    {
                        try
                        {
                            bool ItemConnected = item.operationContext.GetCallbackChannel<ICallBack>().DoWork();

                            // Если аккаунт отключен, то отключи его от сервиса
                            if (ItemConnected == false)
                            {
                                DeleteAccount(item.idAccount); // Отключаем аккаунт от сервиса
                                Console.WriteLine($"\n{DateTime.Now}) Аккаунт <<{item.login}>> отключен от сервера!\n");
                            }
                        }
                        catch (Exception)
                        {
                            DeleteAccount(item.idAccount);
                        }
                    }
                }
            }
        }

        #endregion

        #region Методы контракта службы MyServiceAccount

        // Метод, который подключает юзера к серверу по учетным данным
        public MyModelLibrary.accounts ConnectUser(string login, string password)
        {

            EFGenericRepository<accounts> kek = new EFGenericRepository<accounts>(new MyDB()); // Создаем подключение к бд и передаем контекст
            accounts Account = kek.FindQueryEntity(i => i.login == login && i.password == password); // Получаем аккаунт

            // Если аккаунт не пустой, то значит он есть в базе данных, то сделай проверку: активный он или нет
            if (Account != null)
            {
                // Если статус аккаунта != неактивный, то добавь его к серверу в список подключенных
                if (Account.idStatus != 0 && Account.Users != null)
                {
                    bool Connected = ConnectAccount(Account); // Подключаем аккаунт к сервису

                    // Если подключение к серверу успешное, то верни полный аккаунт
                    if (Connected == true)
                    {
                        #region Генерация DTO объекта и возвращение его клиенту

                        // Создаем генератор DTO объектов
                        MyGeneratorDTO generator = new MyGeneratorDTO();

                        // Преобразуем аккаунт в DTO - объект
                        MyModelLibrary.accounts MyAccount = generator.GetAccountDTO(Account);

                        return MyAccount; // Возвращаем DTO - клиенту

                        #endregion

                    }
                    // Иначе, если аккаунт уже подключен, то выдай ошибку
                    else
                    {
                        Console.WriteLine($"{DateTime.Now}) Аккаунт <<{Account.login}>> уже подключен!");
                        throw new FaultException<Exceptions.AccountConnectedException>(new Exceptions.AccountConnectedException($"Аккаунт <<{Account.login}>> уже подключен!"), new FaultReason($"Аккаунт <<{Account.login}>> уже подключен!"));
                    }
                }
                // Иначе, если статус аккаунта == неактивный, то верни пользователю это
                else if (Account.idStatus == 0)
                {
                    throw new FaultException<Exceptions.AccountConnectedException>(new Exceptions.AccountConnectedException($"Ваш аккаунт <<{Account.login}>> неактивный!\nОбратитесь к администратору для активации"), new FaultReason($"Ваш аккаунт <<{Account.login}>> неактивный!\nОбратитесь к администратору для активации"));                    
                }
                // Иначе, если статус аккаунта != неактивный, но у него нет учетных данных, то выдай соответственную ошибку
                else if (Account.idStatus != 0 && Account.Users == null)
                {
                    throw new FaultException<Exceptions.AccountConnectedException>(new Exceptions.AccountConnectedException($"Аккаунту <<{Account.login}>> еще не присвоили учетных данных!\nОбратитесь к администратору!"), new FaultReason($"Аккаунту <<{Account.login}>> еще не присвоили учетных данных!\nОбратитесь к администратору!"));                    
                }

            }

            // Иначе, если аккаунт пустой, то выдай пустой аккаунт
            return null;
        }


        // Метод, который дисконнектнет юзера со списка подключенных юзеров на сервисе        
        public void DisconnectUser(MyModelLibrary.accounts MyAccount)
        {
            DeleteAccount(MyAccount.idAccount); // Дисконнектим юзера
        }

        #endregion

        #region CallBack методы

        // CallBack метод DoWork отправляется клиентом на сервер каждые 30 секунд
        // Если он отправляется клиентом на сервер, то он остается в списке подключенных клиентов
        // Если клиентом не отправляется этот метод, то сервер отключает клиента, т.к. он отключен от сети
        public bool DoWork()
        {
            return true;
        }

        #endregion

    }
}
