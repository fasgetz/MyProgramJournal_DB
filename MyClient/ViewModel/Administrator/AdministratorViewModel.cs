using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;

namespace MyClient.ViewModel.Administrator
{

    /// <summary>
    /// ViewModel Администратора
    /// </summary>


    public class AdministratorViewModel : AccountVM
    {

        #region Команды перехода по страницам (Только страницы администратора)

        // Команда, которая перейдет на UsersListPage
        public ICommand OpenUsersList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.UsersListVM == null)
                        locator.UsersListVM = new UsersListPageViewModel();


                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/UsersPages/UsersListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на AccountsListPage
        public ICommand OpenAccountsList
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.AccountsListVM == null)
                        locator.AccountsListVM = new AccountsListPageViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/AccountsPages/AccountsListPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на CreateAccountPage
        public ICommand OpenCreateAccountPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyCreateAccountVM == null)
                        locator.MyCreateAccountVM = new CreateAccountVM();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/AccountsPages/CreateAccountPage.xaml");
                });
            }
        }

        // Команда, которая перейдет на CreateNewsPage
        public ICommand OpenCreateNewsPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если наша vm = null, то проинициализируй ее
                    if (locator.MyCreateNewsVM == null)
                        locator.MyCreateNewsVM = new CreateNewsViewModel();

                    // Мессенджер: передай UsersListPageVM наш MyAcc
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт

                    // Перейди в Page главной страницы
                    navigation.Navigate("View/Administrator/News/AddNewsPage.xaml");
                });
            }
        }

        #endregion


        #region Конструктор

        // Конструктор класса
        public AdministratorViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            
            locator = new ViewModelLocator(); // Инициализируем локатор VM
            navigation = new NavigateViewModel(); // Инициализируем навигацию
            dialog = new DialogService(); // Для диалогов

            // Задаем текущий Instance для общения клиента с сервером
            MyAccountLogic = new ProgramLogic.ServiceLogic.AccountLogic(new System.ServiceModel.InstanceContext(this));
        }


        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion

    }
}
