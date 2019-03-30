using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{


    /// <summary>
    /// Сущность итоговых оценок студента
    /// </summary>

    [Serializable]
    public class FinalAttendances
    {

        public FinalAttendances()
        {

        }

        public FinalAttendances(int idFinalAttendance, int idTeacherArivities, int idStudent, int? Mark)
        {
            this.idFinalAttendance = idFinalAttendance;
            this.idTeacherActivities = idTeacherActivities;
            this.idStudent = idStudent;
            this.Mark = Mark;
        }

        public FinalAttendances(int idFinalAttendance, int idTeacherArivities, int idStudent, int? Mark, int NumberJournal)
            :this(idFinalAttendance, idTeacherArivities, idStudent, Mark)
        {
            this.numberjournal = NumberJournal;
        }


        #region

        public int numberjournal { get; set; } // Для сортировки по номеру журнала

        public int idFinalAttendance { get; set; }
        public int idTeacherActivities { get; set; }
        public int idStudent { get; set; }
        public int? Mark { get; set; }

        public virtual StudentsGroup StudentsGroup { get; set; }
        public virtual TeacherDisciplines TeacherDisciplines { get; set; }

        #endregion

    }
}
