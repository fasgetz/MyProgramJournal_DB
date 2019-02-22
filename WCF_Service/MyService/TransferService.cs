using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Service.DataBase;

namespace WCF_Service.MyService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "TransferService" в коде и файле конфигурации.
    public class TransferService : ITransferService
    {
        public void DoWork()
        {

        }

        public string login()
        {
            try
            {
                using (MyDB db = new MyDB())
                {
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
