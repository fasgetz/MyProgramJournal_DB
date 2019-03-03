using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Reflection;
using System.Windows;

namespace MyClient.ViewModel._Navigation
{
    // Класс предоставляет методы для навигации по страницам

    public class NavigateViewModel : ViewModelBase
    {
        public NavigateViewModel()
        {

        }

        public string Title { get; set; }


        #region Методы для навигации

        // Страничная навигация
        public void Navigate(string url)
        {
            Messenger.Default.Send(new NavigateArgs(url));
        }

        // Метод для открытия нового окна
        public void NewWindow(string WindowName)
        {
            try
            {
                Window thisWindow = (Window)CreateWindow(WindowName); // Получаем ссылку на новое окно
                Window OldWindow = App.Current.MainWindow; // Ссылку на старое окно

                //thisWindow.DataContext = OldWindow.DataContext; // Дата контекст передаем в новое окно
                //Messenger.Default.Send("kek");
                
                OldWindow.Close(); // Закрываем старое окно
                thisWindow.Show(); // Открываем новое окно

                App.Current.MainWindow = thisWindow; // Новое окно делаем новым и основным окном

            }
            catch (Exception ex)
            {
                DialogService dialog = new DialogService();
                dialog.ShowMessage(ex.ToString());
            }
        }

        // Метод для закрытия текущего окна
        public void CloseWindow()
        {
            App.Current.MainWindow.Close(); // Закрываем главное окно
        }

        #endregion

        #region Вспомогательные методы

        // Ассемблирование для определения какое окно открывать из библиотеки
        private object CreateWindow(string fullClassName)
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

        #endregion
    }
}
