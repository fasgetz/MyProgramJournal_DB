using System.Collections.Generic;
using System.ServiceModel;
using WCF_Service.DataBase;

namespace WCF_Service.MyService.ServiceWorkDB
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "ITransferService" в коде и файле конфигурации.
    [ServiceContract]
    public interface ITransferService
    {


        #region Общие методы администратора и преподавателя


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

        // Метод на получение списка кодов специальностей
        [OperationContract]
        List<MyModelLibrary.Speciality_codes> GetSpecialityCodes();

        // Метод на получения списка групп
        [OperationContract]
        List<MyModelLibrary.Groups> GetGroups();

        #endregion

        #region Методы администратора

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
