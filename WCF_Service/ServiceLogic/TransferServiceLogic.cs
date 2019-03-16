using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_Service.DataBase;
using WCF_Service.DataBase.DTO;
using WCF_Service.DataBase.Repositories;
using WCF_Service.Exceptions;

namespace WCF_Service.ServiceLogic
{

    // Класс реализует логику работы TransferSerice
    public class TransferServiceLogic
    {

        #region Методы, которые вызываются в службах

        #region Методы Администратора и преподавателя

        // Метод, который получает список дисциплин
        // Предварительно аккаунт проходит проверку на соответствие статуса
        // Если администратор, то выдаются все дисциплины. Если преподаватель, то только те, которые он ведет
        public List<MyModelLibrary.Discipline> GetDisciplinesList(MyModelLibrary.accounts MyAcc)
        {
            // Делаем проверку статуса аккаунта
            int idAccountStatus = HelpersForTransferService.CheckUserStatus(MyAcc.idAccount);

            // Если аккаунт преподаватель или администратор, то продолжи
            if (idAccountStatus == 2 || idAccountStatus == 3)
            {
                EFGenericRepository<Discipline> repository = new EFGenericRepository<Discipline>(new MyDB()); // Репозиторий для работы с бд
                List<Discipline> list; // Список дисциплин

                // Если администратор, то выдай ему весь список дисциплин
                if (idAccountStatus == 3)
                {
                    list = repository.GetAllList(); // Получаем весь список дисциплин
                }
                // Если преподаватель, то выдай ему список дисциплин, которые он ведет
                else if (idAccountStatus == 2)
                {
                    // Сделать реализацию
                    list = null;
                }
                else
                {
                    list = null;
                }

                // Если список не пустой, то сгенерируй DTO список и отправь его клиенту
                if (list != null)
                {
                    // Создаем DTO список и возрващаем его
                    MyGeneratorDTO generator = new MyGeneratorDTO();
                    List<MyModelLibrary.Discipline> ListDTO = new List<MyModelLibrary.Discipline>();

                    // Перебираем весь список
                    foreach (var item in list)
                    {
                        ListDTO.Add(new MyModelLibrary.Discipline(item.idDiscipline, item.NameDiscipline)); // Добавляем DTO объект в DTO список
                    }

                    return ListDTO.OrderBy(i => i.idDiscipline).ToList(); // Возвращаем список (Отсортированный по айди дисциплины
                }
            }
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея необходимый статус!");
            }

            return null;
        }


