using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{

    /// <summary>
    /// Класс предоставляет сущность новостей
    /// </summary>

    [Serializable]
    public class News
    {

        public News()
        {

        }


        public News(DateTime DatePost)
        {
            this.DatePost = DatePost;
        }    

        public News(int IdNews, string Title, string Content)
        {
            this.IdNews = IdNews;
            this.Title = Title;
            this.Content = Content;
        }



        public News(int IdNews, string Title, string Content, DateTime DatePost)
            :this(DatePost)
        {
            this.IdNews = IdNews;
            this.Title = Title;
            this.Content = Content;            
        }


        #region Свойства для контролов

        public string GetDate
        {
            get
            {
                return $"{DatePost}";
            }
        }

        #endregion

        #region Свойства сущности новость

        public int IdNews { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime DatePost { get; set; }

        public List<Images> Images { get; set; } // Список изображений

        #endregion
    }
}
