using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class Groups
    {
        public Groups()
        {
            //this.StudentsGroup = new List<StudentsGroup>();
            //this.TeacherDisciplines = new List<TeacherDisciplines>();
        }

        // Конструктор, который инициализирует группы
        public Groups(int idGroup, string GroupName)
        {
            this.idGroup = idGroup;
            this.GroupName = GroupName;
        }

        #region Свойства

        public int idGroup { get; set; }
        public string GroupName { get; set; }


        public virtual List<StudentsGroup> StudentsGroup { get; set; }
        public virtual List<TeacherDisciplines> TeacherDisciplines { get; set; }
        public virtual Speciality_codes Speciality_codes { get; set; } // Ссылка на код специальности

        #endregion
    }
}
