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

        #region Вспомогательные методы

        // Метод, который проверяет статус юзера в базу данных и возвращает его
        private int CheckUserStatus(int idAccount)
        {
            // Возвращаем статус юзера из базы данных
            return Convert.ToInt32(new MyDB().Users.FirstOrDefault(i => i.idUser == idAccount).idUserStatus);
        }
        
        #endregion

        #region Методы, которые вызываются в службах

        #region Методы Администратора и преподавателя

        // Метод, который выдаст список студентов по айди группы
        // (Предварительно аккаунт должен пройти проверку на соответствие статуса)
        public List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup)
        {
            // Создаем репозиторий для работы с бд
            EFGenericRepository<Users> repository = new EFGenericRepository<Users>(new MyDB());


            // Делаем проверку статуса аккаунта
            int idAccountStatus = CheckUserStatus(MyAcc.idAccount);
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
                catch(Exception ex)
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
                    if (new MyDB().TeacherDisciplines.FirstOrDefault(i => i.IdTeacher == MyAcc.idAccount && i.IdGroup == idGroup) != null)
                    {
                        mylist = null;
                    }
                    // Иначе, если группа не найдена, то верни пустой список
                    else
                    {
                        return null; // Возвращаем пустой список
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    mylist = null;
                }

            }
            // Иначе, если не преподаватель и не администратор послал запрос, то выдай ошибку
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null; // Иначе вернет пустой список
        }

        #endregion

        #region Методы администратора

        // Метод на удаления студенты из группы (С проверкой на администратора)
        public bool RemoveStudentFromGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Если пользователь, который послал запрос является администратором, то приступи к удалению студента из группы
            if (CheckUserStatus(MyAcc.idAccount) == 3)
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

                            // Если список студентов > 0, то сдвинь номера студентов в журнале
                            if (list.Count > 0)
                            {
                                int index = Convert.ToInt16(DeletedStudent.NumberInJournal); // Определяем индекс, позицию, с которой декрементировать номера по журналу

                                // Перебираем всех студентов во второй половине списка и декрементируем номер по журналу каждого студента
                                for (int i = index; i < list.Count; i++)
                                {
                                    list.ElementAt(i).NumberInJournal--; // Уменьшаем номер в журнале на -1
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
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // Иначе, если запрос послал не администратор, то выдай ему экзепшен
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }


            return false; // Возвращаем false, если удаление не удалось
        }

        // Метод на добавление студента в группу (С проверкой на администратора)
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            // Если пользователь, который послал запрос является администратором, то приступи к добавлению студента в группу
            if (CheckUserStatus(MyAcc.idAccount) == 3)
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
                        catch(Exception ex)
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
            // Иначе, если запрос послал не администратор, то выдай ему экзепшен
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return false; // Возвращаем false, если добавление не удалось
        }

        // Метод, который создает группу
        public bool AddGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.Groups NewGroup)
        {
            // Если статус аккаунта, который послал запрос == 3 (Он администратор), то создай группу
            if (CheckUserStatus(MyAcc.idAccount) == 3)
            {
                var repository = new EFGenericRepository<Groups>(new MyDB());

                // Если все данные заполнили, то создай новую группу
                if (NewGroup.GroupName != string.Empty && NewGroup.idSpeciality != null)
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
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return false; // Верни false, если неудачное создание
        }

        // Метод, который получает аккаунт по айди
        public MyModelLibrary.accounts GetAccount(MyModelLibrary.accounts MyAcc, int idSearchAccount)
        {
            // Если аккаунт, который послал запрос администратор, то верни ему аккаунт по айди
            if (CheckUserStatus(MyAcc.idAccount) == 3)
            {
                try
                {
                    var SearchedAccount = new EFGenericRepository<accounts>(new MyDB()).FindById(idSearchAccount); // Находим аккаунт

                    var MyAcountDTO = new MyGeneratorDTO().GetAccountDTO(SearchedAccount); // Получаем DTO объект аккаунта

                    return MyAcountDTO; // Возвращаем аккаунт
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null; // Вернет null, если запрос послал не администратор
        }

        // Метод, который вернет весь список аккаунтов, если он является администратором
        public List<MyModelLibrary.accounts> GetAllAccountsList(int IdAcc)            
        {
            // Делаем проверку: Является ли полученный юзер администратором ( s == 3 )
            if (CheckUserStatus(IdAcc) == 3)
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

            // Иначе вернется пустой список
            return null;
        }

        // Метод, который принимает со стороны клиента аккаунты (он должен быть администратором, чтобы можно было редактирвоать редактируемый аккаунт)
        public bool EditAccount(MyModelLibrary.accounts MyAcc, MyModelLibrary.accounts EditAcc)
        {
            // Делаем проверку в базе данных: является ли юзер, который послал запрос администратором
            // Если является, то отредактируй аккаун
            // Если не является, то отмени запрос
            if (CheckUserStatus(MyAcc.idAccount) == 3)
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
            // Иначе, если не администратор послал запрос, то выдай ему ошибку, что он не администратор
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }


            return false; // Если редактирование не успешно
        }

        // Метод, который принимает со стороны клиента аккаунт и добавляет его в базу данных, соответственно, если аккаунт == администратор и возвращает true, если аккаунт создан
        public bool AddUser(MyModelLibrary.accounts AddAcc, int CurrentIdAcc)
        {
            // Делаем проверку в базе данных: является ли юзер, который послал запрос администратором
            // Если является, то продолжи добавление аккаунта
            // Если не является, то отмени запрос
            if (CheckUserStatus(CurrentIdAcc) == 3)
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
            // Иначе, если запрос подал не администратор, то выдай ошибку
            else
            {
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return false; // Верни false, если операция не выполнилась
        }

        // Метод, который вернет весь список пользователей если он является администратором
        public List<MyModelLibrary.Users> GetAllUsersList(int IdAcc)
        {
            // Делаем проверку: Является ли полученный юзер администратором ( s == 3 )
            if (CheckUserStatus(IdAcc) == 3)
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
            else
            {
                // Иначе, если юзер не администратор, то выдай ошибку об этом
                ExceptionSender.SendException("Вы не можете выполнить запрос, не имея статус администратора!");
            }

            return null;

        }

        #endregion

        #endregion

    }
}
