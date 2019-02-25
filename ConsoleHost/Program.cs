﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запускаем сервис TransferService
            using (var host = new ServiceHost(typeof(WCF_Service.MyService.ServiceWorkDB.TransferService)))
            {
                host.Open(); // Открываем хост
                Console.WriteLine($"{DateTime.Now}) Хост TransferSerivce стартовал!");
                Console.ReadLine();
            }
        }
    }
    
}
