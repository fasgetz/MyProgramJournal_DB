using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{

    /// <summary>
    /// Класс предоставляет сущность Image
    /// </summary>

    [Serializable]
    public class Images
    {

        // Конструктор
        public Images(int IdNews, byte[] Image, string format_img, string ImagePath, string ImageName)
        {
            this.IdNews = IdNews;
            this.Image = Image;
            this.format_img = format_img;
            this.ImagePath = ImagePath;
            this.ImgName = ImageName;
        }


        #region Свойства

        public int IdImage { get; set; }
        public int IdNews { get; set; }
        public string ImgName { get; set; }
        public string ImagePath { get; set; }
        public byte[] Image { get; set; }
        public string format_img { get; set; }

        #endregion

    }
}
