﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
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
        ServiceNewsLogic MyNewsLogic = new ServiceNewsLogic();

        #endregion


        #region Методы контракта служб Администратора


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

        #endregion

        #region Методы новостей

        // Метод, который создает новость
        public void AddNews(MyModelLibrary.accounts MyAcc, MyModelLibrary.News MyNews)
        {
            MyNewsLogic.AddNews(MyAcc, MyNews);
        }

        #endregion

    }
}
