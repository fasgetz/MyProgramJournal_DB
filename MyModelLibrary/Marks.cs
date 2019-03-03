using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{

    [Serializable]
    public class Marks
    {
        // Конструктор
        public Marks(string Mark)
        {
            this.Mark = Mark;
        }

        #region Свойства

        public string Mark { get; set; }

        #endregion
    }
}
