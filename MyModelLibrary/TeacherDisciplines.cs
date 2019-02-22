using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class TeacherDisciplines
    {


        public TeacherDisciplines()
        {
            this.Attendance = new List<Attendance>();
            this.LessonsDate = new List<LessonsDate>();
        }

        // Конструктор для инициализации всех полей
        public TeacherDisciplines(int IdTeacher, int IdDiscipline, int IdGroup, int NumberSemester, List<Attendance> Attendance_list, List<LessonsDate> LessonsDateList, Discipline Discipline, Groups Groups)
        {
            this.IdTeacher = IdTeacher;
            this.IdDiscipline = IdDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
            this.Attendance = Attendance_list;
            this.LessonsDate = LessonsDateList;
            this.Discipline = Discipline;
            this.Groups = Groups;
        }

        public TeacherDisciplines(int IdTeacher, int IdDiscipline, int IdGroup, int NumberSemester)
        {
            this.IdTeacher = IdTeacher;
            this.IdDiscipline = IdDiscipline;
            this.IdGroup = IdGroup;
            this.NumberSemester = NumberSemester;
        }

        #region Свойства TeacherDisciplines
        public int IdTeacher { get; set; }
        public int IdDiscipline { get; set; }
        public int IdGroup { get; set; }
        public int NumberSemester { get; set; }


        public List<Attendance> Attendance { get; set; }
        public List<LessonsDate> LessonsDate { get; set; }
        public Discipline Discipline { get; set; }
        public Groups Groups { get; set; }
        //public virtual Users Users { get; set; }
        #endregion
    }
}
