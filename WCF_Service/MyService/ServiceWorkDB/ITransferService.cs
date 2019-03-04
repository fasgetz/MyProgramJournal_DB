using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.DataBase;

namespace WCF_Service.MyService.ServiceWorkDB
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITransferService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITransferService
    {



        #region Методы администратора

        // Метод, который получит весь список пользователей, если пользователь пройдет проверку (Он должен быть администратором)
        [OperationContract]
        List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc);

        // Метод, который получает весь список аккаунтов, если пользователь пройдет проверку (Если он администратор)
        [OperationContract]
        List<MyModelLibrary.accounts> GetAllAccountsList(MyModelLibrary.accounts MyAcc);

        // Метод, который принимает со стороны клиента аккаунт и добавляет его в базу данных, соответственно, если аккаунт == администратор и возвращает true, если аккаунт создан
        [OperationContract]
        bool AddAccount(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc);



        #endregion



        #region Методы новостей

        [OperationContract]
        void AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews);


        #endregion

    }
}
