using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyClient.ProgramLogic.DialogServices
{

    /// <summary>
    /// Класс предоставляет функционал для работы с диалоговыми окнами
    /// </summary>


    public class DialogService : IDialogService
    {

        #region Свойства

        // Путь к файлу
        public string FilePath { get; set; }

        #endregion

        #region Методы

        // Открытие файла для IMG
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.jpg, *.png)|*.jpg;*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        // Сохранение файла
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }


        // Метод вызывает MessageBox с сообщением string
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

        #region Для работы сервисом новостей

        // Загрузка изображений
        public MyModelLibrary.Images AddImage(MyModelLibrary.News MyNews)
        {
            OpenFileDialog(); // Открываем диалог

            // Если изображение выбрано, то добавь его в список
            if (FilePath != null)
            {
                // конвертация изображения в байты
                byte[] imageData = null;
                FileInfo fInfo = new FileInfo(FilePath);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                imageData = br.ReadBytes((int)numBytes);

                // получение расширения файла изображения не забыв удалить точку перед расширением
                string iImageExtension = (Path.GetExtension(FilePath)).Replace(".", "").ToLower();

                // Получаем название изображения
                string name = (Path.GetFileName(FilePath));

                string TempPath = FilePath; // Создаем временную переменную для хранения пути изображения
                FilePath = null; // Обнуляем путь файла

                return new MyModelLibrary.Images(MyNews.IdNews, imageData, iImageExtension, TempPath, name); // Возврщаем изображение
            }
            // Иначе, если путь не выбран, то ничего не возвращай
            else
            {
                return null;
            }
        }

        #endregion

    }
}
