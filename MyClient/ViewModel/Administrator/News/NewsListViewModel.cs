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
using System.Collections;

namespace MyClient.ViewModel.Administrator.News
{

    /// <summary>
    /// VM представляет страницу NewsListPage
    /// </summary>

    public class NewsListViewModel : AdministratorViewModel
    {

        #region Свойства

        protected MyModelLibrary.News MyNews; // Ссылка на текущую новость

        // Выбранное изображение, которое передается в кнопку удалить
        private MyModelLibrary.Images _SelectedImage;
        public MyModelLibrary.Images SelectedImage
        {
            get
            {
                return _SelectedImage;
            }
            set
            {
                _SelectedImage = value;
                RaisePropertyChanged("SelectedImage");
            }
        }

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

        // Команда на удаления картинки из списка
        public ICommand DeleteImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ImagesList.Remove(SelectedImage);
                });
            }
        }

        // Команда редактирования выбранной (SelectedNews) новости
        public ICommand EditNews
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedNews != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.EditNewsVM == null)
                            locator.EditNewsVM = new EditNewsViewModel();

                        // Создаем список, который передадим в следующий контекст VM (MyAcc - текущий аккаунт и SelectedNews - выбранная новость для редактирования)
                        ArrayList list = new ArrayList() { MyAcc, SelectedNews };

                        // Мессенджер: передай в EditNewsViewModel наш MyAcc и SelectedNews в ArrayList
                        Messenger.Default.Send(new GenericMessage<ArrayList>(list)); // Отправляем в следующий DataContext аккаунт

                        // Перейди в Page главной страницы
                        navigation.Navigate("View/Administrator/News/EditNewsPage.xaml");
                    }
                });
            }
        }

        // Команда удаления выбранной (SelectedNews) новости
        public ICommand DeleteNews
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedNews != null)
                    {
                        // Удаляем новость
                        bool delete = MyNewsLogic.DeleteNews(MyAcc, SelectedNews.IdNews); // Удаляем из базы данных
                        NewsList.Remove(SelectedNews);

                        // Если новость удалили успешно, то обнули SelectedNews
                        if (delete == true)
                        {
                            SelectedNews = null;
                        }
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
