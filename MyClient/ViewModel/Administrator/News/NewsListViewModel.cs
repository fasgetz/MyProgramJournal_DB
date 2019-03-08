using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using MyClient.ProgramLogic.ServiceLogic;

namespace MyClient.ViewModel.Administrator.News
{

    /// <summary>
    /// VM представляет страницу NewsListPage
    /// </summary>

    public class NewsListViewModel : AdministratorViewModel
    {

        #region Свойства

        // Выбранная новость
        private MyModelLibrary.News _SelectedNews;
        public MyModelLibrary.News SelectedNews
        {
            get
            {
                return _SelectedNews;
            }
            set
            {
                _SelectedNews = value;
                RaisePropertyChanged("SelectedNews");
            }
        }

        #endregion

        #region Команды

        public ICommand DeleteNews
        {
            get
            {
                return new RelayCommand(() =>
                {

                    // Удаляем новость
                    bool delete = MyNewsLogic.DeleteNews(MyAcc, SelectedNews.IdNews); // Удаляем из базы данных
                    NewsList.Remove(SelectedNews);

                    // Если новость удалили успешно, то обнули SelectedNews
                    if (delete == true)
                    {
                        SelectedNews = null;
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        public NewsListViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            MyNewsLogic = new NewsLogic();
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            NewsList = MyNewsLogic.GetNews();
        }

        #endregion
    }
}
