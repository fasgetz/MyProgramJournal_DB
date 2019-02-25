using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AccountsHost
{

    // Хост для работы с подключениями аккаунтов
    class Program
    {
        static void Main(string[] args)
        {


            using (var host = new ServiceHost(typeof(WCF_Service.MyService.AccountsService.Service.MyServiceAccount)))
            {
                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) Хост MyServiceAccount стартовал!\n");
                Console.ReadLine();
            }
        }
    }
}
