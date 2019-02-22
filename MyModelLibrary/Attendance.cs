using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class Attendance
    {

        public Attendance()
        {

        }

        // Конструктор с параметрами, в котором инициализируем наши свойства
        public Attendance(int idAttendance, int idTeacher, int idGroup, int IdDiscipline, int NumbSem, int StudentId, System.DateTime DateLesson, int LessonNumb, string mark)
        {
            this.idAttendance = idAttendance;
            this.IdTeacher = idTeacher;
            this.IdGroup = idGroup;
            this.IdDiscipline = IdDiscipline;
            this.NumberSemester = NumbSem;
            this.StudentId = StudentId;
            this.DateLesson = DateLesson;
            this.LessonNumber = LessonNumb;
            this.Mark = mark;
        }

        #region Список свойств

        public int idAttendance { get; set; }
        public int IdTeacher { get; set; }
        public int IdGroup { get; set; }
        public int IdDiscipline { get; set; }
        public Nullable<int> NumberSemester { get; set; }
        public int StudentId { get; set; }
        public Nullable<System.DateTime> DateLesson { get; set; }
        public Nullable<int> LessonNumber { get; set; }
        public string Mark { get; set; }

        //public virtual LessonsDate LessonsDate { get; set; }
        //public virtual StudentsGroup StudentsGroup { get; set; }
        //public virtual TeacherDisciplines TeacherDisciplines { get; set; }
        #endregion

    }
}
