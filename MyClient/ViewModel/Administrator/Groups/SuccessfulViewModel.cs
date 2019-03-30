using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу успеваемости группы
    /// </summary>

    public class SuccessfulViewModel : AddGroupDisciplineViewModel
    {

        #region Конструктор

        public SuccessfulViewModel()
        {
            // Получаем аккаунт и группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content;
        }

        #endregion
    }
}
