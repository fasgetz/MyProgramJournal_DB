using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.ServiceLogic
{
    /// <summary>
    /// Класс предоставляет логику работы с сервером (Общая логика)
    /// </summary>

    internal class CommonLogic
    {
        protected MyService.TransferServiceClient client; // Ссылка на клиент
        protected DialogService MyDialog; // Для работы с диалогами

        #region Общие методы

        // Метод на получение списка занятий группы по дате
        public List<MyModelLibrary.LessonsDate> GetLessons(MyModelLibrary.Groups Group, DateTime date)
        {
            try
            {
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetLessons(Group, date);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null; // Возвращаем null, если нету занятий у группы за указанную дату
        }

        // Метод получения списка студентов (Если запрос послал администратор или преподаватель)
        internal List<MyModelLibrary.Users> GetStudentsInGroup(MyModelLibrary.accounts MyAcc, int idGroup)
        {
            try
            {
                // Создаем подключение клиента к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // Получаем список студентов по айди группы с сервера
                    var list = client.GetStudentsGroup(MyAcc, idGroup);

                    // Если список студентов не пустой, то возвращаем список
                    if (list != null)
                        return list; // Возвращаем клиенту список
                }
            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            // Возвращаем null, если не удалось получить список с сервера или возникла какая-нибудь ошибка
            return null;
        }

        // Метод получения списка всех групп
        internal List<MyModelLibrary.Groups> GetGroups()
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    return client.GetGroups().ToList();
                }
            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            // Возвращаем null, если список не удалось получить с сервера или какая-то ошибка возникла
            return null;
        }

        // Метод получения списка всех специальностей
        internal List<MyModelLibrary.Speciality_codes> GetSpecialityCodes()
        {
            try
            {
                // Создаем подключение к серверу
                using (client = new MyService.TransferServiceClient())
                {
                    // возвращаем список специальностей
                    return client.GetSpecialityCodes().ToList();
                }
            }
            catch(Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            // Возвращаем null, если список не удалось получить с сервера или какая-то ошибка возникла
            return null;
        }

        #endregion

        internal CommonLogic()
        {
            MyDialog = new DialogService(); // Инициализируем диалог
        }



    }
}
