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
    
        public int IdLesson { get; set; }
        public int IdTeacherActivities { get; set; }
        public System.DateTime DateLesson { get; set; }
        public int LessonNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendance { get; set; }
        public virtual TeacherDisciplines TeacherDisciplines { get; set; }
    }
}
