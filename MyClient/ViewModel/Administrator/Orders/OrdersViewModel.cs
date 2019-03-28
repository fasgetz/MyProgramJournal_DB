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

        // Команда на создание ворд файла
        public ICommand CreateWord
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ProgramLogic.Documentation.MyWord.CreateWordDoc(SelectedOrder); // Создаем вордовый документ
                });
            }
        }

        // Команда на кнопку в контекстном меню показать приказ
        public ICommand OpenWatchOrder
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если выбрали приказ, то перейди на страницу и передай его контекст
                    if (SelectedOrder != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.OrdersWatchVM == null)
                            locator.OrdersWatchVM = new OrdersWatchViewModel();

                        SelectedOrder = new MyModelLibrary.OrderArchive(SelectedOrder.IdOrder, SelectedOrder.Date, SelectedOrder.Commentary, SelectedOrder.IdOrderType, 
                            SelectedOrder.OrderTypes = new MyModelLibrary.OrderTypes(SelectedOrder.OrderTypes.IdOrderType, SelectedOrder.OrderTypes.OrderName));

                        // Мессенджер: передай в ScheduleViewModel наш MyAcc
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.OrderArchive>(SelectedOrder)); // Отправляем в следующий DataContext аккаунт

                        // Перейди в Page страницы занятий группы
                        navigation.Navigate("View/Administrator/Orders/OrdersWatchPage.xaml");
                    }
                });
            }
        }


        // Метод загрузки списка приказов
        private async void LoadOrdersList()
        {
            // Если выбрали дату, то асинхронно загрузи (Чтобы не блочить UI)
            if (SelectedDate != null)
            {
                Orders = await MyAdminLogic.GetOrderse(MyAcc, SelectedDate);
                text = $"Приказов: {Orders.Count}";
            }                
        }

        // Команда на кнопку загрузить приказы
        public ICommand LoadOrders
        {
            get
            {
                return new RelayCommand(() =>
                {
                    LoadOrdersList(); // Загружаем список приказов в асинхронном режиме
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

            // Обнуляем некоторые данные контекста, если != null и выставляем первоначальные значения
            text = "Приказы";

            if (Orders != null)
                Orders = null;
        }

        #endregion

    }
}
