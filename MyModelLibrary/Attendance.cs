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

        // Конструктор
        public Attendance(int idAttendance, int IdLesson, int StudentId, string Mark, int numberjournal)
            :this (idAttendance, IdLesson, StudentId, Mark)
        {
            this.numberjournal = numberjournal;
        }

        #region Свойства

        public int numberjournal { get; set; } // Для сортировки по номеру журнала

        public int idAttendance { get; set; }
        public int IdLesson { get; set; }
        public int StudentId { get; set; }
        public string Mark { get; set; }
        public Attendance MyAttendance { get; set; }

        #endregion

    }
}
