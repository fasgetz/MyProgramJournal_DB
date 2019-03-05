using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.Exceptions;

namespace WCF_Service.ServiceLogic
{
    class ServiceNewsLogic
    {

        #region Вспомогательные методы

        // Метод, который проверяет статус юзера в базу данных и возвращает его
        private int CheckUserStatus(int idAccount)
        {
            // Возвращаем статус юзера из базы данных
            return Convert.ToInt32(new MyDB().Users.FirstOrDefault(i => i.idUser == idAccount).idUserStatus);
        }

        #endregion


        #region Методы для контракта служб

        // Метод удаления новости
        public bool RemoveNews(int IdNews, MyModelLibrary.accounts MyAcc)
        {
            // Если статус юзера == администратор, то приступи к удалению новости
            if (CheckUserStatus(MyAcc.idAccount) == 3)
            {
                try
                {
                    // Создаем канал связи с бд
                    using (DbNews db = new DbNews())
                    {
                        // Ищем новость которую надо удалять по айди
                        db.News.Remove(db.News.FirstOrDefault(i => i.IdNews == IdNews));
                        Console.WriteLine($"Пользователь {MyAcc.login} удалил новость id: {IdNews}");

                        return true; // Возвращаем истину (Новость удалена)
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return false; // Новость не удалена
        }

        // Метод для добавления новости
        public bool AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {
            // Если статус юзера == администратор, то приступи к созданию новости
            if (CheckUserStatus(MyAcc.idAccount) == 3)
            {
                // Если новость не пустая, то приступи к добавлению новости в бд
                if (MyNews != null)
                {
                    // Создаем новость, которую поместим в бд
                    News News = new News(MyNews.Title, MyNews.Content);

                    // Если список картинок не пустой, то
                    if (MyNews.Images != null)
                    {
                        try
                        {
                            // Количество изображений
                            List<Images> list = new List<Images>();

                            foreach (var item in MyNews.Images)
                            {
                                list.Add(new Images(News.IdNews, item.Image, item.format_img));
                            }

                            News.Images = list; // Добавляем новости картинки

                            using (DbNews db = new DbNews())
                            {
                                db.News.Add(News);
                                db.SaveChanges();
                                Console.WriteLine("Новость добавлена успешно!");
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }
                }
            }
            // Иначе, если юзер != администратор, то выдай ошибку
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return false;
        }


        // Метод, который создаст список новостей и вернет юзеру
        public List<MyModelLibrary.News> GetNews()
        {
            // Создаем подключение к бд
            using (DbNews db = new DbNews())
            {

                // Всего новостей 
                Console.WriteLine($"Всего новостей: {db.News.Count()}");

                // Если новостей > 0, то верни список
                if (db.News.Count() > 0)
                {
                    // Создаем DTO - список, который вернем юзеру
                    List<MyModelLibrary.News> NewsList = new List<MyModelLibrary.News>();

                    // Генерируем DTO - объекты
                    foreach (var item in db.News)
                    {
                        MyModelLibrary.News News = new MyModelLibrary.News(item.IdNews, item.Title, item.Content, item.DatePost);

                        // Если изображений больше 0, то добавь их к новостям
                        if (item.Images.Count() != 0)
                        {
                            News.Images = new List<MyModelLibrary.Images>();

                            // Перебираем список изображений
                            foreach (var img in item.Images)
                            {
                                // Добавляем изображение в список
                                News.Images.Add(new MyModelLibrary.Images(News.IdNews, img.Image, img.format_img));
                            }

                            Console.WriteLine($"Изображений у новости {News.IdNews}: {News.Images.Count}");
                        }

                        NewsList.Add(News); // Добавляем новость в dto - список
                    }


                    return NewsList; // Возвращаем dto список
                }
            }

            // Если новостей нет, то верни пустой список
            return null;
        }

        #endregion

    }
}
