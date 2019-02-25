using MyProgramJournal_DB.MVVM.Interfaces;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MyProgramJournal_DB.MVVM
{
    class DialogService : IDialogService
    {
        private Window thisWindow; // Текущее открытое окно в сервисе

        #region Методы
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

                thisWindow = (Window)CreateWindow(Window); // Получаем ссылку на новое окно
                Window OldWindow = App.Current.MainWindow; // Ссылку на старое окно
                thisWindow.DataContext = OldWindow.DataContext; // Дата контекст передаем в новое окно

                OldWindow.Close(); // Закрываем старое окно
                thisWindow.Show(); // Открываем новое окно

                App.Current.MainWindow = thisWindow; // Новое окно делаем новым и основным окном


            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
            }
        }

        // Метод для закрытия текущего окна
        public void CloseWindow()
        {
            App.Current.MainWindow.Close(); // Закрываем главное окно
        }


        #endregion

    }
}
