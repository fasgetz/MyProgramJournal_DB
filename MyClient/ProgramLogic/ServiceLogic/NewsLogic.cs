using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.ServiceLogic
{


    /// <summary>
    /// Класс предназначен для работы с сервером новостей
    /// </summary>

    public class NewsLogic
    {

        private MyService.TransferServiceClient client; // Ссылка на клиент
        private DialogService MyDialog; // Для работы с диалогами

        #region Методы

        // Метод создания новости
        public void AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {


            try
            {
                // Инициализируем канал связи клиента с сервером
                if (client == null)
                    client = new MyService.TransferServiceClient();

                client.AddNews(MyAcc, MyNews);
            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

        }

        #endregion


        public NewsLogic()
        {
            MyDialog = new DialogService();
        }

    }
}
