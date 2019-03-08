﻿using System;
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

                DataBase.DTO.MyGeneratorDTO generator = new MyGeneratorDTO();

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
