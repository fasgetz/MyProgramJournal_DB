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
    
    public partial class FinalAttendances
    {
        public int idFinalAttendance { get; set; }
        public int idTeacherActivities { get; set; }
        public int idStudent { get; set; }
        public int? Mark { get; set; }


        public FinalAttendances()
        {

        }

        public FinalAttendances(int idTeacherActivities, int idStudent)
        {
            this.idTeacherActivities = idTeacherActivities;
            this.idStudent = idStudent;
        }

        public virtual GroupDisciplines GroupDisciplines { get; set; }
        public virtual StudentsGroup StudentsGroup { get; set; }
    }
}
