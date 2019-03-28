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

namespace MyClient.ViewModel.Administrator.Orders
{
    public class OrdersViewModel : AdministratorViewModel
    {

        #region Свойства

        // Выбранная дата
        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get => _SelectedDate.Date;
            set
            {
                _SelectedDate = value;
                RaisePropertyChanged("SelectedDate");
            }
        }

        // Выбранный приказ
        private MyModelLibrary.OrderArchive _SelectedOrder;
        public MyModelLibrary.OrderArchive SelectedOrder
        {
            get => _SelectedOrder;
            set
            {
                _SelectedOrder = value;
                RaisePropertyChanged("SelectedOrder");
            }
        }

        // Список приказов
        private List<MyModelLibrary.OrderArchive> _Orders;
        public List<MyModelLibrary.OrderArchive> Orders
        {
            get => _Orders;
            set
            {
                _Orders = value;                
                RaisePropertyChanged("Orders");
            }
        }

        #endregion


        // Команда на кнопку в контекстном меню показать приказ
        private ICommand OpenWatchOrder
        {
            get
            {
                return new RelayCommand(() =>
                {

                });
            }
        }


        // Метод загрузки списка приказов
        private async void LoadOrdersList()
        {
            // Если выбрали дату, то асинхронно загрузи (Чтобы не блочить UI)
            if (SelectedDate != null)
                Orders = await MyAdminLogic.GetOrderse(MyAcc, SelectedDate);
        }

        // Команда на кнопку загрузить приказы
        public ICommand LoadOrders
        {
            get
            {
                return new RelayCommand(() =>
                {
                    LoadOrdersList(); // Загружаем список приказов в асинхронном режиме

                    //DirectoryInfo dirInfo = new DirectoryInfo("OrdersDocuments");

                    //// Создаем папку, если она не создана
                    //if (!dirInfo.Exists)
                    //    dirInfo.Create();

                });
            }
        }

        #region Конструктор

        public OrdersViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            SelectedDate = System.DateTime.Now;

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic();
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion

    }
}