        // Метод, который выдаст список студентов по айди группы
        // (Предварительно аккаунт должен пройти проверку на соответствие статуса)
        public List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup)
        {
            // Создаем репозиторий для работы с бд
            EFGenericRepository<Users> repository = new EFGenericRepository<Users>(new MyDB());

            // Делаем проверку статуса аккаунта
            int idAccountStatus = HelpersForTransferService.CheckUserStatus(MyAcc.idAccount);
            List<Users> mylist; // Список студентов

            // Если аккаунт является администратором,
            // то выдай ему данные о любой группе по его запросу
            if (idAccountStatus == 3)
            {
                try
                {
                    // Если айди группы != 0, то найди всех студентов этой группы
                    if (idGroup != 0)
                    {
                        // Найди всех студентов в группе idGroup
                        mylist = repository.GetQueryList(i => i.StudentsGroup != null && i.StudentsGroup.idGroup == idGroup).ToList();
                    }
                    // Иначе, если айди группы == 0, то найди всех студентов без группы
                    else
                    {
                        // Найди всех студентов без группы
                        mylist = repository.GetQueryList(i => i.StudentsGroup == null && i.idUserStatus == 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mylist = null;
                }
            }
            // Иначе, если статус аккаунта преподаватель,
            // то выдай ему данные о группе, только которой он ведет предмет
            else if (idAccountStatus == 2)
            {
                try
                {
                    // Получаем список студентов
                    // Если преподаватель есть в списке преподавателей и он ведет у этой группы (Дополнить - ведет предмет)
                    // && i.GroupDisciplines == idGroup
                    if (new MyDB().TeacherDisciplines.FirstOrDefault(i => i.IdTeacher == MyAcc.idAccount) != null)
                    {
                        mylist = null;
                    }
                    // Иначе, если группа не найдена, то верни пустой список
                    else
                    {
                        return null; // Возвращаем пустой список
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mylist = null;
                }

            }
            // Иначе, если не преподаватель и не администратор послал запрос, то выдай ошибку
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея необходимый статус!");
                mylist = null;
            }

            // Если список не пустой, то вернем студентов
            if (mylist != null)
            {
                // Создаем DTO - список студентов
                List<MyModelLibrary.Users> StudentsListDTO = new List<MyModelLibrary.Users>();
                MyGeneratorDTO generator = new MyGeneratorDTO(); // DTO - генератор

                // Преобразовываем всех NOT DTO в DTO
                foreach (var item in mylist)
                {
                    // Добавляем dto объект в список
                    StudentsListDTO.Add(generator.GetUser(item));
                }

                // После этого возвращаем список пользователю, который послал запрос
                return StudentsListDTO;
            }

            return null; // Возвращаем null, если статус не подходит / студентов нет в группе
        }

        #endregion

        #region Общие методы

        // Метод, который получает весь список специальность
        public List<MyModelLibrary.Speciality_codes> GetSpecialityCodes()
        {
            try
            {
                var repository = new EFGenericRepository<Speciality_codes>(new MyDB()); // Создаем репозиторий для работы с бд
                var list = repository.GetAllList(); // Получаем весь список

                // Если список не пустой, то приступи к генерации DTO объектов
                if (list != null)
                {
                    List<MyModelLibrary.Speciality_codes> MySpecialityCodesDTO = new List<MyModelLibrary.Speciality_codes>(); // Список специальностей
                    MyGeneratorDTO generator = new MyGeneratorDTO(); // Генератор DTO сущностей

                    // Перебираем весь список и вносим создаем список DTO объектов специальностей
                    foreach (var item in list)
                    {
                        MySpecialityCodesDTO.Add(generator.GetSpeciality(item));
                    }

                    return MySpecialityCodesDTO; // Возвращаем список DTO объектов специальностей
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return null;
        }

        // Метод, который получает список групп
        public List<MyModelLibrary.Groups> GetGroups()
        {
            try
            {
                var repository = new EFGenericRepository<Groups>(new MyDB());
                var list = repository.GetAllList().ToList();

                // Если список не пустой, то верни его
                if (list != null)
                {
                    List<MyModelLibrary.Groups> MyGroupsDTO = new List<MyModelLibrary.Groups>(); // Список групп DTO объектов
                    MyGeneratorDTO generator = new MyGeneratorDTO(); // Генератор DTO объектов

                    // Необходимо сделать преобразование в DTO
                    // Преобразование
                    foreach (var item in list)
                    {
                        // Добавляем группу, преобразовав в DTO объект
                        MyGroupsDTO.Add(generator.GetGroups(item));
                    }

                    return MyGroupsDTO; // Вернуть список DTO объектов
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null; // Иначе вернет пустой список
        }

        #endregion

        #region Методы администратора

        // Метод, который добавляет занятие (С проверкой на администратора) группе в семестре, по выбранной дате, по номеру пары и возвращает true, если занятие добавлено успешно
        public bool AddLessonGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, int? semestr, int? number, System.DateTime date)
        {
            return false;
        }

        // Метод, который возвращает пользователю с проверкой на администратора, список пар (от 1 до 8), которые можно еще добавить группе в семестре, по дате
        public List<int> GetLessonsNumbers(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups SelectedGroup, DateTime Date, int? Semestr)
        {
            // Проверяем юзера на соответствие статуса
            int status = HelpersForTransferService.CheckUserStatus(MyAcc.idAccount);

            // Если аккаунт администратор, то проверь правильность входных данных, иначе выдай экзепшен
            if (status == 3)
            {
                // Если входные данные соответствуют логике приложения, то продолжи, иначе выдай экзепшен
                if (SelectedGroup != null && Date != null && Semestr != null && Semestr >= 1 && Semestr <= 8)
                {
                    // Создаем репозиторий для работы с БД
                    EFGenericRepository<LessonsDate> repository = new EFGenericRepository<LessonsDate>(new MyDB()); // Репозиторий для работы с занятиями
                    EFGenericRepository<GroupDisciplines> group_repository = new EFGenericRepository<GroupDisciplines>(new MyDB()); // Репозиторий для работы с группами

                    // Ищем список занятий группы в указанный период (Даты и семестра)
                    var list = repository.GetQueryList(i => i.DateLesson == Date // Соответствие по дате
                    && new MyDB().GroupDisciplines.FirstOrDefault(s => s.idTeacherActivities == i.IdTeacherActivities).IdGroup == SelectedGroup.idGroup // Соответствие по айди группы
                    && new MyDB().GroupDisciplines.FirstOrDefault(s => s.idTeacherActivities == i.IdTeacherActivities).NumberSemester == Semestr // Соответствие по семестру
                    );

                    // Создаем список пар (От 1 до 8)
                    List<int> dto = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

                    // Перебираем пары и сравниваем. Если пара (номер занятия) есть в списке, то убираем ее. Таким образом формируем список
                    foreach (var item in list)
                    {
                        if (dto.Contains(item.LessonNumber))
                            dto.Remove(item.LessonNumber);
                    }


                    return dto; // Возвращаем список
                }
                else
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
            }

            return null; // Возвращаем null, если не удалось получить список
        }

        // Метод, который получает список занятий за выбранный период (Дата) группы в семестре с проверкой на администратора
        public List<MyModelLibrary.LessonsDate> GetLessonsOnDate(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, System.DateTime date, int? semestr)
        {
            // Проверяем юзера на соответствие статуса
            int status = HelpersForTransferService.CheckUserStatus(MyAcc.idAccount);

            // Если аккаунт администратор, то проверь правильность входных данных, иначе выдай экзепшен
            if (status == 3)
            {
                if (group != null && date != null && semestr != null && semestr >= 1 && semestr <= 8)
                {
                    // Создаем репозиторий для работы с БД
                    EFGenericRepository<LessonsDate> repository = new EFGenericRepository<LessonsDate>(new MyDB());

                    // Ищем все занятия группы по времени date семестру semestr
                    var list = repository.GetQueryList
                        (
                        i => i.DateLesson == date // По дате
                        && i.GroupDisciplines.IdGroup == group.idGroup // Занятие по айди группы
                        && i.GroupDisciplines.NumberSemester == semestr // По номеру семестра
                        );

                    // Если список занятий не пустой, то верни его пользователю в DTO
                    if (list != null)
                    {
                        MyGeneratorDTO generator = new MyGeneratorDTO();

                        return generator.GetLessonsDates(list); ; // Возвращаем DTO список
                    }
                }
                else
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
            }

            return null; // Возвращаем пустой список, если не удалось получить
        }

        // Метод, который редактирует название дисциплины. Возвращает true, если редактирование успешно
        public bool EditDisciplineName(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline Discipline)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если статус соответствует, то приступи к добавлению группы
            if (status == true)
            {
                // Если входные данные дисциплины не пусты, то приступи к редактированию
                if (Discipline != null && Discipline.NameDiscipline != string.Empty)
                {
                    try
                    {
                        // Репозиторий для работы с БД
                        EFGenericRepository<Discipline> repository = new EFGenericRepository<Discipline>(new MyDB());

                        // Ищем дисциплину
                        var discip = repository.FindQueryEntity(i => i.idDiscipline == Discipline.idDiscipline);

                        // Если дисциплину нашли, то измени ее название
                        if (discip != null)
                        {
                            discip.NameDiscipline = Discipline.NameDiscipline; // Изменяем название дисциплины
                            repository.Edit(discip); // Отправляем измененную дисциплину в бд

                            return true; // Возвращаем true, т.к. дисциплина отредактирована
                        }
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ExceptionSender.SendException($"Дисциплина <<{Discipline.NameDiscipline}>> уже существует!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
            }

            return false; // Возвращаем false, если редактирование неудачное
        }

        // Метод, который добавляет дисциплину группе (С проверкой на администратора)
        public bool AddDisciplineGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline discipline, int? NumbSem)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если статус соответствует, то приступи к добавлению группы
            if (status == true)
            {
                // Если входные данные не пусты, то приступи к добавлению группы
                if (group != null && Teacher != null && discipline != null && NumbSem != null && NumbSem >= 1 && NumbSem <= 8)
                {
                    // Устанавливаем подключение к бд
                    using (MyDB db = new MyDB())
                    {
                        // Необходимо додумать проверку на то, ведет ли уже преподаватель данную дисциплину в данном семестре у данной группы
                        var check = db.GroupDisciplines.FirstOrDefault
                            (
                                i => i.TeacherDisciplines.IdTeacher == Teacher.idUser && i.TeacherDisciplines.IdDiscipline == discipline.idDiscipline
                                && i.IdGroup == group.idGroup
                                && i.NumberSemester == NumbSem
                            );

                        // Если не нашли, то можно добавить
                        if (check == null)
                        {
                            try
                            {
                                // Создаем репозиторий для работы с БД
                                EFGenericRepository<GroupDisciplines> repository = new EFGenericRepository<GroupDisciplines>(new MyDB());

                                // Добавляем дисциплину группе
                                repository.Add(new GroupDisciplines(new MyDB().TeacherDisciplines.FirstOrDefault(i => i.IdTeacher == Teacher.idUser && i.IdDiscipline == discipline.idDiscipline).IdTeacherDiscipline,
                                    group.idGroup, Convert.ToInt16(NumbSem)));

                                return true;
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }                            
                        }
                        else
                        {
                            ExceptionSender.SendException("Мы не можем добавить так как дисциплина у группы уже добавлена!");
                        }
                    }
                }
                // Иначе, если входные данные не заполнены, то выдай экзепшен пользователю
                else
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
            }

            return false; // Верни false, если добавление дисциплины группе неудачно
        }

        // Метод, который возвращает список преподавателей, которые ведут дисциплину
        public List<MyModelLibrary.Users> GetUsersFromDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline SelectedDiscipline)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если статус соответствует, то выполни поиск преподавателей, которые ведут эту дисциплину
            if (status == true)
            {
                if (SelectedDiscipline != null)
                {
                    // Создаем репозиторий для работы с БД
                    EFGenericRepository<Users> repository = new EFGenericRepository<Users>(new MyDB());
                    EFGenericRepository<TeacherDisciplines> teachers_repository = new EFGenericRepository<TeacherDisciplines>(new MyDB());

                    // Ищем всех преподавателей, которые ведут выбранню дисциплину
                    var list = new MyDB().TeacherDisciplines.Where(i => i.IdDiscipline == SelectedDiscipline.idDiscipline);

                    // Если список не пустой, то создай DTO список и верни его пользователю
                    if (list != null)
                    {
                        MyGeneratorDTO generator = new MyGeneratorDTO();
                        List<MyModelLibrary.Users> Teachers = new List<MyModelLibrary.Users>();

                        // Перебираем весь список и добавляем в DTO список уже DTO объекты
                        foreach (var item in list)
                        {
                            var teacher = repository.FindQueryEntity(i => i.idUser == item.IdTeacher); // Находим юзера в бд
                            Teachers.Add(generator.GetUser(teacher)); // Добавляем в список
                        }

                        return Teachers; // Возвращаем DTO список
                    }
                }
                else
                {
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
                }
            }

            return null; // Возвращаем null, если не удалось получить список
        }

        // Метод, который возвращает список дисциплин группы, которых еще нету у группы в семестре
        public List<MyModelLibrary.Discipline> GetNotAddedGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, byte sem)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Если группу выбрали, то продолжи
                if (group != null && sem >= 1 && sem <= 8)
                {
                    try
                    {
                        // Репозитории для работы с БД
                        EFGenericRepository<Discipline> discipline_repository = new EFGenericRepository<Discipline>(new MyDB());

                        // Сперва находим дисциплины, которые есть у группы в этом семестре
                        var templist = new MyDB().GroupDisciplines.Where(i => i.IdGroup == group.idGroup && i.NumberSemester == sem);
                        Console.WriteLine($"Всего нашлось: {templist.Count()} дисциплин у группы {group.GroupName}");

                        // Если дисциплины у группы есть, то верни ей список тех дисциплин, которых нету у группы
                        if (templist != null)
                        {

                            var alldisciplines = discipline_repository.GetAllList(); // Получаем весь список дисциплин
                            List<MyModelLibrary.Discipline> disciplines = new List<MyModelLibrary.Discipline>(); // Список дисциплин в DTO

                            // Перебираем весь список дисциплин и сравниваем
                            foreach (var item in alldisciplines)
                            {
                                // Находим дисциплину в списке по айди
                                var MyDiscipline = templist.FirstOrDefault(i => i.TeacherDisciplines.IdDiscipline == item.idDiscipline);

                                // Если дисциплина найдена, то добавляем в DTO список
                                if (MyDiscipline == null)
                                    disciplines.Add(new MyModelLibrary.Discipline(item.idDiscipline, item.NameDiscipline)); // Добавляем дисциплину
                            }

                            return disciplines; // Возвращаем список
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Иначе выдай ошибку о том, что входные данные не заполнены
                else
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
            }

            return null; // Возвращаем null, если неудалось получить список
        }

        // Метод получения списка дисциплин групп с проверкой на администратора по выбранному семестру
        public List<MyModelLibrary.GroupDisciplines> GetGroupDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups group, int? semestr)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Проверяем на правильность входных данных (Должна быть указана группа и семестр в диапазоне 1..8
                if (group != null && semestr != null && semestr >= 1 & semestr <= 8)
                {
                    // Создаем репозиторий для работы с бд
                    EFGenericRepository<GroupDisciplines> repository = new EFGenericRepository<GroupDisciplines>(new MyDB());
                    EFGenericRepository<Users> users = new EFGenericRepository<Users>(new MyDB());

                    // Ищем все дисциплины учителя, указанной группы, указанного семестра
                    var list = repository.GetQueryList
                        (i => 
                        i.IdGroup == group.idGroup // По айди группы
                        && i.NumberSemester == semestr // По семестру
                        );

                    // Если список не пустой преобразуй список в DTO
                    if (list != null)
                    {
                        // Создаем генератор и DTO список
                        MyGeneratorDTO generator = new MyGeneratorDTO();
                        List<MyModelLibrary.GroupDisciplines> MyDTOList = new List<MyModelLibrary.GroupDisciplines>();

                        // Перебираем все элементы и добавляем их в dto список
                        foreach (var item in list)
                        {
                            MyDTOList.Add(new MyModelLibrary.GroupDisciplines
                                (
                                item.idTeacherActivities, item.IdTeacherDiscipline, item.IdGroup, item.NumberSemester, 
                                item.TeacherDisciplines.Discipline.NameDiscipline, 
                                users.FindQueryEntity(i => i.idUser == item.TeacherDisciplines.IdTeacher).GetFIO) // Добавляем фио учителя
                                ); // Добавляем в dto список
                        }

                        // Возвращаем dto список пользователю
                        return MyDTOList;
                    }
                    else
                    {
                        Console.WriteLine($"Не удалось найти");
                    }
                }
                // Если входные данные не заполнены, то здесь что-то не так, выдай экзепшен шутнику
                else
                {
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
                }
            }
        
            return null; // Возвращаем null, если неудалось найти
        }

        // Метод редактирования группы
        public bool EditGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups EditGroup)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Если название данные группы заполнены, то отредактируй
                if (EditGroup != null && EditGroup.GroupName != string.Empty && EditGroup.idSpeciality != null)
                {
                    try
                    {
                        // Создаем репозиторий для работы с бд
                        EFGenericRepository<Groups> repository = new EFGenericRepository<Groups>(new MyDB());
                        repository.Edit(new Groups(EditGroup.idGroup, EditGroup.GroupName, Convert.ToInt16(EditGroup.idSpeciality))); // Обновляем группу в бд 

                        Console.WriteLine($"Группа {EditGroup.idGroup} успешно редактирована администратором {MyAcc.login}");
                        return true; // Возвращаем true, т.к. успешно отредактировано
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Иначе, выдай ошибку о том, что входные данные не заполнены
                else
                {
                    ExceptionSender.SendException("Вы не заполнили необходимые данные!");
                }
            }

            return false; // Вовзращаем false, если неудачное редактирование
        }

        // Метод удаления группы (С проверкой на администратора)
        public bool RemoveGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups Group)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                try
                {
                    if (Group != null)
                    {
                        // Ищем группу по айди, которую надо удалить
                        EFGenericRepository<Groups> repository = new EFGenericRepository<Groups>(new MyDB());
                        repository.Remove(repository.FindQueryEntity(i => i.idGroup == Group.idGroup)); // Удаляем группу

                        return true; //Возвращаем true, т.к. группа удалена успешно
                    }
                    else
                    {
                        ExceptionSender.SendException("Вы не выбрали группу");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return false;
        }

        // Метод, который выдаст список преподавателей (С проверкой на администратора)
        public List<MyModelLibrary.Users> GetTeacherList(MyModelLibrary.accounts MyAcc)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                EFGenericRepository<Users> repository = new EFGenericRepository<Users>(new MyDB());

                var list = repository.GetQueryList(i => i.idUserStatus == 2); // Получаем список преподавателей

                // Если список не пустой, то преобразуй его в DTO список и отправь клиенту
                if (list != null)
                {
                    try
                    {
                        MyGeneratorDTO generator = new MyGeneratorDTO(); // Генератор DTO объектов
                        List<MyModelLibrary.Users> MyDTOList = new List<MyModelLibrary.Users>(); // DTO список

                        // Перебираем весь список и преобразуем в DTO
                        foreach (var item in list)
                        {
                            MyDTOList.Add(generator.GetUser(item)); //  добавляем DTO элемент в DTO список
                        }

                        return MyDTOList; // Возвращаем DTO списо
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return null; // Вернуть null, если неудалось получить список
        }

        // Метод, который выдаст список дисциплин учителя / дисциплин, которых еще нет у учителя
        // (param = 1, выдаст дисциплины учителя / param = 2, выдаст дисциплины, которых еще нет у учителя)
        public List<MyModelLibrary.Discipline> GetTeacherDisciplines(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, byte param)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Если выбрали учителя, то прогрузи список дисциплин по выбранному параметру
                if (Teacher != null && HelpersForTransferService.CheckUserStatus(Teacher.idUser) == 2)
                {
                    try
                    {
                        // Создаем репозиторий для работы с БД
                        EFGenericRepository<Discipline> repository = new EFGenericRepository<Discipline>(new MyDB());
                        EFGenericRepository<TeacherDisciplines> DisciplinesRepository = new EFGenericRepository<TeacherDisciplines>(new MyDB()); // Репозиторий для работы с бд в контексте дисциплин учителя
                        List<TeacherDisciplines> list; // Список дисциплин

                        // Если выбрали 1 (Значит, что необходимо выбрать все дисциплины, которые ведет учитель)
                        if (param == 1)
                        {
                            list = DisciplinesRepository.GetQueryList(i => i.IdTeacher == Teacher.idUser); // Получаем список дисциплин, которые ведет учитель

                            // Если список не пустой, то преобразуй DTO список объектов и верни юзеру
                            if (list != null)
                            {
                                List<MyModelLibrary.Discipline> disciplines = new List<MyModelLibrary.Discipline>(); // Список дисциплин в DTO

                                var notdtodisciplines = repository.GetAllList(); // Получаем весь список дисциплин

                                // Перебираем все дисциплины и добавляем их в DTO список
                                foreach (var item in list)
                                {
                                    // Находим дисциплину в списке по айди
                                    var MyDiscipline = list.FirstOrDefault(i => i.IdDiscipline == item.IdDiscipline);

                                    disciplines.Add(new MyModelLibrary.Discipline(item.IdDiscipline, item.Discipline.NameDiscipline)); // Добавляем дисциплину
                                }

                                Console.WriteLine($"Возвращаем юзеру: {disciplines.Count()} дисциплин!");
                                return disciplines; // Возвращаем список дисциплин
                            }
                        }
                        // Если выбрали 2 (Значит, что необходимо выбрать дисциплины, которые не ведет учитель)
                        else if (param == 2)
                        {
                            // Загружаем весь список дисциплин
                            var all_list = repository.GetAllList(); // Получаем весь список дисциплин
                            List<MyModelLibrary.Discipline> disciplines; // Список дисциплин

                            // Получаем список дисциплин, которые ведет учитель
                            list = DisciplinesRepository.GetQueryList(i => i.IdTeacher == Teacher.idUser); // Получаем список дисциплин, которые ведет учитель

                            // Если список не пустой, то верни дисциплины, которые не ведет учитель
                            if (list != null)
                            {
                                disciplines = new List<MyModelLibrary.Discipline>(); // Инициализируем список

                                // Перебираем все дисциплины и сравниваем есть ли в списке дисциплин учителя эта дисциплина
                                // Если нету, то добавляем в список
                                foreach (var item in all_list)
                                {
                                    // Ищем дисциплину в списке
                                    var MyDiscipline = list.FirstOrDefault(i => i.IdDiscipline == item.idDiscipline);

                                    // Если дисциплина == null, то добавь ее в список
                                    // Это значит, что ее нет в списке дисциплин учителя
                                    if (MyDiscipline == null)
                                    {
                                        disciplines.Add(new MyModelLibrary.Discipline(item.idDiscipline, item.NameDiscipline)); // Добавляем DTO объект в список
                                    }
                                }

                                return disciplines; // Возвращаем список
                            }
                        }
                        // Иначе, если параметр выбран неверно, то верни ошибку
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Иначе, если учителя не выбрали, то верни экзепшен
                else
                {
                    ExceptionSender.SendException("Вы не выбрали учителя!!");
                }
            }

            return null; // Вернуть null, если неудалось получить список
        }

        // Метод добавления дисциплины учителю
        public bool AddTeacherDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Users Teacher, MyModelLibrary.Discipline Discipline)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Если входные данные не пустые, то выполни запрос
                if (Teacher != null && Discipline != null)
                {
                    // Создаем репозиторий для работы с бд
                    EFGenericRepository<TeacherDisciplines> repository = new EFGenericRepository<TeacherDisciplines>(new MyDB());

                    // Делаем проверку на то, есть ли уже эта дисципла у преподавателя в бд
                    // Если нету, то добавь ее. Если есть, то отклони
                    if (repository.FindQueryEntity(i => i.IdDiscipline == Discipline.idDiscipline && i.IdTeacher == Teacher.idUser) == null)
                    {
                        try
                        {
                            // Добавляем дисциплину в репозиторий
                            repository.Add(new TeacherDisciplines(Teacher.idUser, Discipline.idDiscipline));

                            return true; // Возвращаем истину, т.к. дисциплину добавили успешно
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        ExceptionSender.SendException("Эта дисциплина уже есть у преподавателя!");
                    }
                }
                // Иначе выдай экзепшен
                else
                    ExceptionSender.SendException("Вы не можете выполнить запрос! Проверьте входные данные");
            }

            return false; // Возвращаем false, если неудачный запрос на добавление дисциплины преподавателю
        }

        // Метод добавления новой дисциплины
        public bool AddDiscipline(MyModelLibrary.accounts MyAcc, MyModelLibrary.Discipline NewDiscipline)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Если название дисциплины != пустое название, то добавь дисциплину, иначе выдай ошибку
                if (NewDiscipline.NameDiscipline != null & NewDiscipline != null)
                {
                    try
                    {
                        // Создаем репозиторий для работы с бд
                        EFGenericRepository<Discipline> repository = new EFGenericRepository<Discipline>(new MyDB());
                        repository.Add(new Discipline(NewDiscipline.NameDiscipline)); // Добавляем дисциплину в репозиторий

                        return true; // Возвращаем true, т.к. дисциплина была успешно добавлена в бд
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ExceptionSender.SendException($"Дисциплина <<{NewDiscipline.NameDiscipline}>> уже существует!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Если название дисциплины передано пустое, то выдай ошибку
                else
                {
                    ExceptionSender.SendException("Вы не ввели название дисциплины");
                }
            }

            return false; // Возвращаем false, если неудачно
        }

        // Метод на удаления студенты из группы (С проверкой на администратора)
        public bool RemoveStudentFromGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                try
                {
                    using (MyDB db = new MyDB())
                    {
                        // Ищем удаляемого студента в списке студентов группы
                        var DeletedStudent = db.StudentsGroup.FirstOrDefault(i => i.IdStudent == Student.IdStudent && i.idGroup == Student.idGroup);

                        // Если студента нашли в списке, то удали его
                        if (DeletedStudent != null)
                        {
                            // Необходимо сдвинуть номера в журнале всех студентов (Иначе будет проплешина)
                            // Получаем весь список студентов группы удаляемого студента
                            List<StudentsGroup> list = db.StudentsGroup.Where(i => i.idGroup == Student.idGroup).ToList();

                            // Перебираем всех студентов и декрементируем номер по журналу каждого студента, после удаляемого студента
                            foreach (var item in list)
                            {
                                if (item.NumberInJournal > DeletedStudent.NumberInJournal)
                                {
                                    item.NumberInJournal--;
                                }
                            }

                            db.StudentsGroup.Remove(DeletedStudent); // Удаляем студента
                            db.SaveChanges(); // Сохраняем бд

                            return true; // Возвращаем true, т.к. удаление успешно
                        }
                        // Иначе, если студента не нашли, то выдай экзепшен
                        else
                        {
                            ExceptionSender.SendException($"Удаляемого студента нет в списке группы!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return false; // Возвращаем false, если удаление не удалось
        }

        // Метод на добавление студента в группу (С проверкой на администратора)
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Создаем репозитории для работы с БД
                EFGenericRepository<Users> repository = new EFGenericRepository<Users>(new MyDB()); // Репозиторий для работы с юзерами
                EFGenericRepository<StudentsGroup> students_repository = new EFGenericRepository<StudentsGroup>(new MyDB()); // Репозиторий для работы со студентами

                var NewStudent = repository.FindQueryEntity(i => i.idUser == Student.IdStudent); // Ищем добавляемого студента в списке юзеров

                // Если юзер существует в списке юзеров и он является студентом по статусу, а также у него нет группы, то добавь его в список студентов
                if (NewStudent != null)
                {
                    // Делаем проверку: является ли добавляемый студент студентом и состоит ли он в группе
                    if (NewStudent.idUserStatus == 1 && NewStudent.StudentsGroup == null)
                    {
                        try
                        {
                            // Добавляем в репозиторий студентов нового студента
                            students_repository.Add(new StudentsGroup
                                (
                                Student.IdStudent, // Номер студента
                                Convert.ToInt16(Student.idGroup), // Айди группы
                                students_repository.GetQueryList(i => i.idGroup == Student.idGroup).Count() + 1 // Номер по журналу = Количество всех студентов в группе + 1
                                ));

                            return true; // Возвращаем true, т.к. студент успешно добавлен в группу
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        ExceptionSender.SendException($"Пользователю нельзя добавить группу!");
                    }
                }
            }

            return false; // Возвращаем false, если добавление не удалось
        }

        // Метод, который создает группу
        public bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                var repository = new EFGenericRepository<Groups>(new MyDB());

                // Если все данные заполнили, то создай новую группу
                if (NewGroup.GroupName != null && NewGroup.idSpeciality != null)
                {
                    try
                    {
                        // Добавляем новую группу в репозиторий
                        repository.Add(new Groups(NewGroup.GroupName, Convert.ToInt16(NewGroup.idSpeciality)));

                        return true;
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ExceptionSender.SendException($"Группа <<{NewGroup.GroupName}>> уже существует в базе данных!\nВыберите другое название для группы");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Иначе выдай ошибку, что не получилось создать группу т.к. не все данные заполнены
                else
                {
                    ExceptionSender.SendException("Вы не заполнили всех необходимых данных!");
                }
            }

            return false; // Верни false, если неудачное создание
        }

        // Метод, который получает аккаунт по айди
        public MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int idSearchAccount)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                try
                {
                    var SearchedAccount = new EFGenericRepository<accounts>(new MyDB()).FindById(idSearchAccount); // Находим аккаунт

                    var MyAcountDTO = new MyGeneratorDTO().GetAccountDTO(SearchedAccount); // Получаем DTO объект аккаунта

                    return MyAcountDTO; // Возвращаем аккаунт
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null; // Вернет null, если неудачный запрос
        }

        // Метод, который вернет весь список аккаунтов, если он является администратором
        public List<MyModelLibrary.accounts> GetAllAccountsList(int IdAcc)            
        {            
            MyModelLibrary.accounts Acc = new MyModelLibrary.accounts();
            Acc.idAccount = IdAcc; // Инициализируем айди

            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(Acc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                try
                {
                    // Если прошли проверку на администратора, то возвращаем список аккаунтов администратору
                    List<MyModelLibrary.accounts> AccountsList = new List<MyModelLibrary.accounts>();

                    // Заносим список not dto юзеров
                    var a = new EFGenericRepository<accounts>(new MyDB()).GetAllList();

                    MyGeneratorDTO generator = new MyGeneratorDTO();

                    // Перебираем список и генерируем DTO, который внесем в DTO список
                    foreach (var item in a)
                    {
                        // Вносим в список сгенерированный dto item
                        AccountsList.Add(generator.GetAccountDTO(item));
                    }

                    // Возвращаем dto список
                    return AccountsList;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Иначе вернется пустой список
            return null;
        }

        // Метод, который принимает со стороны клиента аккаунты (он должен быть администратором, чтобы можно было редактирвоать редактируемый аккаунт)
        public bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc)
        {
            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                try
                {
                    // Найдем редактируемый аккаунт в бд и отредактируем его
                    EFGenericRepository<accounts> repository = new EFGenericRepository<accounts>(new MyDB()); // Репозиторий для работы с аккаунтами
                    EFGenericRepository<Users> users_repository = new EFGenericRepository<Users>(new MyDB()); // Репозиторий для работы с юзерами

                    repository.Edit(new accounts
                        (EditAcc.idStatus,
                        EditAcc.login,
                        EditAcc.password,
                        EditAcc.DateRegistration,
                        EditAcc.Users.Name,
                        EditAcc.Users.Family,
                        EditAcc.Users.Surname,
                        EditAcc.Users.Gender,
                        Convert.ToInt32(EditAcc.Users.idUserStatus),
                        EditAcc.Users.NumberPhone,
                        EditAcc.Users.DateOfBirthDay,
                        EditAcc.idAccount
                        ));

                    users_repository.Edit(new Users
                        (
                        EditAcc.idAccount,
                        Convert.ToInt16(EditAcc.Users.idUserStatus),
                        EditAcc.Users.Name,
                        EditAcc.Users.Family,
                        EditAcc.Users.Surname,
                        EditAcc.Users.Gender,
                        EditAcc.Users.NumberPhone,
                        EditAcc.Users.DateOfBirthDay
                        ));

                    Console.WriteLine($"{DateTime.Now}) Аккаунт {EditAcc.idAccount} успешно отредактирован! администратором <<{MyAcc.login}>>");

                    return true;
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    ExceptionSender.SendException($"Пользатель с логином <<{EditAcc.login}>> уже существует!\nЛогин должен быть уникальным!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return false; // Если редактирование неудачно, верни false
        }

        // Метод, который принимает со стороны клиента аккаунт и добавляет его в базу данных, соответственно, если аккаунт == администратор и возвращает true, если аккаунт создан
        public bool AddUser(MyModelLibrary.accounts AddAcc, int CurrentIdAcc)
        {
            MyModelLibrary.accounts MyAcc = new MyModelLibrary.accounts();
            MyAcc.idAccount = CurrentIdAcc;

            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                if
                                   (AddAcc.login != null
                                   && AddAcc.password != null
                                   && AddAcc.idStatus == 1 || AddAcc.idStatus == 2
                                   && AddAcc.Users.Name != null
                                   && AddAcc.Users.Family != null
                                   && AddAcc.Users.Gender != null
                                   && AddAcc.Users.idUserStatus == 1 || AddAcc.Users.idUserStatus == 2) // Если пользователь студент или администратор
                {
                    try
                    {
                        // Если логин или пароль пользователя < 6 символов длиной, то выдай соответствующую ошибку
                        if (
                            AddAcc.login.Length > 5
                            && AddAcc.password.Length > 5)
                        {
                            // Создаем аккаунт и инициализируем данные
                            accounts NewAccount = new accounts
                                (AddAcc.idStatus,
                                AddAcc.login,
                                AddAcc.password,
                                DateTime.Now,
                                AddAcc.Users.Name,
                                AddAcc.Users.Family,
                                AddAcc.Users.Surname,
                                AddAcc.Users.Gender,
                                Convert.ToInt16(AddAcc.Users.idUserStatus),
                                AddAcc.Users.NumberPhone,
                                AddAcc.Users.DateOfBirthDay
                                );

                            // Создаем репозиторый для работы с контекстом базы данных
                            EFGenericRepository<accounts> MyRepository = new EFGenericRepository<accounts>(new MyDB());
                            MyRepository.Add(NewAccount);
                            Console.WriteLine($"{DateTime.Now}) Аккаунт <<{AddAcc.login}>> успешно создан!");
                            return true;
                        }
                        // Иначе, если учетные данные меньше 5 символов, то выдай ошибку
                        else
                        {
                            ExceptionSender.SendException($"Логин и пароль должны иметь больше 5-ти символов!");
                            return false;
                        }
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ExceptionSender.SendException($"Пользатель с логином <<{AddAcc.login}>> уже существует!\nЛогин должен быть уникальным!");
                    }
                }
                // Иначе, если необходимые данные не заполнены, то выдай ошибку
                else
                {
                    ExceptionSender.SendException("Вы не можете выполнить запрос, пока не заполните необходимые данные!");
                }
            }

            return false; // Верни false, если операция не выполнилась
        }

        // Метод, который вернет весь список пользователей если он является администратором
        public List<MyModelLibrary.Users> GetAllUsersList(int IdAcc)
        {
            MyModelLibrary.accounts MyAcc = new MyModelLibrary.accounts();
            MyAcc.idAccount = IdAcc;

            // Проверяем юзера на соответствие статуса
            bool status = HelpersForTransferService.CheckStatus(MyAcc, 3);

            // Если аккаунт прошел проверку статуса (Он администратор, то выполни дальнейшее)
            if (status == true)
            {
                // Тогда создаем DTO список юзеров и возвращаем его администратору
                List<MyModelLibrary.Users> UsersList = new List<MyModelLibrary.Users>();

                // Заносим список not dto юзеров 
                var a = new EFGenericRepository<Users>(new MyDB()).GetAllList();

                MyGeneratorDTO generator = new MyGeneratorDTO();

                // Перебираем список и генерируем DTO, который внесем в DTO список
                foreach (var item in a)
                {
                    // Вносим в список сгенерированный dto item
                    UsersList.Add(generator.GetUser(item));
                }

                // Возвращаем dto список
                return UsersList;
            }

            return null; // Возвращаем null, если запрос неудачный
        }

        #endregion

        #endregion

    }
}
