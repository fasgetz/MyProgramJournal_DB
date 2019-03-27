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

        public GroupDisciplines(int idTeacherActivities, int IdTeacherDiscipline, int IdGroup, int NumberSemester, string DiscipName)
        {
            this.idTeacherActivities = idTeacherActivities;
            this.IdTeacherDiscipline = IdTeacherDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
            this.DiscipName = DiscipName;
        }

        public GroupDisciplines(int idTeacherActivities, int IdTeacherDiscipline, int IdGroup, int NumberSemester, string DiscipName, string TeacherFIO)
        {
            this.idTeacherActivities = idTeacherActivities;
            this.IdTeacherDiscipline = IdTeacherDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
            this.DiscipName = DiscipName;
            this.TeacherFio = TeacherFIO;
        }

        public GroupDisciplines(int idTeacherActivities, int IdGroup, int NumberSemester, string DiscipName, string GroupName)
        {
            this.idTeacherActivities = idTeacherActivities;
            this.NumberSemester = NumberSemester;
            this.DiscipName = DiscipName;
            this.GroupName = GroupName;
        }


        public GroupDisciplines(string DiscipName, string GroupName, int IdTeacherActivities, int IdGroup, int NumberSemester)
        {
            this.DiscipName = DiscipName;
            this.GroupName = GroupName;
            this.idTeacherActivities = IdTeacherActivities;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
        }

        public GroupDisciplines(string DiscipName, string GroupName, int IdTeacherActivities, int IdGroup, int NumberSemester, string TeacherFIO)
        {
            this.DiscipName = DiscipName;
            this.GroupName = GroupName;
            this.idTeacherActivities = IdTeacherActivities;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
            this.TeacherFio = TeacherFIO;
        }
        #region Свойства

        public int idTeacherActivities { get; set; }
        public int IdTeacherDiscipline { get; set; }
        public int IdGroup { get; set; }
        public int NumberSemester { get; set; }

        // Дополнительные свойства
        public string DiscipName { get; set; }
        public string TeacherFio { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<FinalAttendances> FinalAttendances { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual TeacherDisciplines TeacherDisciplines { get; set; }
        public virtual ICollection<LessonsDate> LessonsDate { get; set; }

        #endregion
    }
}
