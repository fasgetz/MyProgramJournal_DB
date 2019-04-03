using GalaSoft.MvvmLight.Messaging;

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
