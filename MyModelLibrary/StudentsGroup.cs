using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class StudentsGroup
    {


        #region Конструкторы

        public StudentsGroup()
        {

        }

        public StudentsGroup(int IdStudent, int idGroup)
        {
            this.IdStudent = IdStudent;
            this.idGroup = idGroup;
        }

        // Конструктор, который инициализирует свойства
        public StudentsGroup(int idStudent, int idGroup, int NumberInJournal)
        {
            this.IdStudent = IdStudent;
            this.idGroup = idGroup;
            this.NumberInJournal = NumberInJournal;
        }

        #endregion

        #region Свойства сущности Студент

        public int IdStudent { get; set; }
        public Nullable<int> idGroup { get; set; }
        public Nullable<int> NumberInJournal { get; set; }

        public virtual List<Attendance> Attendance { get; set; }
        public Groups Groups { get; set; }
        //public virtual Users Users { get; set; }
        public virtual List<FinalAttendances> FinalAttendances { get; set; } // Список итоговых оценок студента

        #endregion



    }
}
