using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.DataBase;

namespace WCF_Service.MyService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITransferService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITransferService
    {

        
        [OperationContract]
        [FaultContract(typeof(Exceptions.AccountConnectedException))] // В этом методе может вызываться это исключение
        MyModelLibrary.accounts GetAccounts(string login, string password); // Метод, который возвращает юзера в случае удачного коннекта

        // Метод, который дисконнектнет юзера со списка подключенных юзеров на сервисе
        // IsOneWay = true значит, что метод не должен ждать ответа от сервера. Его задача просто послать этот метод
        [OperationContract(IsOneWay = true)]
        void DisconnectUser(int IdAccount);


        // Метод, который получит весь список пользователей, если пользователь пройдет проверку (Он должен быть администратором)
        [OperationContract]
        List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc); 


    }
}
