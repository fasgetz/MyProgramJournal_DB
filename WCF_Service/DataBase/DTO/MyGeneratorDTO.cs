using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.DataBase.DTO
{
    // Вспомогательный класс, который преобразует прокси-объекты в DTO - объекты, для передачи данных.
    public class MyGeneratorDTO
    {
        #region Методы для получения DTO объектов

        // Метод преобразования DTO списка приказов
        public List<MyModelLibrary.OrderArchive> GetOrdersDTO(List<OrderArchive> NotDTOList)
        {
            // Создаем DTO List, если NotDTOList не пустой
            if (NotDTOList != null)
            {
                // Создаем DTO список
                List<MyModelLibrary.OrderArchive> list = new List<MyModelLibrary.OrderArchive>();

                // Добавляем в DTO список DTO объекты
                foreach (var item in NotDTOList)
                {
                    list.Add(new MyModelLibrary.OrderArchive(item.IdOrder, item.Date, item.Commentary, Convert.ToInt16(item.IdOrderType), new MyModelLibrary.OrderTypes(item.OrderTypes.IdOrderType, item.OrderTypes.OrderName)));
                }
                    

                return list; // Возвращаем DTO список
            }                

            return null; // Возвращаем null, если передали пустой список
        }

        #region Методы, которые используют преподаватели и администраторы

        // Метод который преобразует NotDTO список в DTO список
        public List<MyModelLibrary.FinalAttendances> GetFinalAttendances(List<FinalAttendances> notdtolist)
        {
            // Если итемов в списке > 0, то выполни преобразование и верни пользователю DTO список
            if (notdtolist.Count != 0)
            {
                int group = Convert.ToInt16(notdtolist.FirstOrDefault().StudentsGroup.idGroup);

                // Находим всех студентов одним запросов (Чтобы узнать его номер по журналу)
                var students = new MyDB().StudentsGroup.Where(i => i.idGroup == group);

                // Создаем список
                List<MyModelLibrary.FinalAttendances> list = new List<MyModelLibrary.FinalAttendances>();

                // Перебираем список и создаем dto объект, который вносим в dto список
                foreach (var item in notdtolist)
                {
                    list.Add(new MyModelLibrary.FinalAttendances(item.idFinalAttendance, item.idTeacherActivities, item.idStudent, item.Mark,
                                            Convert.ToInt16(students.FirstOrDefault(i => i.IdStudent == item.idStudent).NumberInJournal),
                                            students.FirstOrDefault(i => i.IdStudent == item.idStudent).Users.GetFIO));

                }

                // Возвращаем dto список, отсортированный по номеру в журнале
                return list.OrderBy(i => i.numberjournal).ToList();
            }

            return null; // Возвращаем null, если список пустой (Или в случае неудачи)
        }

        // Метод который преобразует NotDTO список в DTO список
        public List<MyModelLibrary.GroupDisciplines> GetTeacherDiscipline(List<GroupDisciplines> NotDTOList)
        {
            List<MyModelLibrary.GroupDisciplines> list = new List<MyModelLibrary.GroupDisciplines>();

            foreach (var item in NotDTOList)
            {
                list.Add(new MyModelLibrary.GroupDisciplines(item.TeacherDisciplines.Discipline.NameDiscipline, item.Groups.GroupName,
                    item.idTeacherActivities, item.IdGroup, item.NumberSemester, new MyDB().Users.FirstOrDefault(i => i.idUser == item.TeacherDisciplines.IdTeacher).GetFIO
                    ));
            }
            return list;
        }
        
        public List<MyModelLibrary.LessonsDate> GetTeacherLessons(List<LessonsDate> NotDTOList)
        {
            // Если передали не пустой список, то выполни алгоритм преобразования в DTO список
            if (NotDTOList.Count != 0)
            {
                // Номер группы
                int group = NotDTOList.FirstOrDefault().GroupDisciplines.IdGroup;

                // Находим список студентов одним запросом, чтобы потом не обращаться. Необходимо, чтобы определить номер журнала студента
                using (MyDB db = new MyDB())
                {
                    // Находим всех студентов одним запросов (Чтобы узнать его номер по журналу)
                    var students = new MyDB().StudentsGroup.Where(i => i.idGroup == group);

                    // Создаем DTO список
                    List<MyModelLibrary.LessonsDate> list = new List<MyModelLibrary.LessonsDate>();

                    // Перебираем не DTO список и преобразуем итемы в DTO
                    foreach (var item in NotDTOList)
                    {
                        // Создаем список оценок
                        List<MyModelLibrary.Attendance> attendances = new List<MyModelLibrary.Attendance>();

                        // Перебираем все оценки занятия и добавляем в новый DTO список
                        foreach (var kek in item.Attendance)
                        {
                            // Добавляем итем в DTO список
                            attendances.Add(new MyModelLibrary.Attendance(kek.idAttendance, kek.IdLesson, kek.StudentId, kek.Mark,
                                Convert.ToInt16(students.FirstOrDefault(i => i.IdStudent == kek.StudentId).NumberInJournal)));
                        }

                        // Сортируем оценку по номеру журнала
                        attendances = attendances.OrderBy(i => i.numberjournal).ToList(); // Сортируем по номеру журналу оценки

                        // Добавляем итем в DTO список
                        list.Add(new MyModelLibrary.LessonsDate(item.IdTeacherActivities, item.IdLesson, item.DateLesson, item.LessonNumber,
                            attendances));

                    }

                    // Возвращаем DTO список (Отсортированный)
                    return list.OrderBy(i => i.DateLesson).ThenBy(i => i.LessonNumber).ToList();
                }
            }
            // Иначе, если список пустой, то верни null
            else
                return null;
        }

        #endregion

        // Метод для получения DTO - Список LessonsDate + Готово
        public List<MyModelLibrary.LessonsDate> GetLessonsDates(List<LessonsDate> lessonsDates)
        {
            // Создаем новый список, который будем возвращать
            List<MyModelLibrary.LessonsDate> list = new List<MyModelLibrary.LessonsDate>();

            // Если список lessonsDates не пустой, то добавь в него
            if (lessonsDates.Count() > 0)
            {
                // Перебираем все lessonsDates и присваиваем в новый список
                foreach (var item in lessonsDates)
                {
                    list.Add(new MyModelLibrary.LessonsDate(item.IdTeacherActivities, item.IdLesson, item.DateLesson, item.LessonNumber,
                        new MyDB().Users.FirstOrDefault(i => i.idUser == item.GroupDisciplines.TeacherDisciplines.IdTeacher).GetFIO,
                        item.GroupDisciplines.TeacherDisciplines.Discipline.NameDiscipline));
                }
            }

            return list.ToList(); // Верни список
        }

        // Метод для получения DTO - список TeacherDisciplines + Готово
        private List<MyModelLibrary.TeacherDisciplines> GetTeacherDisciplines(List<TeacherDisciplines> list)
        {
            // Создаем новый список, который будем возвращать
            List<MyModelLibrary.TeacherDisciplines> newlist = new List<MyModelLibrary.TeacherDisciplines>();

            //  Если дисциплин > 0, то перебираем все ценки и присваиваем в новый список
            if (list.Count() > 0)
            {
                // Перебираем все дисциплины и присваиваем в новый список
                foreach (var item in list)
                {
                    //var a = GetAttendanceList(item.Attendance);
                    newlist.Add(new MyModelLibrary.TeacherDisciplines
                        (
                        item.IdTeacherDiscipline,
                        item.IdTeacher,
                        item.IdDiscipline
                        ));
                }
            }

            return newlist.ToList();
        }

        // Метод для получения DTO - Специальности (Применяется в методе GetGroups)
        public MyModelLibrary.Speciality_codes GetSpeciality(Speciality_codes speciality)
        {
            return new MyModelLibrary.Speciality_codes(speciality.idSpeciality, speciality.code_speciality, speciality.name_speciality);
        }

        // Метод для получения DTO - группа + Готова
        public MyModelLibrary.Groups GetGroups(Groups Groups)
        {
            // Создаем новый объет Группы, которую будем возвращать
            MyModelLibrary.Groups MyGroup = new MyModelLibrary.Groups(Groups.idGroup, Groups.GroupName, Convert.ToInt16(Groups.idSpeciality));
            MyGroup.StudentsCount = Groups.StudentsGroup.Count();

            // Если группе присвоили специальность, то присвой ее к dto объекту
            if (Groups.Speciality_codes != null)
            {
                MyGroup.Speciality_codes = GetSpeciality(Groups.Speciality_codes);
            }


            return MyGroup; // Возвращаем группу
        }

        // Метод для получения DTO - Discipline + Готов
        public MyModelLibrary.Discipline GetDiscipline(Discipline Discipline)
        {
            MyModelLibrary.Discipline MyDiscipline = new MyModelLibrary.Discipline(Discipline.idDiscipline, Discipline.NameDiscipline);

            return MyDiscipline;
        }

        // Метод для получения DTO - объекта Список оценок + Готов
        private List<MyModelLibrary.Attendance> GetAttendanceList(List<Attendance> old_list)
        {
            // Объявляем новый список, который будем возвращать
            List<MyModelLibrary.Attendance> MyAttendance = new List<MyModelLibrary.Attendance>();

            //  Если оценок > 0, то перебираем все ценки и присваиваем в новый список
            if (old_list.Count() > 0)
            {
                // Перебираем все оценки и присваиваем в новый список
                foreach (var item in old_list)
                {
                    MyAttendance.Add(new MyModelLibrary.Attendance(item.idAttendance, item.IdLesson, item.StudentId, item.Mark));
                }
            }
            return MyAttendance; // Возвращаем список
        }

        // Метод для получения DTO - объекта Студент группы Готов
        private MyModelLibrary.StudentsGroup GetStudentsGroup(StudentsGroup studentsGroup)
        {

            // Создаем объект DTO и инициализируем его
            MyModelLibrary.StudentsGroup StudentGroup = new MyModelLibrary.StudentsGroup
                (
                studentsGroup.IdStudent,
                Convert.ToInt16(studentsGroup.idGroup),
                Convert.ToInt16(studentsGroup.NumberInJournal)
                );

            return StudentGroup; // Возвращаем объект
        }

        // Метод для получения DTO - оъекта статуса юзера + Готов
        private MyModelLibrary.UserStatus GetUserStatus(UserStatus UserStatus)
        {
            // Создаем новый DTO - объект и инициализируем его
            MyModelLibrary.UserStatus UserStatusDTO = new MyModelLibrary.UserStatus(UserStatus.idUserStatus, UserStatus.StatusUser);

            return UserStatusDTO; // Возвращаем новый DTO - объект
        }

        //// Метод для получения DTO - объекта статуса аккаунта + Готов
        private MyModelLibrary.StatusAccounts GetStatusAcc(StatusAccounts status)
        {
            // Объявляем новый статус аккаунта и инициализируем его свойства
            MyModelLibrary.StatusAccounts NewStatus = new MyModelLibrary.StatusAccounts(status.idStatus, status.StatusAccount);

            return NewStatus; // Возвращаем 
        }

        // Метод для получения DTO - списка сессий аккаунта
        public List<MyModelLibrary.SessionsAccounts> GetSessionsAcc(List<SessionsAccounts> sessions)
        {
            // Объявляем новый список
            List<MyModelLibrary.SessionsAccounts> SessionsDTO = new List<MyModelLibrary.SessionsAccounts>();

            // Перебираем все сессии из списка сессий и присваиваем в SessionsDTO
            foreach (var item in sessions)
            {
                // Добавляем сессию в список
                SessionsDTO.Add(new MyModelLibrary.SessionsAccounts(item.idSession, item.idAccount, Convert.ToDateTime(item.StartLogin), Convert.ToDateTime(item.EndLogin)));
            }

            return SessionsDTO.ToList(); // Возвращаем список сессий
        }

        // Метод для получения DTO - Персональные данные юзера
        public MyModelLibrary.Users GetUser(Users user)
        {
            if (user != null)
            {
                // Объявляем нового юзера и инициализируем его
                MyModelLibrary.Users UserDTO = new MyModelLibrary.Users
                    (
                    user.idUser,
                    Convert.ToInt16(user.idUserStatus),
                    user.Name,
                    user.Family,
                    user.Surname,
                    user.Gender,
                    user.NumberPhone,
                    user.DateOfBirthDay
                    //GetUserStatus(user.UserStatus),
                    //GetStudentsGroup(user.StudentsGroup),
                    //GetTeacherDisciplines(user.TeacherDisciplines.ToList())
                    );

                // Присваиваем статус юзера
                UserDTO.UserStatus = GetUserStatus(user.UserStatus);

                // Если этот юзер студент, то присвой ему группу
                if (user.StudentsGroup != null)
                {
                    UserDTO.StudentsGroup = GetStudentsGroup(user.StudentsGroup);
                }
                

                // Конвертируем nullable дату
                if (user.DateOfBirthDay != null)
                    UserDTO.DateOfBirthDay = Convert.ToDateTime(user.DateOfBirthDay);

                return UserDTO; // Возвращаем DTO - юзера
            }


            return null;

        }

        // Метод для получения DTO - Список дисциплин учителя
        private List<MyModelLibrary.GroupDisciplines> GetGroupDisciplines(Groups group)
        {
            List<MyModelLibrary.GroupDisciplines> MyList = new List<MyModelLibrary.GroupDisciplines>();

            // Если список дисциплин у группы > 0, то добавь их
            if (group.GroupDisciplines.Count > 0)
            {
                // Необходимо проинициализировать какие дисциплины ведутся у группы
                foreach (var item in group.GroupDisciplines)
                {
                    MyModelLibrary.GroupDisciplines teacherDisciplines = new MyModelLibrary.GroupDisciplines
                        (
                            item.idTeacherActivities,
                            item.IdTeacherDiscipline,
                            item.IdGroup,
                            item.NumberSemester
                        );
                                        
                    //// Теперь добавляем итоговые отметки студентов по дисциплине (Если они есть)
                    //if (item.FinalAttendances != null)
                    //{
                    //    // Создаем список итоговых оценок по дисциплине
                    //    List<MyModelLibrary.FinalAttendances> AttendancesList = new List<MyModelLibrary.FinalAttendances>();

                    //    // Перебираем все элементы
                    //    foreach (var attendances in item.FinalAttendances)
                    //    {
                    //        AttendancesList.Add(new MyModelLibrary.FinalAttendances(attendances.idFinalAttendance, attendances.idTeacherActivities, attendances.idStudent));
                    //    }

                    //    // Присваиваем список итоговых оценок дисциплине учителя
                    //    teacherDisciplines.FinalAttendances = AttendancesList;
                    //}
                    

                    MyList.Add(teacherDisciplines);
                }


            }


            return MyList.ToList();
        }        

        // Метод, который генерирует DTO - объект типа accounts и возвращает его + Готов
        public MyModelLibrary.accounts GetAccountDTO(accounts AccountNotDTO)
        {

            try
            {
                // Объявляем аккаунт и инициализируем в нем данные
                MyModelLibrary.accounts Account = new MyModelLibrary.accounts
                    (
                        AccountNotDTO.idAccount,
                        AccountNotDTO.idStatus,
                        AccountNotDTO.login,
                        AccountNotDTO.password,
                        Convert.ToDateTime(AccountNotDTO.DateRegistration)
                    );

                Account.StatusAccounts = GetStatusAcc(AccountNotDTO.StatusAccounts); // Присваиваем данные о статусе аккаунта юзера

                // Далее делаем проверку: если аккаунту создали учетные данные, то присвой их к нему
                if (AccountNotDTO.Users != null)
                {
                    Account.Users = GetUser(AccountNotDTO.Users); // Присваиваем юзеру его данные

                    // Если юзер == 1 (Если юзер == студент, который имеет группу, то проинициализируй дальше
                    if (AccountNotDTO.Users.idUserStatus == 1 && AccountNotDTO.Users.StudentsGroup != null)
                    {                        
                        Account.Users.StudentsGroup = GetStudentsGroup(AccountNotDTO.Users.StudentsGroup); // Присвой принадлежность студента к группе
                        Account.Users.StudentsGroup.Attendance = GetAttendanceList(AccountNotDTO.Users.StudentsGroup.Attendance.ToList()); // Присвой список оценок студента
                        Account.Users.StudentsGroup.FinalAttendances = GetFinalAttendances(AccountNotDTO.Users.StudentsGroup.FinalAttendances.ToList()); // Список итоговых оценок

                        // Если студент состоит в группе, то добавь ее
                        if (AccountNotDTO.Users.StudentsGroup.Groups != null)
                            Account.Users.StudentsGroup.Groups = new MyModelLibrary.Groups
                                (
                                AccountNotDTO.Users.StudentsGroup.Groups.GroupName,
                                Convert.ToInt16(AccountNotDTO.Users.StudentsGroup.Groups.idSpeciality
                                ));
                        //Account.Users.StudentsGroup.Groups.TeacherDisciplines = GetGroupDisciplines(AccountNotDTO.Users.StudentsGroup.Groups); // Присваиваем список дисциплин группе студента       

                    }
                    // Если юзер == 2 (Если юзер == преподаватель, который имеет дисциплины, то проинициализируй дальше
                    if (Account.Users.idUserStatus == 2 && AccountNotDTO.Users.TeacherDisciplines != null)
                    {
                        Account.Users.StudentsGroup = null; // StudentsGroup == null (Если юзер преподаватель, то он не состоит ни в какой группе)
                        Account.Users.TeacherDisciplines = GetTeacherDisciplines(AccountNotDTO.Users.TeacherDisciplines.ToList()); // Получаем дисциплины учителя

                    }
                    // Если юзер == 3 (Если юзер == администратор), то значит, что нет смысла инициализировать все данные, кроме юзера
                    if (Account.Users.idUserStatus == 3)
                    {
                        Account.Users.StudentsGroup = null;
                        Account.Users.TeacherDisciplines = null;
                    }


                }


                return Account; // Возвращаем аккаунт
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Аккаунт{AccountNotDTO.idAccount} : {ex.Message}");
            }


            return null;
        }

        #endregion

    }
}
