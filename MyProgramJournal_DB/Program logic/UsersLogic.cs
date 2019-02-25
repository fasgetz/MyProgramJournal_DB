using MyProgramJournal_DB.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgramJournal_DB.Program_logic
{

    /// <summary>
    /// Класс предназначен для организации логики работы с пользователями
    /// </summary>

    public class UsersLogic
    {


        private static MyService.TransferServiceClient client; // Ссылка на клиент
        private static DialogService MyDialog; // Для работы с диалогами


        // Метод, который получает всех пользователей
        public List<MyModelLibrary.Users> GetUsersList(MyModelLibrary.accounts MyAcc)
        {
            // Создаем подключение к серверу
            using (client = new MyService.TransferServiceClient())
            {
                // Прогружаем список пользователей
                List<MyModelLibrary.Users> UsersList = client.GetAllUsersList(MyAcc).ToList();              
                return UsersList; // Возвращаем список пользователей
            }


        }

        public UsersLogic()
        {
            MyDialog = new DialogService();
        }

    }
}
