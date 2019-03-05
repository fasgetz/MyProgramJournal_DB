using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyModelLibrary;
using WCF_Service.DataBase;
using WCF_Service.ServiceLogic;

namespace WCF_Service.MyService.News
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "NewsService" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class NewsService : INewsService
    {

        #region Свойства

        ServiceNewsLogic MyNewsLogic = new ServiceNewsLogic();

        #endregion


        #region Методы контракта службы

        // Метод, который создает новость, если аккаунт имеет статус администратора
        public bool AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {
            return MyNewsLogic.AddNews(MyAcc, MyNews);
        }

        // Метод, который вернет список новостей
        public List<MyModelLibrary.News> GetNewsList()
        {
            List<MyModelLibrary.News> MyList = MyNewsLogic.GetNews(); // Получаем список новостей

            // Если список не пустой, то выполни реверсию и верни список
            if (MyList != null)
            {
                MyList.Reverse(); // Реверсируем список
                return MyList; // Верни список новостей
            }

            return null; // Иначе вернет пустой список
        }

        // Метод, который удаляет новости
        public bool RemoveNews(int idNews, MyModelLibrary.accounts MyAcc)
        {
            return MyNewsLogic.RemoveNews(idNews, MyAcc); // Возвращаем true если новость удалена. Или false, если новость не удалена
        }



        #endregion

    }
}
