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
    
    public partial class Discipline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discipline()
        {
            this.TeacherDisciplines = new HashSet<TeacherDisciplines>();
        }

        public Discipline(string NameDiscipline)
        {
            this.NameDiscipline = NameDiscipline;
        }

        public Discipline(int idDiscipline, string NameDiscipline)
        {
            this.idDiscipline = idDiscipline;
            this.NameDiscipline = NameDiscipline;
        }

        public int idDiscipline { get; set; }
        public string NameDiscipline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeacherDisciplines> TeacherDisciplines { get; set; }
    }
}
