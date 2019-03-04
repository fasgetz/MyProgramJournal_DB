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

        #endregion

    }
}
