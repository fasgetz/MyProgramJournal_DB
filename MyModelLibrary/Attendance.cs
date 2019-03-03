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

        // Конструктор
        public Attendance(int idAttendance, int IdLesson, int StudentId, string Mark)
        {
            this.idAttendance = idAttendance;
            this.IdLesson = IdLesson;
            this.StudentId = StudentId;
            this.Mark = Mark;
        }

        #region Свойства

        public int idAttendance { get; set; }
        public int IdLesson { get; set; }
        public int StudentId { get; set; }
        public string Mark { get; set; }

        #endregion

    }
}
