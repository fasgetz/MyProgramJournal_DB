using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyProgramJournal_DB.MVVM.Interfaces
{
    interface IDialogService
    {

        void ShowMessage(string message);   // Вывод MessageBox с сообщением string

        void newWindow(string WindowName); // Создание нового окна

        Page newPage(string PageName); // Создание новой страницы
    }
}
