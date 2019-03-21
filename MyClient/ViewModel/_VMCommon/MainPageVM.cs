using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using MyClient.ProgramLogic.ServiceLogic;
using MyClient.ViewModel._Navigation;

namespace MyClient.ViewModel._VMCommon
{
    public class MainPageVM : AccountVM
    {

        #region Свойства

        //NewsLogic NewsLogicService; // Сервис для работы с логикой новостей


        #endregion

        #region Команды



        // Команда на получение списка новостей
        public ICommand GetNewsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //// Если сервис новостей не проинициализирован, то проинициализируй его
                    //NewsLogicService = new NewsLogic();

                    // Загружаем список новостей в асинхронном режиме
                    Load();
                    //Thread t = new Thread(Load);
                    //t.SetApartmentState(ApartmentState.STA);
                    //t.Start();
                    //Load(); // Загружаем новости в асинхронном режиме
                });
            }
        }

        #endregion


        public MainPageVM()
        {
            // Инициализируем диалог
            dialog = new DialogService();            
        }
    }
}
