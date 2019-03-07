using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ViewModel.Administrator
{

    /// <summary>
    /// VM страницы информации об аккаунте
    /// </summary>

    public class AccountInfoViewModel : UsersListPageViewModel
    {

        public AccountInfoViewModel()
        {
            // Получаем аккаунты из предыдущей VM
            Messenger.Default.Register<GenericMessage<List<MyModelLibrary.accounts>>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private void GetAccount(GenericMessage<List<MyModelLibrary.accounts>> GetAcc)
        {
            MyAcc = GetAcc.Content.ElementAt(0); // Мой аккаунт для манипулирования данными
            SelectedAccount = GetAcc.Content.ElementAt(1); // Выбранный аккаунт


            Initialization(); // Инициализируем свойства
        }
    }
}
