using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.ServiceLogic;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Linq;
using System.Collections;

namespace MyClient.ViewModel.Administrator.News
{
    public class EditNewsViewModel : NewsListViewModel
    {

        #region Команды

        // Команда на добавление изображения в список
        public ICommand AddImage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Создаем список изображений (Если он пустой)
                    if (ImagesList == null)
                        ImagesList = new System.Collections.ObjectModel.ObservableCollection<MyModelLibrary.Images>();

                    // Создаем image
                    MyModelLibrary.Images img = dialog.AddImage(SelectedNews);

                    // Если img выбрали, то добавь его в список изображений
                    if (img != null)
                        ImagesList.Add(img);
                });
            }
        }

        // Команда редактирования новости
        public ICommand EditNewsCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (SelectedNews != null)
                    {                        
                        SelectedNews = new MyModelLibrary.News(SelectedNews.IdNews, title, text);
                        if (ImagesList != null)
                            SelectedNews.Images = ImagesList.ToList();

                        // Посылаем запрос на редактирование новости
                        bool edit_news = MyNewsLogic.EditNews(MyAcc, SelectedNews);

                        // Если новость успешна отредактирована, то перейди в список новостей
                        if (edit_news == true)
                        {
                            // Перейди в Page страницы новостей
                            locator.NewsListVM.NewsList = MyNewsLogic.GetNews();
                            navigation.Navigate("View/Administrator/News/NewsListPage.xaml");
                        }
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        public EditNewsViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<ArrayList>>(this, GetList);

            MyAdminLogic = new AdministratorLogic(); // Инициализируем логику администратора
            MyNewsLogic = new NewsLogic();
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        private void GetList(GenericMessage<ArrayList> GetAcc)
        {
            // Инициализируем необходимые нам свойства при открытии данной страницы
            MyAcc = (MyModelLibrary.accounts)GetAcc.Content[0];
            SelectedNews = (MyModelLibrary.News)GetAcc.Content[1];
            title = SelectedNews.Title;
            text = SelectedNews.Content;
            if (SelectedNews.Images != null)
                ImagesList = new System.Collections.ObjectModel.ObservableCollection<MyModelLibrary.Images>(SelectedNews.Images);
        }


        #endregion

    }
}
