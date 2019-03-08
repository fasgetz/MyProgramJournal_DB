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
