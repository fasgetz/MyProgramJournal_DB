using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.Exceptions;

namespace WCF_Service.ServiceLogic
{

    /// <summary>
    /// Вспомогательный класс для TransferServiceLogic
    /// </summary>

    internal class HelpersForTransferService
    {

        #region Вспомогательные методы

        // Метод, который проверяет статус юзера в базу данных и возвращает его
        internal static int CheckUserStatus(int idAccount)
        {
            // Возвращаем статус юзера из базы данных
            return Convert.ToInt32(new MyDB().Users.FirstOrDefault(i => i.idUser == idAccount).idUserStatus);
        }

        // Метод, который проверяет аккаунт на соответствующий статус idStatusCheck и возвращает true, если статус соответствует
        internal static bool CheckStatus(MyModelLibrary.accounts MyAcc, int idStatusCheck)
        {
            // Если входные данные не пусты, то приступи к проверке
            if (MyAcc != null)
            {
                // Если пользователь, который послал запрос является администратором, то приступи к получению списка преподавателей
                if (CheckUserStatus(MyAcc.idAccount) == idStatusCheck)
                {
                    return true; // Возвращаем true, если проверка на статус удалась
                }
                // Если тот, кто послал запрос не является администратором, то отправь ему экзепшн
                else
                {
                    ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
                }
            }
            else
            {
                ExceptionSender.SendException("Вы отправили пустые входные данные юзера!");
            }

            return false; // false, если проверка неудачна
        }

        #endregion

    }
}
