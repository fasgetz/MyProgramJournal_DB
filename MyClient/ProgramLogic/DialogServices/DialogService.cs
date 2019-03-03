using Microsoft.Win32;
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
    /// Класс предоставляет функционал для работы с диалоговыми окнами
    /// </summary>


    public class DialogService : IDialogService
    {

        #region Методы

        // Путь к файлу
        public string FilePath { get; set; }

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

    }
}
