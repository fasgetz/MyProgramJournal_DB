using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.Exceptions
{
    // Класс, который посылает экзепшен юзеру
    public static class ExceptionSender
    {
        // Метод, который принимает Message и отправляет экзепшен юзеру
        public static void SendException(string ExceptionMessage)
        {
            throw new FaultException<Exceptions.AccountConnectedException>(new Exceptions.AccountConnectedException(ExceptionMessage),
                            new FaultReason(ExceptionMessage));
        }
    }
}
