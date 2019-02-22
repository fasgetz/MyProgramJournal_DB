using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgramJournal_DB.Program_logic
{
    /// <summary>
    /// Класс предназначен для организации логики работы с аккаунтом
    /// </summary>
    
    public static class AccountLogic
    {


        private static MyService.TransferServiceClient client; // Ссылка на клиент



        public static MyModelLibrary.accounts AuthorizationUser() // Метод для авторизации юзера
        {
            client = new MyService.TransferServiceClient();


            return null;
        }
    }
}
