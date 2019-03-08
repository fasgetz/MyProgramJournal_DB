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


        public Groups(string GroupName, int idSpeciality)
        {
            this.GroupName = GroupName;
            this.idSpeciality = idSpeciality;
        }

        public Groups(int idGroup, string GroupName, int idSpeciality)
        {
            this.idGroup = idGroup;
            this.GroupName = GroupName;
            this.idSpeciality = idSpeciality;
        }



        #region Свойства

        public int idGroup { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> idSpeciality { get; set; }
        public int StudentsCount { get; set; } // Количество студентов в группе


        public virtual List<StudentsGroup> StudentsGroup { get; set; }
        public virtual List<TeacherDisciplines> TeacherDisciplines { get; set; }
        public virtual Speciality_codes Speciality_codes { get; set; } // Ссылка на код специальности

        #endregion
    }
}
