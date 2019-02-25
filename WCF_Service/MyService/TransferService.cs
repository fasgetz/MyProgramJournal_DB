using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCF_Service.DataBase;
using WCF_Service.DataBase.DTO;
using WCF_Service.DataBase.Repositories;

namespace WCF_Service.MyService
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TransferService : ITransferService
    {
        // Список подключенных аккаунтов к службе
        List<accounts> ConnectedAccounts = new List<accounts>();

        #region Вспомогательные методы

        // Метод который дисконнектит юзера из списка подключенных на сервисе и заканчивает сессию
        private void UserDisconnect(int IdAccount)
        {

            try
            {
                // Необходимо отключить аккаунт от подключенных, а так-же добавить сессию
                // Ищем аккаунт в списке подключенных
                var a = ConnectedAccounts.FirstOrDefault(i => i.idAccount == IdAccount);
                if (a != null)
                {
                    a.SessionsAccounts.LastOrDefault().EndLogin = System.DateTime.Now; // Присваиваем концу сессии текущее время
                    ConnectedAccounts.Remove(a); // Удаляем юзера из списка

                    Console.WriteLine($"Аккаунт: <<{a.login}>> отключился в {System.DateTime.Now}");
                    Console.WriteLine($"Всего юзеров подключено: {ConnectedAccounts.Count()}");
                    foreach (var item in ConnectedAccounts)
                    {
                        Console.WriteLine($"Юзер: {item.login}");
                    }
                    Console.WriteLine();

                    // Добавляем сессию в бд
                    EFGenericRepository<SessionsAccounts> kek = new EFGenericRepository<SessionsAccounts>(new MyDB()); // Создаем подключение к бд и передаем контекст
                    SessionsAccounts MySession = new SessionsAccounts();
                    MySession.idAccount = a.idAccount;
                    MySession.StartLogin = a.SessionsAccounts.LastOrDefault().StartLogin;
                    MySession.EndLogin = System.DateTime.Now;
                    kek.Add(MySession);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        // Метод, который добавляет юзера в список подключений на сервисе и начинает сессию
        private bool AddListUser(accounts MyAcc)
        {
            // Делаем проверку: если этого юзера нет в списке, то добавить его в список
            if (ConnectedAccounts.FirstOrDefault(i => i.idAccount == MyAcc.idAccount) == null)
            {
                // Добавляем сессию аккаунту и инициализируем данные
                SessionsAccounts MySession = new SessionsAccounts();
                MySession.idAccount = MyAcc.idAccount; // Присваиваем сессии айди аккаунта
                MySession.StartLogin = System.DateTime.Now;  // Присваиваем начало сессии текущую дату

                MyAcc.SessionsAccounts.Add(MySession); // Добавляем сессию аккаунту


                // Далее заносим аккаунт в список подключенных аккаунтов
                ConnectedAccounts.Add(MyAcc);

                // Выводим в консоль надпись о подключении клиента
                Console.WriteLine($"Аккаунт: <<{MyAcc.login}>> подключился в {System.DateTime.Now}");
                Console.WriteLine($"Всего юзеров подключено: {ConnectedAccounts.Count()}");
                foreach (var item in ConnectedAccounts)
                {
                    Console.WriteLine($"Юзер: {item.login}");
                }
                Console.WriteLine();

                return true;
            }
            else
            {
                Console.WriteLine("Невозможно авторизироваться, т.к. юзер уже подключен");
                return false; // Верни false
            }
            
        }

        #endregion


        #region Методы контракта служб

        // Метод, который вернет весь список пользователей если он является администратором
        public List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc)
        {
            // Создаем репозиторий для работы с контекстом базы данных
            EFGenericRepository<Users> MyRepository = new EFGenericRepository<Users>(new MyDB());

            // Делаем проверку: Является ли полученный юзер администратором ( s == 3 )
            if (Convert.ToInt32(MyRepository.FindQueryEntity(i => i.idUser == MyAcc.Users.idUser).idUserStatus) == 3)
            {
                // Тогда создаем DTO список юзеров и возвращаем его администратору
                List<MyModelLibrary.Users> UsersList = new List<MyModelLibrary.Users>();

                // Заносим список not dto юзеров 
                var a = MyRepository.GetAllList();

                DataBase.DTO.MyGeneratorDTO generator = new MyGeneratorDTO();

                // Перебираем список и генерируем DTO, который внесем в DTO список
                foreach (var item in a)
                {
                    // Вносим в список сгенерированный dto item
                    UsersList.Add(generator.GetUser(item));
                }

                Console.WriteLine($"{System.DateTime.Now}) Возвращаем клиенту {MyAcc.login} список с {a.Count()} клиентами!");

                // Возвращаем dto список
                return UsersList;
            }


            return null;
        }

        // Метод, который вернет юзера в случае успешной авторизации
        public MyModelLibrary.accounts GetAccounts(string login, string password)
        {

            EFGenericRepository<accounts> kek = new EFGenericRepository<accounts>(new MyDB()); // Создаем подключение к бд и передаем контекст

            accounts Account = kek.FindQueryEntity(i => i.login == login && i.password == password); // Получаем аккаунт

            // Если аккаунт не пустой, то верни клиенту в DTO
            if (Account != null)
            {

                // Если статус аккаунта != неактивный, то добавь его к серверу в список подключенных
                if (Account.idStatus != 0)
                {
                    bool Connected = AddListUser(Account); // Подключаем аккаунт к сервису

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
                        Console.WriteLine("Выбрасываем исключение");
                        throw new FaultException<Exceptions.AccountConnectedException>(new Exceptions.AccountConnectedException($"Пользователь  <<{Account.login}>> уже подключен!"));
                    }
                }
           
            }

            // Иначе, если аккаунт пустой, то выдай пустой аккаунт
            return null;
        }

        // Метод, который дисконнектит юзера
        public void DisconnectUser(int IdAccount)
        {
            UserDisconnect(IdAccount);
        }



        #endregion
    }
}
