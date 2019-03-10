using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{

    [Serializable]
    public class GroupDisciplines
    {


        public GroupDisciplines()
        {

        }

        public GroupDisciplines(int idTeacherActivities, int IdTeacherDiscipline, int IdGroup, int NumberSemester)
        {
            this.idTeacherActivities = idTeacherActivities;
            this.IdTeacherDiscipline = IdTeacherDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
        }


        #region Свойства

        public int idTeacherActivities { get; set; }
        public int IdTeacherDiscipline { get; set; }
        public int IdGroup { get; set; }
        public int NumberSemester { get; set; }

        public virtual ICollection<FinalAttendances> FinalAttendances { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual TeacherDisciplines TeacherDisciplines { get; set; }
        public virtual ICollection<LessonsDate> LessonsDate { get; set; }

        #endregion
    }
}
