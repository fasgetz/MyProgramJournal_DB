using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using MyModelLibrary;
using WCF_Service.DataBase;
using WCF_Service.DataBase.DTO;
using WCF_Service.DataBase.Repositories;
using WCF_Service.Exceptions;
using WCF_Service.ServiceLogic;

namespace WCF_Service.MyService.ServiceWorkDB
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TransferService : ITransferService
    {
        #region Свойства

        TransferServiceLogic MyServiceLogic = new TransferServiceLogic();

        #endregion

        #region Методы студента

        // Метод получения итоговой оценки за семестр
        public MyModelLibrary.FinalAttendances GetFinalAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines discipline)
        {
            return MyServiceLogic.GetFinalAttendance(MyAcc, discipline);
        }

        // Метод, который прогружает список дисциплин студента в семестре
        public List<MyModelLibrary.GroupDisciplines> GetStudentsDiscipline(MyModelLibrary.accounts MyAcc, int? semestr)
        {
            return MyServiceLogic.GetStudentsDiscipline(MyAcc, semestr);
        }

        #endregion

        #region Методы контракта служб администратора и преподавателя

        // Метод выставления итоговой отметки (С проверкой статуса администратора)
        public bool SetFinalAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.FinalAttendances FinalAttendance)
        {
            return MyServiceLogic.SetFinalAttendance(MyAcc, FinalAttendance);
        }

        // Метод, который получает итоговые оценки по дисциплине (С проверкой статуса)
        public List<MyModelLibrary.FinalAttendances> GetFinalAttendances(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines GroupDiscipline)
        {
            return MyServiceLogic.GetFinalAttendances(MyAcc, GroupDiscipline);
        }

        // Метод, который задает оценку (С проверкой статуса)
        public bool SetAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.Attendance Attendance)
        {
            return MyServiceLogic.SetAttendance(MyAcc, Attendance);
        }

        // Метод, который выдает преподавателю список его групп, занятия у которых он ведет (С проверкой на учителя/администратора)       
        public List<MyModelLibrary.GroupDisciplines> GetTeacherDiscipline(MyModelLibrary.accounts MyAcc)
        {
            return MyServiceLogic.GetTeacherDiscipline(MyAcc);
        }

        // Метод, который выдает список оценок группы по предмету (С проверкой статуса (Преподаватель и Администратор
        public List<MyModelLibrary.LessonsDate> GetAttendancesFromJournal(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines SelectedTeacherAtivitie)
        {
            return MyServiceLogic.GetAttendancesFromJournal(MyAcc, SelectedTeacherAtivitie);
        }

        // Метод, который получает список дисциплин
        // Предварительно аккаунт проходит проверку на соответствие статуса
        // Если администратор, то выдаются все дисциплины. Если преподаватель, то только те, которые он ведет
        public List<MyModelLibrary.Discipline> GetDisciplinesList(MyModelLibrary.accounts MyAcc)
        {
            return MyServiceLogic.GetDisciplinesList(MyAcc);
        }

        // Метод, который выдаст список студентов по айди группы
        // (Предварительно аккаунт должен пройти проверку на соответствие статуса)
        public List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup)
        {
            return MyServiceLogic.GetStudentsGroup(MyAcc, idGroup);
        }

        #endregion

        #region Методы контракта служб Администратора

        // Метод, который удаляет занятие у группы (С проверкой на администратора)
        public bool DeleteLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.LessonsDate lessons)
        {
            return MyServiceLogic.DeleteLessonGroup(MyAcc, lessons);
        }

        // Метод, который добавляет занятие (С проверкой на администратора) группе в семестре, по выбранной дате, по номеру пары и возвращает true, если занятие добавлено успешно
        public bool AddLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines discipline, int? numberlesson, System.DateTime date)
        {
            return MyServiceLogic.AddLessonGroup(MyAcc, discipline, numberlesson, date);
        }

        // Метод, который возвращает пользователю с проверкой на администратора, список пар (от 1 до 8), которые можно еще добавить группе в семестре, по дате
        public List<int> GetLessonsNumbers(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups SelectedGroup, System.DateTime Date, int? Semestr)
        {
            return MyServiceLogic.GetLessonsNumbers(MyAcc, SelectedGroup, Date, Semestr);
        }

        // Метод, который получает список занятий за выбранный период (Дата) группы в семестре с проверкой на администратора
        public List<MyModelLibrary.LessonsDate> GetLessonsOnDate(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, System.DateTime date, int? semestr)
        {
            return MyServiceLogic.GetLessonsOnDate(MyAcc, group, date, semestr);
        }

        // Метод, который редактирует название дисциплины
        // Возвращает true, если редактирование успешно
        public bool EditDisciplineName(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline Discipline)
        {
            return MyServiceLogic.EditDisciplineName(MyAcc, Discipline);
        }

        // Метод, который добавляет дисциплину группе (С проверкой на администратора)
        public bool AddDisciplineGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline discipline, int? NumbSem)
        {
            return MyServiceLogic.AddDisciplineGroup(MyAcc, group, Teacher, discipline, NumbSem);
        }

        // Метод, который возвращает список преподавателей, которые ведут дисциплину
        public List<MyModelLibrary.Users> GetUsersFromDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline SelectedDiscipline)
        {
            return MyServiceLogic.GetUsersFromDiscipline(MyAcc, SelectedDiscipline);
        }

        // Метод, который возвращает список дисциплин группы, которых еще нету у группы в семестре
        public List<MyModelLibrary.Discipline> GetNotAddedGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, byte sem)
        {
            return MyServiceLogic.GetNotAddedGroupDisciplines(MyAcc, group, sem);
        }

        // Метод получения списка дисциплин групп с проверкой на администратора по выбранному семестру
        public List<MyModelLibrary.GroupDisciplines> GetGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, int? semestr)
        {
            return MyServiceLogic.GetGroupDisciplines(MyAcc, group, semestr);
        }

        // Метод удаления группы (С проверкой на администратора)
        public bool RemoveGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups Group)
        {
            return MyServiceLogic.RemoveGroup(MyAcc, Group);
        }

        // Метод редактирования группы
        public bool EditGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups EditGroup)
        {
            return MyServiceLogic.EditGroup(MyAcc, EditGroup);
        }

        // Метод, который выдаст список преподавателей (С проверкой на администратора)
        public List<MyModelLibrary.Users> GetTeacherList(MyModelLibrary.accounts MyAcc)
        {
            return MyServiceLogic.GetTeacherList(MyAcc);
        }

        // Метод, который выдаст список дисциплин учителя / дисциплин, которых еще нет у учителя
        // (param = 1, выдаст дисциплины учителя / param = 2, выдаст дисциплины, которых еще нет у учителя)
        public List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param)
        {
            return MyServiceLogic.GetTeacherDisciplines(MyAcc, Teacher, param);
        }

        // Метод добавления дисциплины учителю
        public bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline)
        {
            return MyServiceLogic.AddTeacherDiscipline(MyAcc, Teacher, Discipline);
        }

        // Метод на добавление дисциплины (С проверкой на администратора)
        public bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline)
        {
            return MyServiceLogic.AddDiscipline(MyAcc, NewDiscipline);
        }

        // Метод на добавление студента в группу (С проверкой на администратора)
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            return MyServiceLogic.AddStudentInGroup(MyAcc, Student); // Вернет true, если удаление прошло успешно
        }

        // Метод на удаление студента из группы (С проверкой на администратора)
        public bool RemoveStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            return MyServiceLogic.RemoveStudentFromGroup(MyAcc, Student); // Вернет false, если удаление прошло успешно
        }

        // Метод, который вернет весь список аккаунтов, если он является администратором
        public List<MyModelLibrary.accounts> GetAllAccountsList(MyModelLibrary.accounts MyAcc)
        {
            return MyServiceLogic.GetAllAccountsList(MyAcc.idAccount);
        }

        // Метод, который вернет весь список пользователей если он является администратором
        public List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc)
        {
            return MyServiceLogic.GetAllUsersList(MyAcc.idAccount);
        }

        // Метод, который принимает со стороны клиента аккаунт и добавляет его в базу данных, соответственно, если аккаунт == администратор и возвращает true, если аккаунт создан
        public bool AddAccount(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc)
        {
            return MyServiceLogic.AddUser(AddAcc, CurrentAcc.idAccount);
        }

        // Метод, который принимает со стороны клиента аккаунты (он должен быть администратором, чтобы можно было редактирвоать редактируемый аккаунт)
        public bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc)
        {
            return MyServiceLogic.EditAccount(MyAcc, EditAcc); // Возвращаем истину, если аккаунт отредактирован успешно, иначе false
        }

        // Метод, который получает один аккаунт по айди (Если запрашиваемый аккаунт == администратор
        public MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int id)
        {
            return MyServiceLogic.GetAccount(MyAcc, id);
        }

        public bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup)
        {
            return MyServiceLogic.AddGroup(MyAcc, NewGroup);
        }

        #endregion

        #region Общие методы контракта служб

        // Метод, который прогружает список приказов (С проверкой на администратора) по выбранной дате        
        public List<MyModelLibrary.OrderArchive> GetOrders(MyModelLibrary.accounts MyAcc, DateTime date)
        {
            return MyServiceLogic.GetOrders(MyAcc, date);
        }

        // Метод на получение списка занятий группы по дате
        public List<MyModelLibrary.LessonsDate> GetLessons(MyModelLibrary.Groups Group, DateTime date)
        {
            return MyServiceLogic.GetLessons(Group, date);
        }

        // Возвращает список специальностей клиенту
        public List<MyModelLibrary.Speciality_codes> GetSpecialityCodes()
        {
            return MyServiceLogic.GetSpecialityCodes(); // Возвращаем список специальностей DTO
        }

        // Возвращает список групп клиенту
        public List<MyModelLibrary.Groups> GetGroups()
        {
            return MyServiceLogic.GetGroups(); // Возвращаем список групп в DTO
        }

        #endregion



    }
}
