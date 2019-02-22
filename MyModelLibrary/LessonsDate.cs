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


        // Конструктор, который инициализирует поля по параметрам
        public LessonsDate(System.DateTime DateLesson, int LessonNumber, int IdTeacher, int IdGroup, int IdDiscipline, int NumberSemester)
        {
            this.DateLesson = DateLesson;
            this.LessonNumber = LessonNumber;
            this.IdTeacher = IdTeacher;
            this.IdGroup = IdGroup;
            this.IdDiscipline = IdDiscipline;
            this.NumberSemester = NumberSemester;
        }
        #region Свойства

        public System.DateTime DateLesson { get; set; }
        public int LessonNumber { get; set; }
        public int IdTeacher { get; set; }
        public int IdGroup { get; set; }
        public int IdDiscipline { get; set; }
        public int NumberSemester { get; set; }

        //public virtual List<Attendance> Attendance { get; set; }
        //public virtual TeacherDisciplines TeacherDisciplines { get; set; }


        #endregion
    }
}
