﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accounts> accounts { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<FinalAttendances> FinalAttendances { get; set; }
        public virtual DbSet<GroupDisciplines> GroupDisciplines { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<LessonsDate> LessonsDate { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<OrderArchive> OrderArchive { get; set; }
        public virtual DbSet<OrderTypes> OrderTypes { get; set; }
        public virtual DbSet<SessionsAccounts> SessionsAccounts { get; set; }
        public virtual DbSet<Speciality_codes> Speciality_codes { get; set; }
        public virtual DbSet<StatusAccounts> StatusAccounts { get; set; }
        public virtual DbSet<StudentsGroup> StudentsGroup { get; set; }
        public virtual DbSet<TeacherDisciplines> TeacherDisciplines { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
    }
}
