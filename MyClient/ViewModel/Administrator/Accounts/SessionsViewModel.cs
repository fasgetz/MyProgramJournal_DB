using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.ViewModel.Administrator.Accounts
{
    /// <summary>
    /// VM представляет страницу сессий выбранного аккаунта
    /// </summary>

    public class SessionsViewModel : AccountsListPageViewModel
    {

        public SessionsViewModel()
        {
            // Получаем аккаунты из предыдущей VM
            Messenger.Default.Register<GenericMessage<List<MyModelLibrary.accounts>>>(this, GetAccount);

            dialog = new ProgramLogic.DialogServices.DialogService();
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private void GetAccount(GenericMessage<List<MyModelLibrary.accounts>> GetAcc)
        {
            MyAcc = GetAcc.Content.ElementAt(0); // Мой аккаунт для манипулирования данными
            SelectedAccount = GetAcc.Content.ElementAt(1); // Выбранный аккаунт
        }



    }
}
