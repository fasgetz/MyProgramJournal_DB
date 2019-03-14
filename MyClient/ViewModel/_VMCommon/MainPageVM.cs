using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        NewsLogic NewsLogicService; // Сервис для работы с логикой новостей


        #endregion

        #region Команды

        // Команда на получение списка новостей
        public ICommand GetNewsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    NewsLogicService = new NewsLogic();

                    // Получаем список новостей 
                    NewsList = NewsLogicService.GetNews();
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
