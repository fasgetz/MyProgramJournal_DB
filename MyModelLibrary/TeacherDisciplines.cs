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



        public TeacherDisciplines(int IdTeacherDiscipline, int IdTeacher, int IdDiscipline)
        {
            this.IdTeacherDiscipline = IdTeacherDiscipline;
            this.IdTeacher = IdTeacher;
            this.IdDiscipline = IdDiscipline;            
        }

        #region Свойства TeacherDisciplines

        public int IdTeacherDiscipline { get; set; }
        public int IdTeacher { get; set; }
        public int IdDiscipline { get; set; }

        public virtual Discipline Discipline { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GroupDisciplines> GroupDisciplines { get; set; }

        #endregion
    }
}
