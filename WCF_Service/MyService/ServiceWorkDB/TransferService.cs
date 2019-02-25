using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WCF_Service.DataBase;
using WCF_Service.DataBase.DTO;
using WCF_Service.DataBase.Repositories;

namespace WCF_Service.MyService.ServiceWorkDB
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TransferService : ITransferService
    {

        #region Методы контракта служб

        // Метод, который вернет весь список пользователей если он является администратором
        public List<MyModelLibrary.Users> GetAllUsersList(MyModelLibrary.accounts MyAcc)
        {
            // Создаем репозиторий для работы с контекстом базы данных
            EFGenericRepository<Users> MyRepository = new EFGenericRepository<Users>(new MyDB());

            // Делаем проверку: Является ли полученный юзер администратором ( s == 3 )
            if (Convert.ToInt32(MyRepository.FindQueryEntity(i => i.idUser == MyAcc.Users.idUser).idUserStatus) == 3)
            {
                // Тогда создаем DTO список юзеров и возвращаем его администратору
                List<MyModelLibrary.Users> UsersList = new List<MyModelLibrary.Users>();

                // Заносим список not dto юзеров 
                var a = MyRepository.GetAllList();

                DataBase.DTO.MyGeneratorDTO generator = new MyGeneratorDTO();

                // Перебираем список и генерируем DTO, который внесем в DTO список
                foreach (var item in a)
                {
                    // Вносим в список сгенерированный dto item
                    UsersList.Add(generator.GetUser(item));
                }

                Console.WriteLine($"{System.DateTime.Now}) Возвращаем клиенту {MyAcc.login} список с {a.Count()} клиентами!");

                // Возвращаем dto список
                return UsersList;
            }


            return null;
        }





        #endregion
    }
}
