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
using System;

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

            // ������������ ���� ViewModel

            // ����� ViewModel
            SimpleIoc.Default.Register<AccountVM>(); // ����� ViewModel
            SimpleIoc.Default.Register<ProfileVM>(); // ViewModel �������
            SimpleIoc.Default.Register<MainPageVM>(); // ViewModel ������� ��������


            // ViewModel ��������������
            SimpleIoc.Default.Register<AdministratorViewModel>(); // ViewModel ��������������
            SimpleIoc.Default.Register<UsersListPageViewModel>(); // VM ������ �������������
            SimpleIoc.Default.Register<AccountsListPageViewModel>(); // VM ������ ���������
            SimpleIoc.Default.Register<CreateAccountVM>(); // VM �������� �������� ��������
            SimpleIoc.Default.Register<CreateNewsViewModel>(); // VM �������� �������� �������
            SimpleIoc.Default.Register<EditAccountVM>(); // VM �������� �������������� ��������
            SimpleIoc.Default.Register<PersonalDataViewModel>(); // VM �������� ������������ ������ ��������
            SimpleIoc.Default.Register<AccountInfoViewModel>(); // VM �������� ���������� �� ��������
            SimpleIoc.Default.Register<CreateGroupViewModel>(); // VM �������� �������� ������
            SimpleIoc.Default.Register<NewsListViewModel>(); // VM �������� ������ ��������
            SimpleIoc.Default.Register<EditNewsViewModel>(); // VM �������� �������������� �������
            SimpleIoc.Default.Register<StudentsGroupViewModel>(); // VM �������� ��������� ������
            SimpleIoc.Default.Register<DisciplinesListViewModel>(); // VM �������� ������ ��������� � ��������
            SimpleIoc.Default.Register<TeacherDisciplinesViewModel>(); // VM �������� ���������� ���������� �������������
            SimpleIoc.Default.Register<AddGroupDisciplineViewModel>(); // VM �������� ���������� ���������� ������


            // ViewModel �������������
            SimpleIoc.Default.Register<TeacherViewModel>(); // ViewModel ���� �������

            // ViewModel ��������
            SimpleIoc.Default.Register<StudentViewModel>(); // ViewModel ���� ��������
        }


        #region VM ��������

        private StudentViewModel _StudentVM;
        public StudentViewModel StudentVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StudentViewModel>();
            }
            set
            {
                _StudentVM = value;
            }
        }

        #endregion

        #region VM �������������

        public TeacherViewModel TeacherVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeacherViewModel>();
            }
        }

        #endregion

        #region VM ��������������

        private AddGroupDisciplineViewModel _AddGroupDisciplineVM;
        public AddGroupDisciplineViewModel AddGroupDisciplineVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddGroupDisciplineViewModel>();
            }
            set
            {
                _AddGroupDisciplineVM = value;
            }
        }

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

        private AdministratorViewModel _AdminVM;
        public AdministratorViewModel AdminVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdministratorViewModel>();
            }
            set
            {
                _AdminVM = value;
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

        #region ����� VM

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


        private static string _currentKey = System.Guid.NewGuid().ToString();
        public static string CurrentKey
        {
            get
            {
                return _currentKey;
            }
            private set
            {
                _currentKey = value;
            }
        }

        // ��� ������� ViewModel (���� ��� ������������� � ����������)
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
            //((IDisposable)ServiceLocator.Current.GetInstance<AccountsListPageViewModel>()).Dispose();
            //SimpleIoc.Default.Unregister<AccountsListPageViewModel>();
            SimpleIoc.Default.GetInstance<AccountsListPageViewModel>().Cleanup();

            CurrentKey = System.Guid.NewGuid().ToString();
        }
    }
}