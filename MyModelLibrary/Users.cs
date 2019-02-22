using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyModelLibrary
{
    [Serializable]
    public class Users
    {
        public Users()
        {
            //this.TeacherDisciplines = new List<TeacherDisciplines>();
        }


        // Конструктор для инициализации свойств юзера
        public Users(int idUser, int idUserStatus, string Name, string Family, string Surname, string Gender, string NumberPhone, System.DateTime DateOfBirthday)
        {
            this.idUser = idUser;
            this.idUserStatus = idUserStatus;
            this.Name = Name;
            this.Family = Family;
            this.Surname = Surname;
            this.Gender = Gender;
            this.NumberPhone = NumberPhone;
            this.DateOfBirthDay = DateOfBirthDay;
        }


        //// Конструктор для инициализации свойств юзера
        //public Users(int idUser, int idUserStatus, string Name, string Family, string Surname, string Gender, string NumberPhone, System.DateTime DateOfBirthday, UserStatus UserStatus, StudentsGroup StudentGroups, List<TeacherDisciplines> teacherDisciplines)
        //{
        //    this.idUser = idUser;
        //    this.idUserStatus = idUserStatus;
        //    this.Name = Name;
        //    this.Family = Family;
        //    this.Surname = Surname;
        //    this.Gender = Gender;
        //    this.NumberPhone = NumberPhone;
        //    this.DateOfBirthDay = DateOfBirthDay;
        //    this.UserStatus = UserStatus;
        //    this.StudentsGroup = StudentsGroup;
        //    this.TeacherDisciplines = TeacherDisciplines;
        //}

        #region Список свойств Юзера

        public int idUser { get; set; }
        public Nullable<int> idUserStatus { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string NumberPhone { get; set; }
        public Nullable<System.DateTime> DateOfBirthDay { get; set; }


        //public virtual accounts accounts { get; set; }
        //public accounts myaccounts { get; set; }
        public StudentsGroup StudentsGroup { get; set; }
        public List<TeacherDisciplines> TeacherDisciplines { get; set; }
        public UserStatus UserStatus { get; set; }

        #endregion
    }
}
