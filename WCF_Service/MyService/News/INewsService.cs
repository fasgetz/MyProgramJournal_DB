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

        // Метод, который получает список новостей
        [OperationContract]
        List<MyModelLibrary.News> GetNewsList();

        // Метод на удаление новости. Принимаем аккаунт 
        // (Делаем проверку чтобы аккаунт был администратором) и айди новости, 
        // которую будем удалять если он администратор = =3
        [OperationContract]
        bool RemoveNews(int idNews, MyModelLibrary.accounts MyAcc);
        #endregion
    }
}
