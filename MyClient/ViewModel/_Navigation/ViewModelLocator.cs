/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:MyClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using MyClient.ViewModel._VMCommon;
using MyClient.ViewModel.Administrator;
using MyClient.ViewModel.Student;
using MyClient.ViewModel.Teacher;

namespace MyClient.ViewModel._Navigation
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Регистрируем наши ViewModel

            // Общие ViewModel
            SimpleIoc.Default.Register<AccountVM>(); // Общая ViewModel
            SimpleIoc.Default.Register<ProfileVM>(); // ViewModel профиля
            SimpleIoc.Default.Register<MainPageVM>(); // ViewModel главной страницы


            // ViewModel администратора
            SimpleIoc.Default.Register<AdministratorViewModel>(); // ViewModel администратора
            SimpleIoc.Default.Register<UsersListPageViewModel>(); // VM списка пользователей
            SimpleIoc.Default.Register<AccountsListPageViewModel>(); // VM списка аккаунтов
            SimpleIoc.Default.Register<CreateAccountVM>(); // VM страницы создания аккаунта
            SimpleIoc.Default.Register<CreateNewsViewModel>(); // VM страница создания новости

            // ViewModel Преподавателя
            SimpleIoc.Default.Register<TeacherViewModel>(); // ViewModel окна учителя

            // ViewModel Студента
            SimpleIoc.Default.Register<StudentViewModel>(); // ViewModel окна студента
        }


        #region VM Студента

        public StudentViewModel StudentVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StudentViewModel>();
            }
        }

        #endregion

        #region VM Преподавателя

        public TeacherViewModel TeacherVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeacherViewModel>();
            }
        }

        #endregion

        #region VM Администратора

        public AdministratorViewModel AdminVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdministratorViewModel>();
            }
        }

        private CreateNewsViewModel _MyCreateNewsVM;
        public CreateNewsViewModel MyCreateNewsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateNewsViewModel>();
            }
            set
            {
                _MyCreateNewsVM = value;
            }
        }

        private CreateAccountVM _MyCreateAccountVM;
        public CreateAccountVM MyCreateAccountVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateAccountVM>();
            }
            set
            {
                _MyCreateAccountVM = value;
            }
        }

        private UsersListPageViewModel _UsersListVM;
        public UsersListPageViewModel UsersListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UsersListPageViewModel>();
            }
            set
            {
                _UsersListVM = value;
            }
        }

        private AccountsListPageViewModel _AccountsListVM;
        public AccountsListPageViewModel AccountsListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountsListPageViewModel>();
            }
            set
            {
                _AccountsListVM = value;
            }
        }
        #endregion

        #region Общие VM

        public AccountVM CommonVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountVM>();
            }
        }


        private ProfileVM _MyProfileVM;
        public ProfileVM MyProfileVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProfileVM>();
            }
            set
            {
                _MyProfileVM = value;
            }
        }

        private MainPageVM _MyMainPageVM;
        public MainPageVM MyMainPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageVM>();
            }
            set
            {
                _MyMainPageVM = value;
            }
        }

        #endregion




        // Для очистки ViewModel (пока нет необходимости в реализации)
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}