using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.DataBase.DTO
{
    // Вспомогательный класс, который преобразует прокси-объекты в DTO - объекты, для передачи данных.
    class MyGeneratorDTO
    {
        #region Методы для получения DTO объектов

        // Метод для получения DTO - Список LessonsDate + Готово
        private List<MyModelLibrary.LessonsDate> GetLessonsDates(List<LessonsDate> lessonsDates)
        {
            // Создаем новый список, который будем возвращать
            List<MyModelLibrary.LessonsDate> list = new List<MyModelLibrary.LessonsDate>();

            // Если список lessonsDates не пустой, то добавь в него
            if (lessonsDates.Count() > 0)
            {
                // Перебираем все lessonsDates и присваиваем в новый список
                foreach (var item in lessonsDates)
                {
                    list.Add(new MyModelLibrary.LessonsDate(item.DateLesson, item.LessonNumber, item.IdTeacher, item.IdGroup, item.IdDiscipline, item.NumberSemester));
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
                        item.IdTeacher,
                        item.IdDiscipline,
                        item.IdGroup,
                        item.NumberSemester,
                        GetAttendanceList(item.Attendance.ToList()),
                        GetLessonsDates(item.LessonsDate.ToList()),
                        GetDiscipline(item.Discipline),
                        GetGroups(item.Groups)
                        ));
                }
            }

            return newlist.ToList();
        }

        // Метод для получения DTO - группа + Готова
        private MyModelLibrary.Groups GetGroups(Groups Groups)
        {
            // Создаем новый объет Группы, которую будем возвращать
            MyModelLibrary.Groups MyGroup = new MyModelLibrary.Groups(Groups.idGroup, Groups.GroupName);

            return MyGroup; // Возвращаем группу
        }

        // Метод для получения DTO - Discipline + Готов
        private MyModelLibrary.Discipline GetDiscipline(Discipline Discipline)
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
                    MyAttendance.Add(new MyModelLibrary.Attendance(item.idAttendance, item.IdTeacher, item.IdGroup, item.IdDiscipline, Convert.ToInt32(item.NumberSemester), item.StudentId, Convert.ToDateTime(item.DateLesson), Convert.ToInt32(item.LessonNumber), item.Mark));
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
                GetAttendanceList(studentsGroup.Attendance.ToList()),
                GetGroups(studentsGroup.Groups)
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

        // Метод для получения DTO - Юзера + готов
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

                // Конвертируем nullable дату
                if (user.DateOfBirthDay != null)
                    UserDTO.DateOfBirthDay = Convert.ToDateTime(user.DateOfBirthDay);

                return UserDTO; // Возвращаем DTO - юзера
            }


            return null;

        }

        // Метод для получения DTO - Дисциплины групп
        private List<MyModelLibrary.TeacherDisciplines> GetGroupDisciplines(Groups group)
        {
            List<MyModelLibrary.TeacherDisciplines> MyList = new List<MyModelLibrary.TeacherDisciplines>();

            // Если список дисциплин у группы > 0, то добавь их
            if (group.TeacherDisciplines.Count > 0)
            {
                // Необходимо проинициализировать какие дисциплины ведутся у группы
                foreach (var item in group.TeacherDisciplines)
                {
                    MyModelLibrary.TeacherDisciplines teacherDisciplines = new MyModelLibrary.TeacherDisciplines
                        (
                        item.IdTeacher,
                        item.IdDiscipline,
                        item.IdGroup,
                        item.NumberSemester
                        );

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
                    if (Account.Users.idUserStatus == 1 && AccountNotDTO.Users.StudentsGroup != null)
                    {
                        Account.Users.StudentsGroup = GetStudentsGroup(AccountNotDTO.Users.StudentsGroup); // Присвой группу студенту
                        Account.Users.StudentsGroup.Attendance = GetAttendanceList(AccountNotDTO.Users.StudentsGroup.Attendance.ToList()); // Присвой список оценок студента
                        Account.Users.StudentsGroup.Groups.TeacherDisciplines = GetGroupDisciplines(AccountNotDTO.Users.StudentsGroup.Groups); // Присваиваем список дисциплин группе студента                            
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
