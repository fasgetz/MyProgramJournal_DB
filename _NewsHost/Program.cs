using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _NewsHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WCF_Service.MyService.News.ServiceNews)))
            {
                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) Хост NewsService стартовал!\n");
                Console.ReadLine();
            }
        }
    }
}
