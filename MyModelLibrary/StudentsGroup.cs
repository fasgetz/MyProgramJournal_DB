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



        public StudentsGroup()
        {
            this.Attendance = new List<Attendance>();
        }

        // Конструктор, который инициализирует свойства
        public StudentsGroup(int idStudent, int idGroup, List<Attendance> Attendance_list, Groups Groups)
        {
            this.IdStudent = IdStudent;
            this.idGroup = idGroup;
            this.Attendance = Attendance_list;
            this.Groups = Groups;
        }

        #region Свойства сущности Студент
        public int IdStudent { get; set; }
        public Nullable<int> idGroup { get; set; }

        public virtual List<Attendance> Attendance { get; set; }
        public Groups Groups { get; set; }
        //public virtual Users Users { get; set; }

        #endregion
    }
}
