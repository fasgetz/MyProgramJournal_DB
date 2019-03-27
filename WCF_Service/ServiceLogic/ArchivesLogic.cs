using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.DataBase.Repositories;

namespace WCF_Service.ServiceLogic
{
    /// <summary>
    /// Класс реализует логику работы с архивами
    /// </summary>
    

    class ArchivesLogic
    {
        // Асинхронный метод добавления архив в БД
        public async static void AddArchive(string Commentary, int IdOrderArchive)
        {
            try
            {
                await Task.Run(() =>
                {
                    // Создаем репозиторий и добавляем оценку в БД
                    new EFGenericRepository<OrderArchive>(new MyDB()).Add(new OrderArchive(Commentary, IdOrderArchive));
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
