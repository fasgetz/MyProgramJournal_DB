using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class Discipline
    {
        public Discipline()
        {
            //this.TeacherDisciplines = new List<TeacherDisciplines>();
        }

        public Discipline(string NameDiscipline)
        {
            this.NameDiscipline = NameDiscipline;
        }

        public Discipline(int idDiscipline, string NameDiscipline)
        {
            this.idDiscipline = idDiscipline;
            this.NameDiscipline = NameDiscipline;
        }

        #region Свойства

        public int idDiscipline { get; set; }
        public string NameDiscipline { get; set; }

        //public virtual List<TeacherDisciplines> TeacherDisciplines { get; set; }
        #endregion

    }
}
