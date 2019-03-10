﻿using System;
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

        #region Методы контракта служб администратора и преподавателя

        // Метод, который выдаст список студентов по айди группы
        // (Предварительно аккаунт должен пройти проверку на соответствие статуса)
        public List<MyModelLibrary.Users> GetStudentsGroup(MyModelLibrary.accounts MyAcc, int idGroup)
        {
            return MyServiceLogic.GetStudentsGroup(MyAcc, idGroup);
        }

        #endregion

        #region Методы контракта служб Администратора

        // Метод на добавление студента в группу (С проверкой на администратора)
        public bool AddStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            return MyServiceLogic.AddStudentInGroup(MyAcc, Student); // Вернет true, если удаление прошло успешно
        }

        // Метод на удаление студента из группы (С проверкой на администратора)
        public bool RemoveStudentInGroup(MyModelLibrary.accounts MyAcc, MyModelLibrary.StudentsGroup Student)
        {
            return false; // Вернет false, если удаление прошло успешно
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
