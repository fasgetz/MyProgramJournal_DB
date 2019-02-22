using System.ServiceModel;

namespace WCF_Service.MyService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITransferService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITransferService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        string login();
    }
}
