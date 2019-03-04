using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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

        #endregion

    }
}
