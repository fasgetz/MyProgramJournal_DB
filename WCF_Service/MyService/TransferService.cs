using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using WCF_Service.DataBase;
using WCF_Service.DataBase.Repositories;

namespace WCF_Service.MyService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "TransferService" в коде и файле конфигурации.
    public class TransferService : ITransferService
    {
        public void DoWork()
        {

        }

        // Метод, который вернет юзера в случае успешной авторизации
        public MyModelLibrary.accounts GetAccounts(string login, string password)
        {
            throw new NotImplementedException();
        }

        public string login()
        {

            EFGenericRepository<UserStatus> kek2 = new EFGenericRepository<UserStatus>(new MyDB());

            EFGenericRepository<accounts> kek = new EFGenericRepository<accounts>(new MyDB());
            var a = kek.GetAllList();
            var b = kek2.GetAllList();

            var tp = kek.GetQueryList(i => i.login == "fasgetz");
            Console.WriteLine($"Всего аккаунтов! {a.Count()}");
            Console.WriteLine($"Всего статусов! {b.Count()}");
            Console.WriteLine($"Аккаунт найден {a.FirstOrDefault().login}");


            var MyUser = kek.FindQueryEntity(i => i.login == "fasgetz");
            Console.WriteLine($"\n\n\n Аккаунт найден!! {MyUser.SessionsAccounts.Count()} \n");

            try
            {
                using (MyDB db = new MyDB())
                {

                    var abba = db.accounts.Where(i => i.login.StartsWith("f")).ToList();
                    Console.WriteLine($"Найдено аккаунтов на f: {abba.Count()}");

                    Console.WriteLine(db.Database.Connection.Database.ToString());
                    Console.WriteLine(db.accounts.FirstOrDefault().login.ToString());
                    Console.WriteLine(db.accounts.FirstOrDefault().StatusAccounts.StatusAccount);
                    return db.accounts.FirstOrDefault().login;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return "kek";
        }
    }
}
