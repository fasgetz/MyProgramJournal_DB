using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class LessonsDate
    {
        public LessonsDate()
        {
            //this.Attendance = new List<Attendance>();
        }


        // Конструктор
        public LessonsDate(int idTeacherActivities, int idLesson, DateTime DateLesson, int LessonNumber)
        {
            this.IdTeacherActivities = idTeacherActivities;
            this.IdLesson = idLesson;
            this.DateLesson = DateLesson;
            this.LessonNumber = LessonNumber;
        }


        #region Свойства

        public int IdLesson { get; set; }
        public int IdTeacherActivities { get; set; }
        public System.DateTime DateLesson { get; set; }
        public int LessonNumber { get; set; }

        //public virtual List<Attendance> Attendance { get; set; }
        //public virtual TeacherDisciplines TeacherDisciplines { get; set; }


        #endregion
    }
}
