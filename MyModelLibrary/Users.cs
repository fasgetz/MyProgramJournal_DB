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
                    return "Мужчина";
                else if (Gender == "Ж")
                    return "Женщина";
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

        public string GetNumberPhone
        {
            get
            {
                if (NumberPhone is null)
                    return " "; // Сделано для поиска в DataGrid (Если ввели пробел)
                return NumberPhone;
            }
        }
        
        // Свойство для вывода ФИО + id
        public string GetFIO
        {
            get
            {
                return $"{Family} {Name} {Surname} - (id: {idUser})";
            }
        }

        // Свойства для простого вывода ФИО
        public string fio
        {
            get => $"{Family} {Name} {Surname}";
        }
        #endregion

        public Users(string Name, string Family, string Surname, string Gender, int UserStatus, string NumberPhone, DateTime? DateOfBirthday)
        {
            this.Name = Name;
            this.Family = Family;
            this.Surname = Surname;

            // Определяем гендер
            if (Gender == "Мужчина")
                this.Gender = "М";
            else
                this.Gender = "Ж";

            this.idUserStatus = UserStatus;
            this.NumberPhone = NumberPhone;
            this.DateOfBirthDay = DateOfBirthDay;
        }

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
