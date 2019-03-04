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

    }
}
