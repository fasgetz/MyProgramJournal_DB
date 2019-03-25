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

        public LessonsDate(int idTeacherActivities, int idLesson, DateTime DateLesson, int LessonNumber, List<Attendance> attendances)
            : this(idTeacherActivities, idLesson, DateLesson, LessonNumber)
        {
            this.Attendance = attendances;
        }

        public LessonsDate(int idTeacherActivities, int idLesson, DateTime DateLesson, int LessonNumber, string FIO, string DisciplineName)
            :this(idTeacherActivities, idLesson, DateLesson, LessonNumber)
        {
            this.TeacherFIO = FIO;
            this.DisciplineName = DisciplineName;
        }


        #region Свойства

        public string TeacherFIO { get; set; } // Фио учителя
        public string DisciplineName { get; set; } // Название дисциплины

        public int IdLesson { get; set; }
        public int IdTeacherActivities { get; set; }
        public System.DateTime DateLesson { get; set; }
        public int LessonNumber { get; set; }

        public List<Attendance> Attendance { get; set; } // Список оценок занятия
        //public virtual TeacherDisciplines TeacherDisciplines { get; set; }


        #endregion
    }
}
