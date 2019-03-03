using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class TeacherDisciplines
    {



        public TeacherDisciplines(int IdTeacherActivities, int IdTeacher, int IdDiscipline, int IdGroup, int NumberSemester)
        {
            this.IdTeacherActivities = IdTeacherActivities;
            this.IdTeacher = IdTeacher;
            this.IdDiscipline = IdDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
        }

        #region Свойства TeacherDisciplines

        public int IdTeacherActivities { get; set; }
        public int IdTeacher { get; set; }
        public int IdDiscipline { get; set; }
        public int IdGroup { get; set; }
        public int NumberSemester { get; set; }


        public List<LessonsDate> LessonsDate { get; set; }
        public Discipline Discipline { get; set; }
        public Groups Groups { get; set; }
        public Users User { get; set; }
        //public virtual Users Users { get; set; }
        #endregion
    }
}
