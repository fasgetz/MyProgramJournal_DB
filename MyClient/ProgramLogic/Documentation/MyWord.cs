using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ProgramLogic.Documentation
{
    /// <summary>
    /// Класс предоставляет логику работы с документами Word
    /// </summary>

    class MyWord
    {
        // Асинхронный метод, который сохраняет файл в Word
        public static async void CreateWordDoc(MyModelLibrary.OrderArchive Order)
        {
            // Если входной параметр не пустой, то приступи к сохранению Word файла
            if (Order != null)
            {
                await Task.Run(() =>
                {
                    // Устанавливаем директорию
                    DirectoryInfo dirInfo = new DirectoryInfo("OrdersDocuments");

                    // Создаем папку, если она не создана
                    if (!dirInfo.Exists)
                        dirInfo.Create();


                    Microsoft.Office.Interop.Word.Application winword =
                           new Microsoft.Office.Interop.Word.Application();

                    winword.Visible = false;

                    //Заголовок документа
                    winword.Documents.Application.Caption = "Мой документ Word";

                    object missing = System.Reflection.Missing.Value;

                    //Создание нового документа
                    Microsoft.Office.Interop.Word.Document document =
                        winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    // Создаем абзац заголовка.
                    Microsoft.Office.Interop.Word.Paragraph para = document.Paragraphs.Add(ref missing);
                    para.Range.Text = $"Приказ {Order.IdOrder}";
                    object style_name = "Заголовок 1";
                    para.Range.set_Style(ref style_name);
                    para.Range.InsertParagraphAfter();

                    // Добавить текст.
                    para.Range.Text = $"{Order.Commentary}";
                    para.Range.InsertParagraphAfter();

                    winword.Visible = false;


                    document.SaveAs2($"{Directory.GetCurrentDirectory()}/OrdersDocuments/Приказ {Order.IdOrder.ToString()}.docx", ref missing, ref missing, ref missing);
                    document.Close(ref missing, ref missing, ref missing);
                    winword.Quit(ref missing, ref missing, ref missing);

                    new DialogService().ShowMessage("Файл word успешно создан в папке OrdersDocuments");
                });
            }
        }
    }
}
