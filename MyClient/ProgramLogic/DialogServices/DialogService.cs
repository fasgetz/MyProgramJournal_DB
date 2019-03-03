using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyClient.ProgramLogic.DialogServices
{

    /// <summary>
    /// Класс предоставляет методы для диалогов
    /// </summary>


    public class DialogService
    {

        #region Методы

        // Метод вызывает MessageBox с сообщением string
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

    }
}
