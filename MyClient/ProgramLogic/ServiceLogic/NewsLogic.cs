using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.ServiceLogic
{


    /// <summary>
    /// Класс предназначен для работы с сервером новостей
    /// </summary>

    internal class NewsLogic
    {
        private NewsService.NewsServiceClient client; // Ссылка на клиент
        private DialogService MyDialog; // Для работы с диалогами

        #region Методы
        
        // Метод редактирования новости
        public bool EditNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {
            try
            {
                // Инициализируем канал связи клиента с сервером
                using (client = new NewsService.NewsServiceClient())
                {
                    // Посылаем запрос на редактирование новости
                    bool edit_news = client.EditNews(MyAcc, MyNews);

                    // Если новость успешно отредактирована, то выведи об этом и верни true
                    if (edit_news == true)
                    {
                        MyDialog.ShowMessage("Новость успешно отредактирована!");
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return false; // false, если неудачное редактирование
        }

        // Метод удаления новости
        public bool DeleteNews(MyModelLibrary.accounts MyAcc, int IdNews)
        {
            try
            {
                // Создаем канал связи клиента с сервером
                using (client = new NewsService.NewsServiceClient())
                {
                    // Если true, то новость удалена, иначе (если новость не удалена, то false)
                    bool delete_news = client.RemoveNews(IdNews, MyAcc);

                    // Если новость удалена, то выведи об этом и верни true
                    if (delete_news == true)
                    {
                        MyDialog.ShowMessage($"Вы успешно удалили новость!");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return false; // false, если не получилось удалить новость
        }

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

        // Метод для получения списка новостей
        public ObservableCollection<MyModelLibrary.News> GetNews()
        {
            try
            {
                // Создаем канал связи клиента с сервером
                using (client = new NewsService.NewsServiceClient())
                {
                    List<MyModelLibrary.News> list = client.GetNewsList();

                    // Если список новостей не пустой, то верни его
                    if (list != null)
                        return new ObservableCollection<MyModelLibrary.News>(list); // Получаем список новостей

                    return null; // Иначе верни пустой список
                }
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                MyDialog.ShowMessage("Сервер новостей не работает!");
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            // Если новостей нет, то верни null
            return null;
        }

        #endregion


        public NewsLogic()
        {
            MyDialog = new DialogService();
        }

    }
}
