//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCF_Service.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class LessonsDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonsDate()
        {
            this.Attendance = new HashSet<Attendance>();
        }
    
        public System.DateTime DateLesson { get; set; }
        public int LessonNumber { get; set; }
        public int IdTeacher { get; set; }
        public int IdGroup { get; set; }
        public int IdDiscipline { get; set; }
        public int NumberSemester { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual TeacherDisciplines TeacherDisciplines { get; set; }
    }
}
