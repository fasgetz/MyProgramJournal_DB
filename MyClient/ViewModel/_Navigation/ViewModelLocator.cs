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
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using MyClient.ViewModel._VMCommon;
using MyClient.ViewModel.Administrator;
using MyClient.ViewModel.Administrator.Accounts;
using MyClient.ViewModel.Administrator.Disciplines;
using MyClient.ViewModel.Administrator.Groups;
using MyClient.ViewModel.Administrator.News;
using MyClient.ViewModel.Administrator.Orders;
using MyClient.ViewModel.Administrator.Schedule;
using MyClient.ViewModel.Administrator.Users;
using MyClient.ViewModel.Student;
using MyClient.ViewModel.Student.Diary;
using MyClient.ViewModel.Teacher;
using MyClient.ViewModel.Teacher.Journal;
using MyClient.ViewModel.Teacher.TeacherDisciplines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            
            //// ������������ ���� ViewModels
            AdminInitialization(); // �������������� VM
        }


        #region VM ��������

        private DiaryViewModel _DiaryVM;
        public DiaryViewModel DiaryVM
        {
            get => ServiceLocator.Current.GetInstance<DiaryViewModel>();
            set => _DiaryVM = value;
        }

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

        private DisciplinesTeacherViewModel _DisciplinesTeacherVM;
        public DisciplinesTeacherViewModel DisciplinesTeacherVM
        {
            get => ServiceLocator.Current.GetInstance<DisciplinesTeacherViewModel>();
            set => _DisciplinesTeacherVM = value;
        }

        private GroupsJournalViewModel _TeacherGroupsVM;
        public GroupsJournalViewModel TeacherGroupsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GroupsJournalViewModel>();
            }
            set
            {
                _TeacherGroupsVM = value;
            }
        }

        private JournalViewModel _JournalVM;
        public JournalViewModel JournalVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<JournalViewModel>();
            }
            set
            {
                _JournalVM = value;
            }
        }

        public TeacherViewModel TeacherVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TeacherViewModel>();
            }
        }

        #endregion

        #region VM ��������������

        private OrdersWatchViewModel _OrdersWatchVM;
        public OrdersWatchViewModel OrdersWatchVM
        {
            get => ServiceLocator.Current.GetInstance<OrdersWatchViewModel>();
            set => _OrdersWatchVM = value;
        }

        private OrdersViewModel _OrdersVM;
        public OrdersViewModel OrdersVM
        {
            get => ServiceLocator.Current.GetInstance<OrdersViewModel>();
            set => _OrdersVM = value;
        }

        private SessionsViewModel _SessionsVM;
        public SessionsViewModel SessionsVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SessionsViewModel>();
            }
            set
            {
                _SessionsVM = value;
            }
        }

        private ScheduleViewModel _ScheduleVM;
        public ScheduleViewModel ScheduleVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScheduleViewModel>();
            }
            set
            {
                _ScheduleVM = value;
            }
        }

        private EditDisciplineViewModel _MyEditDisciplineVM;
        public EditDisciplineViewModel MyEditDisciplineVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditDisciplineViewModel>();
            }
            set
            {
                _MyEditDisciplineVM = value;
            }
        }

        private EditGroupViewModel _MyEditGroupVM;
        public EditGroupViewModel MyEditGroupVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditGroupViewModel>();
            }
            set
            {
                _MyEditGroupVM = value;
            }
        }

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

        private AllScheduleViewModel _AllScheduleVM;
        public AllScheduleViewModel AllScheduleVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllScheduleViewModel>();
            }
            set
            {
                _AllScheduleVM = value;
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

        #region ��������������� ������ ��� �������������

        // ����� ��� ����������� VM
        private static void AdminInitialization()
        {
            // ����� ViewModel
            SimpleIoc.Default.Register<AccountVM>(); // ����� ViewModel
            SimpleIoc.Default.Register<ProfileVM>(); // ViewModel �������
            SimpleIoc.Default.Register<MainPageVM>(); // ViewModel ������� ��������
            SimpleIoc.Default.Register<AllScheduleViewModel>(); // ViewModel �������� ���������� �������

            //// ViewModel ��������������
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
            SimpleIoc.Default.Register<EditGroupViewModel>(); // VM �������� �������������� ������
            SimpleIoc.Default.Register<EditDisciplineViewModel>(); // VM �������� �������������� ����������
            SimpleIoc.Default.Register<ScheduleViewModel>(); // VM �������� ������� ������
            SimpleIoc.Default.Register<SessionsViewModel>(); // VM �������� ������ ��������
            SimpleIoc.Default.Register<OrdersViewModel>(); // VM �������� ��������
            SimpleIoc.Default.Register<OrdersWatchViewModel>(); // VM �������� ��������� �������� �������

            // ViewModel �������������
            SimpleIoc.Default.Register<TeacherViewModel>(); // ViewModel ���� �������
            SimpleIoc.Default.Register<JournalViewModel>(); // ViewModel �������� ������� ��������� ������
            SimpleIoc.Default.Register<GroupsJournalViewModel>(); // ViewModel �������� ����� �������
            SimpleIoc.Default.Register<DisciplinesTeacherViewModel>(); // ViewModel �������� ��������� �������

            // ViewModel ��������
            SimpleIoc.Default.Register<StudentViewModel>(); // ViewModel ���� ��������
            SimpleIoc.Default.Register<DiaryViewModel>(); // ViewModel �������� ��������
        }

        // ����� ��� ������ ����������� VM
        private static void AdminDeinitiazalition()
        {
            // ����� ViewModel
            SimpleIoc.Default.GetInstance<AccountVM>().Cleanup(); // ����� ViewModel
            SimpleIoc.Default.GetInstance<ProfileVM>().Cleanup(); // ViewModel �������
            SimpleIoc.Default.GetInstance<MainPageVM>().Cleanup(); // ViewModel ������� ��������
            SimpleIoc.Default.GetInstance<AllScheduleViewModel>().Cleanup();

            //// ViewModel ��������������
            SimpleIoc.Default.GetInstance<AdministratorViewModel>().Cleanup(); // ViewModel ��������������
            SimpleIoc.Default.GetInstance<UsersListPageViewModel>().Cleanup(); // VM ������ �������������
            SimpleIoc.Default.GetInstance<AccountsListPageViewModel>().Cleanup(); // VM ������ ���������
            SimpleIoc.Default.GetInstance<CreateAccountVM>().Cleanup(); // VM �������� �������� ��������
            SimpleIoc.Default.GetInstance<CreateNewsViewModel>().Cleanup(); // VM �������� �������� �������
            SimpleIoc.Default.GetInstance<EditAccountVM>().Cleanup(); // VM �������� �������������� ��������
            SimpleIoc.Default.GetInstance<PersonalDataViewModel>().Cleanup(); // VM �������� ������������ ������ ��������
            SimpleIoc.Default.GetInstance<AccountInfoViewModel>().Cleanup(); // VM �������� ���������� �� ��������
            SimpleIoc.Default.GetInstance<CreateGroupViewModel>().Cleanup(); // VM �������� �������� ������
            SimpleIoc.Default.GetInstance<NewsListViewModel>().Cleanup(); // VM �������� ������ ��������
            SimpleIoc.Default.GetInstance<EditNewsViewModel>().Cleanup(); // VM �������� �������������� �������
            SimpleIoc.Default.GetInstance<StudentsGroupViewModel>().Cleanup(); // VM �������� ��������� ������
            SimpleIoc.Default.GetInstance<DisciplinesListViewModel>().Cleanup(); // VM �������� ������ ��������� � ��������
            SimpleIoc.Default.GetInstance<TeacherDisciplinesViewModel>().Cleanup(); // VM �������� ���������� ���������� �������������
            SimpleIoc.Default.GetInstance<AddGroupDisciplineViewModel>().Cleanup(); // VM �������� ���������� ���������� ������
            SimpleIoc.Default.GetInstance<EditGroupViewModel>().Cleanup(); // VM �������� �������������� ������                        
            SimpleIoc.Default.GetInstance<EditDisciplineViewModel>().Cleanup(); // VM �������� �������������� �������� ����������
            SimpleIoc.Default.GetInstance<ScheduleViewModel>().Cleanup(); // VM �������� ������� �����
            SimpleIoc.Default.GetInstance<SessionsViewModel>().Cleanup(); // VM �������� ������ ��������   
            SimpleIoc.Default.GetInstance<OrdersViewModel>().Cleanup(); // VM �������� ��������
            SimpleIoc.Default.GetInstance<OrdersWatchViewModel>().Cleanup(); // VM �������� ��������� �������� �������

            // ViewModel �������������
            SimpleIoc.Default.GetInstance<TeacherViewModel>().Cleanup(); // ViewModel ���� �������
            SimpleIoc.Default.GetInstance<JournalViewModel>().Cleanup(); // ViewModel �������� ������� ��������� ������
            SimpleIoc.Default.GetInstance<GroupsJournalViewModel>().Cleanup(); // ViewModel �������� ����� �������
            SimpleIoc.Default.GetInstance<DisciplinesTeacherViewModel>().Cleanup(); // ViewModel �������� ��������� �������

            // ViewModel ��������
            SimpleIoc.Default.GetInstance<StudentViewModel>().Cleanup(); // ViewModel ���� ��������
            SimpleIoc.Default.GetInstance<DiaryViewModel>().Cleanup(); // ViewModel �������� ��������


            // ����� ViewModel
            SimpleIoc.Default.Unregister<AccountVM>(); // ����� ViewModel
            SimpleIoc.Default.Unregister<ProfileVM>(); // ViewModel �������
            SimpleIoc.Default.Unregister<MainPageVM>(); // ViewModel ������� ��������
            SimpleIoc.Default.Unregister<AllScheduleViewModel>(); // ViewModel �������� ���������� �������

            //// ViewModel ��������������
            SimpleIoc.Default.Unregister<AdministratorViewModel>(); // ViewModel ��������������
            SimpleIoc.Default.Unregister<UsersListPageViewModel>(); // VM ������ �������������
            SimpleIoc.Default.Unregister<AccountsListPageViewModel>(); // VM ������ ���������
            SimpleIoc.Default.Unregister<CreateAccountVM>(); // VM �������� �������� ��������
            SimpleIoc.Default.Unregister<CreateNewsViewModel>(); // VM �������� �������� �������
            SimpleIoc.Default.Unregister<EditAccountVM>(); // VM �������� �������������� ��������
            SimpleIoc.Default.Unregister<PersonalDataViewModel>(); // VM �������� ������������ ������ ��������
            SimpleIoc.Default.Unregister<AccountInfoViewModel>(); // VM �������� ���������� �� ��������
            SimpleIoc.Default.Unregister<CreateGroupViewModel>(); // VM �������� �������� ������
            SimpleIoc.Default.Unregister<NewsListViewModel>(); // VM �������� ������ ��������
            SimpleIoc.Default.Unregister<EditNewsViewModel>(); // VM �������� �������������� �������
            SimpleIoc.Default.Unregister<StudentsGroupViewModel>(); // VM �������� ��������� ������
            SimpleIoc.Default.Unregister<DisciplinesListViewModel>(); // VM �������� ������ ��������� � ��������
            SimpleIoc.Default.Unregister<TeacherDisciplinesViewModel>(); // VM �������� ���������� ���������� �������������
            SimpleIoc.Default.Unregister<AddGroupDisciplineViewModel>(); // VM �������� ���������� ���������� ������
            SimpleIoc.Default.Unregister<EditGroupViewModel>(); // VM �������� �������������� ������
            SimpleIoc.Default.Unregister<EditDisciplineViewModel>(); // VM �������� �������������� ����������
            SimpleIoc.Default.Unregister<ScheduleViewModel>(); // VM �������� ������� �����
            SimpleIoc.Default.Unregister<SessionsViewModel>(); // VM �������� ������ ��������
            SimpleIoc.Default.Unregister<OrdersViewModel>(); // VM �������� ��������
            SimpleIoc.Default.Unregister<OrdersWatchViewModel>(); // VM �������� ��������� �������� �������

            // ViewModel �������������
            SimpleIoc.Default.Unregister<TeacherViewModel>(); // ViewModel ���� �������
            SimpleIoc.Default.Unregister<JournalViewModel>(); // ViewModel �������� ������� ��������� ������
            SimpleIoc.Default.Unregister<GroupsJournalViewModel>(); // ViewModel �������� ����� �������
            SimpleIoc.Default.Unregister<DisciplinesTeacherViewModel>(); // ViewModel �������� ��������� �������

            // ViewModel ��������
            SimpleIoc.Default.Unregister<StudentViewModel>(); // ViewModel ���� ��������
            SimpleIoc.Default.Unregister<DiaryViewModel>(); // ViewModel �������� ��������
        }

        #endregion

        // ��� ������� ViewModel
        public static void Cleanup()
        {
            AdminDeinitiazalition(); // ����������������
            AdminInitialization(); // ���������������
        }
    }
}