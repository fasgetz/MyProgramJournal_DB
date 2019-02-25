using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service.MyService.AccountsService.Service
{

    [ServiceContract(CallbackContract = typeof(ICallBack))]
    public interface IMyServiceAccount
    {

        // Метод, который подключает юзера к серверу по учетным данным
        [OperationContract]
        [FaultContract(typeof(Exceptions.AccountConnectedException))] // В этом методе может вызываться это исключение
        MyModelLibrary.accounts ConnectUser(string login, string password);


        // Метод, который дисконнектнет юзера со списка подключенных юзеров на сервисе
        // IsOneWay = true значит, что метод не должен ждать ответа от сервера. Задача клиента просто послать этот метод на сервер
        [OperationContract(IsOneWay = true)]
        void DisconnectUser(MyModelLibrary.accounts MyAccount);



    }

    [ServiceContract]
    public interface ICallBack
    {
        // Метод, который вызывается каждые 30 секунд со стороны клиента
        // Если true, то юзер подключен к серверу, иначе он отключен и его необходимо отключить из списка
        [OperationContract(IsOneWay = false)]
        bool DoWork();
    }


}
