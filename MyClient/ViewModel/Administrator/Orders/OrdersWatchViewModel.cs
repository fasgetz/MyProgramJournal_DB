using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Windows.Documents;
using System.Threading.Tasks;

namespace MyClient.ViewModel.Administrator.Orders
{

    /// <summary>
    /// VM представляет страницу описания приказа
    /// </summary>

    public class OrdersWatchViewModel : OrdersViewModel
    {

        #region Конструктор

        public OrdersWatchViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.OrderArchive>>(this, GetOrder);

            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected void GetOrder(GenericMessage<MyModelLibrary.OrderArchive> GetOrder)
        {
            SelectedOrder = GetOrder.Content;
        }

        #endregion

    }
}
