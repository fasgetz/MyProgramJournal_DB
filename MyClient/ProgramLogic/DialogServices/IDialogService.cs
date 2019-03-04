using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.DialogServices
{

    /// <summary>
    /// Функционал для работы с диалоговыми окнами
    /// </summary>
    

    interface IDialogService
    {
        void ShowMessage(string message);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialog();  // открытие файла
        bool SaveFileDialog();  // сохранение файла


        
        MyModelLibrary.Images AddImage(MyModelLibrary.News MyNews); // Метод для загрузки изображения с компьютера
    }
}
