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
        private NewsService.NewsServiceClient client; // Ссылка на клиент
        private DialogService MyDialog; // Для работы с диалогами

        #region Методы

        // Метод создания новости
        public bool AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {
            try
            {
                // Инициализируем канал связи клиента с сервером
                if (client == null)
                    client = new NewsService.NewsServiceClient();

                bool added_news = client.AddNews(MyAcc, MyNews);

                // Если новость добавлена успешно, то
                if (added_news == true)
                {
                    MyDialog.ShowMessage("Новость добавлена!");
                    return true;
                }
                // Иначе, если новость не добавлена
                else
                {
                    MyDialog.ShowMessage("Не удалось добавить новость");
                    return false;
                }

            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return false;

        }

        #endregion


        public NewsLogic()
        {
            MyDialog = new DialogService();
        }

    }
}
