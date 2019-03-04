using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service.MyService.News
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "INewsService" в коде и файле конфигурации.
    [ServiceContract]
    public interface INewsService
    {
        #region Методы новостей

        // Метод, который создает новости
        [OperationContract]
        bool AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews);

        #endregion
    }
}
