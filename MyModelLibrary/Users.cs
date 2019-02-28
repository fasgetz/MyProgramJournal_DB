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

        #region Для вывода в controls

        // Свойство, которое выдает статус
        public string GetStatus
        {
            get
            {

                switch (idUserStatus)
                {
                    case (1):
                        return "Студент";                        
                    case (2):
                        return "Преподаватель";
                    case (3):
                        return "Администратор";
                    default:
                        return null;
                }


            }
        }

        // Свойство которое выдает пол юзера в полной форме
        public string GetGender
        {
            get
            {
                if (Gender == "М")
                    return "Мужской";
                else if (Gender == "Ж")
                    return "Женский";
                return null;
            }
        }

        // Свойство, которое конвертирует nullable data в DateTime и выдает на выходе string
        public string GetDataTimeFormat
        {
            get
            {
                DateTime kek = Convert.ToDateTime(DateOfBirthDay);

                // Конвертируем nullable дату
                if (DateOfBirthDay != null)
                    return kek.ToString("D");
                else
                    return " "; // Сделано для поиска в DataGrid (Если ввели пробел)
            }
        }

        #endregion


        // Конструктор для инициализации свойств юзера
        public Users(int idUser, int? idUserStatus, string Name, string Family, string Surname, string Gender, string NumberPhone, DateTime? DateOfBirthday)
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
        public int? idUserStatus { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }

        private string _Surname;
        public string Surname
        {
            get
            {
                if (_Surname is null)
                    return " "; // Сделано для поиска в DataGrid (Если ввели пробел)
                return _Surname;
            }
            set
            {
                _Surname = value;
            }
        }       
        public string Gender { get; set; }

        private string _NumberPhone;
        public string NumberPhone
        {
            get
            {
                if (_NumberPhone is null)
                    return " "; // Сделано для поиска в DataGrid (Если ввели пробел)
                return _NumberPhone;
            }
            set
            {
                _NumberPhone = value;
            }
        }

        private DateTime? _DateOfBirthDay;
        public DateTime? DateOfBirthDay
        {
            get
            {
                return _DateOfBirthDay;
            }
            set
            {
                _DateOfBirthDay = value;
            }
        }


        //public virtual accounts accounts { get; set; }
        //public accounts myaccounts { get; set; }
        public StudentsGroup StudentsGroup { get; set; }
        public List<TeacherDisciplines> TeacherDisciplines { get; set; }
        public UserStatus UserStatus { get; set; }

        #endregion
    }
}
