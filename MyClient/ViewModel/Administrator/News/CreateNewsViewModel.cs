using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Drawing; // в References подключить одноимённую библиотеку
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using MyClient.ProgramLogic.ServiceLogic;

namespace MyClient.ViewModel.Administrator.News
{
    public class CreateNewsViewModel : NewsListViewModel
    {

        #region Команды

        // Команда на добавление изображения в список
        public ICommand AddImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Создаем image
                    MyModelLibrary.Images img = dialog.AddImage(MyNews);

                    // Если img выбрали, то добавь его в список изображений
                    if (img != null)
                        ImagesList.Add(img);
                });
            }
        }

        // Команда на создание новости
        public ICommand CreateNews
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Инициализируем
                    MyNews.Content = text;
                    MyNews.Title = title;
                    MyNews.Images = ImagesList.ToList();

                    // Отправляем новость на сервер
                    MyNewsLogic = new NewsLogic();

                    bool Added_News = MyNewsLogic.AddNews(MyAcc, MyNews);

                    // Перейди на главную страницу
                    if (Added_News == true)
                        navigation.Navigate("View/CommonPages/MainPage.xaml");

                });
            }
        }

        #endregion

        #region Конструктор

        public CreateNewsViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна          
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            MyNews = new MyModelLibrary.News(); // Создаем новость
            ImagesList = new ObservableCollection<MyModelLibrary.Images>(); // Инициализируем коллекцию
            title = "Новая новость";
            text = string.Empty;
        }

        #endregion

    }
}
