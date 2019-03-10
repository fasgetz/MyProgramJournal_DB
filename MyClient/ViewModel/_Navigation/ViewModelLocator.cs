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
using MyClient.ViewModel.Administrator.Accounts;
using MyClient.ViewModel.Administrator.Disciplines;
using MyClient.ViewModel.Administrator.Groups;
using MyClient.ViewModel.Administrator.News;
using MyClient.ViewModel.Administrator.Users;
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
            SimpleIoc.Default.Register<EditAccountVM>(); // VM страницы редактирования аккаунта
            SimpleIoc.Default.Register<PersonalDataViewModel>(); // VM страницы персональных данных аккаунта
            SimpleIoc.Default.Register<AccountInfoViewModel>(); // VM страницы информации об аккаунте
            SimpleIoc.Default.Register<CreateGroupViewModel>(); // VM страницы создания группы
            SimpleIoc.Default.Register<NewsListViewModel>(); // VM страницы списка новостей
            SimpleIoc.Default.Register<EditNewsViewModel>(); // VM страницы редактирования новости
            SimpleIoc.Default.Register<StudentsGroupViewModel>(); // VM страницы студентов группы
            SimpleIoc.Default.Register<DisciplinesListViewModel>(); // VM страницы списка дисциплин и создания
            SimpleIoc.Default.Register<TeacherDisciplinesViewModel>(); // VM страницы добавления дисциплины преподавателю


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

        private TeacherDisciplinesViewModel _TeacherDisciplinesVM;
        public TeacherDisciplinesViewModel TeacherDisciplinesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeacherDisciplinesViewModel>();
            }
            set
            {
                _TeacherDisciplinesVM = value;
            }
        }

        private DisciplinesListViewModel _MyDisciplinesListVM;
        public DisciplinesListViewModel MyDisciplinesListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DisciplinesListViewModel>();
            }
            set
            {
                _MyDisciplinesListVM = value;
            }
        }

        private StudentsGroupViewModel _MyStudentsGroupVM;
        public StudentsGroupViewModel MyStudentsGroupVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StudentsGroupViewModel>();
            }
            set
            {
                _MyStudentsGroupVM = value;
            }
        }

        private EditNewsViewModel _EditNewsVM;
        public EditNewsViewModel EditNewsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditNewsViewModel>();
            }
            set
            {
                _EditNewsVM = value;
            }
        }

        private NewsListViewModel _NewsListVM;
        public NewsListViewModel NewsListVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewsListViewModel>();
            }
            set
            {
                _NewsListVM = value;
            }
        }

        private CreateGroupViewModel _MyCreateGroupVM;
        public CreateGroupViewModel MyCreateGroupVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateGroupViewModel>();
            }
            set
            {
                _MyCreateGroupVM = value;
            }
        }

        private AccountInfoViewModel _MyAccountInfoVM;
        public AccountInfoViewModel MyAccountInfoVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AccountInfoViewModel>();
            }
            set
            {
                _MyAccountInfoVM = value;
            }
        }

        private PersonalDataViewModel _MyPersonalDataVM;
        public PersonalDataViewModel MyPersonalDataVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PersonalDataViewModel>();
            }
            set
            {
                _MyPersonalDataVM = value;
            }
        }

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

        private EditAccountVM _MyEditAccountVM;
        public EditAccountVM MyEditAccountVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditAccountVM>();
            }
            set
            {
                _MyEditAccountVM = value;
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