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

namespace MyClient.ViewModel.Administrator
{
    public class CreateNewsViewModel : AdministratorViewModel
    {

        #region Свойства

        // Логика работы с новостями
        NewsLogic MyNewsLogic;

        // Заголовок новости
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("title");
            }
        }

        // Текст новости
        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RaisePropertyChanged("text");
            }
        }

        MyModelLibrary.News MyNews; // Новость

        // Список картинок
        private ObservableCollection<MyModelLibrary.Images> _ImagesList;
        public ObservableCollection<MyModelLibrary.Images> ImagesList
        {
            get
            {
                return _ImagesList;
            }
            set
            {
                _ImagesList = value;
                RaisePropertyChanged("ImagesList");
            }
        }

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

        #endregion

        #region Команды

        // Команда на добавление изображения в список
        public ICommand AddImage
        {
            get
            {
                return new RelayCommand(() =>
                {

                    dialog.OpenFileDialog(); // Открываем диалог

                    // Если изображение выбрано, то добавь его в список
                    if (dialog.FilePath != null)
                    {
                        // конвертация изображения в байты
                        byte[] imageData = null;
                        FileInfo fInfo = new FileInfo(dialog.FilePath);
                        long numBytes = fInfo.Length;
                        FileStream fStream = new FileStream(dialog.FilePath, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fStream);
                        imageData = br.ReadBytes((int)numBytes);

                        // получение расширения файла изображения не забыв удалить точку перед расширением
                        string iImageExtension = (Path.GetExtension(dialog.FilePath)).Replace(".", "").ToLower();

                        // Получаем название изображения
                        string name = (Path.GetFileName(dialog.FilePath));

                        // Добавляем изображение в список изображений
                        ImagesList.Add(new MyModelLibrary.Images(MyNews.IdNews,imageData, iImageExtension, dialog.FilePath, name));

                        dialog.FilePath = null; // Обнули путь изображения
                    }
                });
            }
        }

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
                    MyNewsLogic.AddNews(MyAcc, MyNews);

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
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
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
