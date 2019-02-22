using MyProgramJournal_DB.MVVM.Interfaces;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MyProgramJournal_DB.MVVM
{
    class DialogService : IDialogService
    {
        // Вызываем новый MessageBox с сообщением string
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }



        // Ассемблирование для определения какую страницу/окно открывать из библиотеки классов
        public object CreateWindow(string fullClassName)
        {
            if (fullClassName != null)
            {
                Assembly asm = this.GetType().Assembly;

                object wnd = asm.CreateInstance(fullClassName);
                if (wnd == null)
                {
                    throw new TypeLoadException("Unable to create window: " + fullClassName);
                }
                return wnd;
            }

            return null;
        }

        // Создание новой страницы
        public Page newPage(string PageName)
        {
            try
            {
                string Window = "MyProgramJournal_DB.View." + PageName; // Ссылка на окно, которое будем создавать

                Page MyPage = (Page)CreateWindow(Window); // Ссылка на страницу
                MyPage.DataContext = this;
                if (MyPage != null)
                    return MyPage;
            }
            catch(Exception ex)
            {
                ShowMessage(ex.ToString());
            }

            return null;
        }

        // Создание нового окна
        public void newWindow(string WindowName)
        {
            try
            {
                string Window = "MyProgramJournal_DB.View." + WindowName; // Ссылка на окно, которое будем создавать

                Window NewWindow = (Window)CreateWindow(Window); // Получаем ссылку на новое окно
                Window OldWindow = App.Current.MainWindow; // Ссылку на старое окно
                NewWindow.DataContext = OldWindow.DataContext; // Дата контекст передаем в новое окно

                OldWindow.Close(); // Закрываем старое окно
                NewWindow.Show(); // Открываем новое окно

                App.Current.MainWindow = NewWindow; // Новое окно делаем новым и основным окном


            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }


        }

    }
}
