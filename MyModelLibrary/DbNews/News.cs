using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary.DbNews
{

    /// <summary>
    /// Класс предоставляет сущность новостей
    /// </summary>

    public class News
    {
        public int IdNews { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime DatePost { get; set; }

        public List<Images> Images { get; set; } // Список изображений
    }
}
