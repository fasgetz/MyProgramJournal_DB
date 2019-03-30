using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.DataBase;

namespace WCF_Service.MyService.ServiceWorkDB
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITransferService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITransferService
    {

        #region Методы студента

        // Метод получения итоговой оценки за семестр
        [OperationContract]
        MyModelLibrary.FinalAttendances GetFinalAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines discipline);

        // Метод, который прогружает список дисциплин студента в семестре
        [OperationContract]
        List<MyModelLibrary.GroupDisciplines> GetStudentsDiscipline(MyModelLibrary.accounts MyAcc, int? semestr);

        #endregion

        #region Общие методы администратора и преподавателя

        // Метод выставления итоговой отметки (С проверкой статуса администратора)
        [OperationContract]
        bool SetFinalAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.FinalAttendances FinalAttendance);

        // Метод, который получает итоговые оценки по дисциплине (С проверкой статуса)
        [OperationContract]
        List<MyModelLibrary.FinalAttendances> GetFinalAttendances(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines GroupDiscipline);

        // Метод, который задает оценку (С проверкой статуса)
        [OperationContract]
        bool SetAttendance(MyModelLibrary.accounts MyAcc, MyModelLibrary.Attendance Attendance);

        // Метод, который выдает преподавателю список его групп, занятия у которых он ведет (С проверкой на учителя/администратора)
        [OperationContract]
        List<MyModelLibrary.GroupDisciplines> GetTeacherDiscipline(MyModelLibrary.accounts MyAcc);

        // Метод, который выдает список оценок группы по предмету (С проверкой статуса (Преподаватель и Администратор
        [OperationContract]
        List<MyModelLibrary.LessonsDate> GetAttendancesFromJournal(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines SelectedTeacherAtivitie);

        // Метод, который получает список дисциплин
        // Предварительно аккаунт проходит проверку на соответствие статуса
        // Если администратор, то выдаются все дисциплины. Если преподаватель, то только те, которые он ведет
        [OperationContract]
        List<MyModelLibrary.Discipline> GetDisciplinesList(MyModelLibrary.accounts MyAcc);

        // Метод, который выдаст список студентов по айди группы
        // (Предварительно аккаунт должен пройти проверку на соответствие статуса)
        [OperationContract]
        List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup);

        #endregion

        #region Общие методы

        // Метод на получение списка занятий группы по дате
        [OperationContract]
        List<MyModelLibrary.LessonsDate> GetLessons(MyModelLibrary.Groups Group, DateTime date);

        // Метод на получение списка кодов специальностей
        [OperationContract]
        List<MyModelLibrary.Speciality_codes> GetSpecialityCodes();

        // Метод на получения списка групп
        [OperationContract]
        List<MyModelLibrary.Groups> GetGroups();

        #endregion

        #region Методы администратора

        // Метод, который прогружает список приказов (С проверкой на администратора) по выбранной дате
        [OperationContract]
        List<MyModelLibrary.OrderArchive> GetOrders(MyModelLibrary.accounts MyAcc, DateTime date);

        // Метод, который удаляет занятие у группы (С проверкой на администратора)
        [OperationContract]
        bool DeleteLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.LessonsDate lessons);

        // Метод, который добавляет занятие (С проверкой на администратора) группе в семестре, по выбранной дате, по номеру пары и возвращает true, если занятие добавлено успешно
        [OperationContract]
        bool AddLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.GroupDisciplines discipline, int? numberlesson, System.DateTime date);

        // Метод, который возвращает пользователю с проверкой на администратора, список пар (от 1 до 8), которые можно еще добавить группе в семестре, по дате
        [OperationContract]
        List<int> GetLessonsNumbers(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups SelectedGroup, System.DateTime Date, int? Semestr);

        // Метод, который получает список занятий за выбранный период (Дата) группы в семестре с проверкой на администратора
        [OperationContract]
        List<MyModelLibrary.LessonsDate> GetLessonsOnDate(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, System.DateTime date, int? semestr);

        // Метод, который редактирует название дисциплины
        // Возвращает true, если редактирование успешно
        [OperationContract]
        bool EditDisciplineName(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline Discipline);

        // Метод, который добавляет дисциплину группе (С проверкой на администратора)
        [OperationContract]
        bool AddDisciplineGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline discipline, int? NumbSem);

        // Метод, который возвращает список преподавателей, которые ведут дисциплину
        [OperationContract]
        List<MyModelLibrary.Users> GetUsersFromDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline SelectedDiscipline);

        // Метод, который возвращает список дисциплин группы, которых еще нету у группы в семестре
        [OperationContract]
        List<MyModelLibrary.Discipline> GetNotAddedGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, byte sem);

        // Метод получения списка дисциплин групп с проверкой на администратора по выбранному семестру
        [OperationContract]
        List<MyModelLibrary.GroupDisciplines> GetGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, int? semestr);

        // Метод редактирования группы
        [OperationContract]
        bool EditGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups EditGroup);

        // Метод удаления группы (С проверкой на администратора)
        [OperationContract]
        bool RemoveGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups Group);

        // Метод, который выдаст список преподавателей (С проверкой на администратора)
        [OperationContract]
        List<MyModelLibrary.Users> GetTeacherList(MyModelLibrary.accounts MyAcc);

        // Метод, который выдаст список дисциплин учителя / дисциплин, которых еще нет у учителя
        // (param = 1, выдаст дисциплины учителя / param = 2, выдаст дисциплины, которых еще нет у учителя)
        [OperationContract]
        List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param);

        // Метод добавления дисциплины учителю
        [OperationContract]
        bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline);

        // Метод на добавление дисциплины (С проверкой на администратора)
        [OperationContract]
        bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline);

        // Метод на добавление студента в группу (С проверкой на администратора)
        [OperationContract]
        bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);

        // Метод на удаление студента из группы (С проверкой на администратора)
        [OperationContract]
        bool RemoveStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student);

        // Метод на создание группы (Только администратор может создать группу)
        [OperationContract]
        bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup);

        // Метод, который получит весь список пользователей, если пользователь пройдет проверку (Он должен быть администратором)
        [OperationContract]
        List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc);

        // Метод, который получает весь список аккаунтов, если пользователь пройдет проверку (Если он администратор)
        [OperationContract]
        List<MyModelLibrary.accounts> GetAllAccountsList(MyModelLibrary.accounts MyAcc);

        // Метод, который принимает со стороны клиента аккаунт и добавляет его в базу данных, соответственно, если аккаунт == администратор и возвращает true, если аккаунт создан
        [OperationContract]
        bool AddAccount(MyModelLibrary.accounts AddAcc, MyModelLibrary.accounts CurrentAcc);

        // Метод, который редактирует аккаунт. Он принимает его со стороны клиента и возвращает истину, если аккаунт отредактирован
        // Также есть проверка на статус администратора (Отправитель)
        [OperationContract]
        bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc);

        // Метод, который получает один аккаунт по айди (Если запрашиваемый аккаунт == администратор
        [OperationContract]
        MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int id);

        #endregion


    }
}
