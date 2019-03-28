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
            
            //// Регистрируем наши ViewModels
            AdminInitialization(); // Инициализируем VM
        }


        #region VM Студента

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

        #region VM Преподавателя

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

        #region VM Администратора

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

        #region Общие VM

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

        #region Вспомогательные методы для инициализации

        // Метод для регистрации VM
        private static void AdminInitialization()
        {
            // Общие ViewModel
            SimpleIoc.Default.Register<AccountVM>(); // Общая ViewModel
            SimpleIoc.Default.Register<ProfileVM>(); // ViewModel профиля
            SimpleIoc.Default.Register<MainPageVM>(); // ViewModel главной страницы
            SimpleIoc.Default.Register<AllScheduleViewModel>(); // ViewModel страницы расписания занятий

            //// ViewModel администратора
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
            SimpleIoc.Default.Register<AddGroupDisciplineViewModel>(); // VM страницы добавления дисциплины группе
            SimpleIoc.Default.Register<EditGroupViewModel>(); // VM страницы редактирования группы
            SimpleIoc.Default.Register<EditDisciplineViewModel>(); // VM страницы редактирования дисциплины
            SimpleIoc.Default.Register<ScheduleViewModel>(); // VM страницы занятий группы
            SimpleIoc.Default.Register<SessionsViewModel>(); // VM страницы сессий аккаунта
            SimpleIoc.Default.Register<OrdersViewModel>(); // VM страницы приказов
            SimpleIoc.Default.Register<OrdersWatchViewModel>(); // VM страницы просмотра описания приказа

            // ViewModel Преподавателя
            SimpleIoc.Default.Register<TeacherViewModel>(); // ViewModel окна учителя
            SimpleIoc.Default.Register<JournalViewModel>(); // ViewModel страницы журнала выбранной группы
            SimpleIoc.Default.Register<GroupsJournalViewModel>(); // ViewModel страницы групп учителя
            SimpleIoc.Default.Register<DisciplinesTeacherViewModel>(); // ViewModel страницы дисциплин учителя

            // ViewModel Студента
            SimpleIoc.Default.Register<StudentViewModel>(); // ViewModel окна студента
            SimpleIoc.Default.Register<DiaryViewModel>(); // ViewModel дневника студента
        }

        // Метод для отмены регистрации VM
        private static void AdminDeinitiazalition()
        {
            // Общие ViewModel
            SimpleIoc.Default.GetInstance<AccountVM>().Cleanup(); // Общая ViewModel
            SimpleIoc.Default.GetInstance<ProfileVM>().Cleanup(); // ViewModel профиля
            SimpleIoc.Default.GetInstance<MainPageVM>().Cleanup(); // ViewModel главной страницы
            SimpleIoc.Default.GetInstance<AllScheduleViewModel>().Cleanup();

            //// ViewModel администратора
            SimpleIoc.Default.GetInstance<AdministratorViewModel>().Cleanup(); // ViewModel администратора
            SimpleIoc.Default.GetInstance<UsersListPageViewModel>().Cleanup(); // VM списка пользователей
            SimpleIoc.Default.GetInstance<AccountsListPageViewModel>().Cleanup(); // VM списка аккаунтов
            SimpleIoc.Default.GetInstance<CreateAccountVM>().Cleanup(); // VM страницы создания аккаунта
            SimpleIoc.Default.GetInstance<CreateNewsViewModel>().Cleanup(); // VM страница создания новости
            SimpleIoc.Default.GetInstance<EditAccountVM>().Cleanup(); // VM страницы редактирования аккаунта
            SimpleIoc.Default.GetInstance<PersonalDataViewModel>().Cleanup(); // VM страницы персональных данных аккаунта
            SimpleIoc.Default.GetInstance<AccountInfoViewModel>().Cleanup(); // VM страницы информации об аккаунте
            SimpleIoc.Default.GetInstance<CreateGroupViewModel>().Cleanup(); // VM страницы создания группы
            SimpleIoc.Default.GetInstance<NewsListViewModel>().Cleanup(); // VM страницы списка новостей
            SimpleIoc.Default.GetInstance<EditNewsViewModel>().Cleanup(); // VM страницы редактирования новости
            SimpleIoc.Default.GetInstance<StudentsGroupViewModel>().Cleanup(); // VM страницы студентов группы
            SimpleIoc.Default.GetInstance<DisciplinesListViewModel>().Cleanup(); // VM страницы списка дисциплин и создания
            SimpleIoc.Default.GetInstance<TeacherDisciplinesViewModel>().Cleanup(); // VM страницы добавления дисциплины преподавателю
            SimpleIoc.Default.GetInstance<AddGroupDisciplineViewModel>().Cleanup(); // VM страницы добавления дисциплины группе
            SimpleIoc.Default.GetInstance<EditGroupViewModel>().Cleanup(); // VM страницы редактирования группы                        
            SimpleIoc.Default.GetInstance<EditDisciplineViewModel>().Cleanup(); // VM страницы редактирования названия дисциплины
            SimpleIoc.Default.GetInstance<ScheduleViewModel>().Cleanup(); // VM страницы занятий групп
            SimpleIoc.Default.GetInstance<SessionsViewModel>().Cleanup(); // VM страницы сессий аккаунта   
            SimpleIoc.Default.GetInstance<OrdersViewModel>().Cleanup(); // VM страницы приказов
            SimpleIoc.Default.GetInstance<OrdersWatchViewModel>().Cleanup(); // VM страницы просмотра описания приказа

            // ViewModel Преподавателя
            SimpleIoc.Default.GetInstance<TeacherViewModel>().Cleanup(); // ViewModel окна учителя
            SimpleIoc.Default.GetInstance<JournalViewModel>().Cleanup(); // ViewModel страницы журнала выбранной группы
            SimpleIoc.Default.GetInstance<GroupsJournalViewModel>().Cleanup(); // ViewModel страницы групп учителя
            SimpleIoc.Default.GetInstance<DisciplinesTeacherViewModel>().Cleanup(); // ViewModel страницы дисциплин учителя

            // ViewModel Студента
            SimpleIoc.Default.GetInstance<StudentViewModel>().Cleanup(); // ViewModel окна студента
            SimpleIoc.Default.GetInstance<DiaryViewModel>().Cleanup(); // ViewModel дневника студента


            // Общие ViewModel
            SimpleIoc.Default.Unregister<AccountVM>(); // Общая ViewModel
            SimpleIoc.Default.Unregister<ProfileVM>(); // ViewModel профиля
            SimpleIoc.Default.Unregister<MainPageVM>(); // ViewModel главной страницы
            SimpleIoc.Default.Unregister<AllScheduleViewModel>(); // ViewModel страницы расписания занятий

            //// ViewModel администратора
            SimpleIoc.Default.Unregister<AdministratorViewModel>(); // ViewModel администратора
            SimpleIoc.Default.Unregister<UsersListPageViewModel>(); // VM списка пользователей
            SimpleIoc.Default.Unregister<AccountsListPageViewModel>(); // VM списка аккаунтов
            SimpleIoc.Default.Unregister<CreateAccountVM>(); // VM страницы создания аккаунта
            SimpleIoc.Default.Unregister<CreateNewsViewModel>(); // VM страница создания новости
            SimpleIoc.Default.Unregister<EditAccountVM>(); // VM страницы редактирования аккаунта
            SimpleIoc.Default.Unregister<PersonalDataViewModel>(); // VM страницы персональных данных аккаунта
            SimpleIoc.Default.Unregister<AccountInfoViewModel>(); // VM страницы информации об аккаунте
            SimpleIoc.Default.Unregister<CreateGroupViewModel>(); // VM страницы создания группы
            SimpleIoc.Default.Unregister<NewsListViewModel>(); // VM страницы списка новостей
            SimpleIoc.Default.Unregister<EditNewsViewModel>(); // VM страницы редактирования новости
            SimpleIoc.Default.Unregister<StudentsGroupViewModel>(); // VM страницы студентов группы
            SimpleIoc.Default.Unregister<DisciplinesListViewModel>(); // VM страницы списка дисциплин и создания
            SimpleIoc.Default.Unregister<TeacherDisciplinesViewModel>(); // VM страницы добавления дисциплины преподавателю
            SimpleIoc.Default.Unregister<AddGroupDisciplineViewModel>(); // VM страницы добавления дисциплины группе
            SimpleIoc.Default.Unregister<EditGroupViewModel>(); // VM страницы редактирования группы
            SimpleIoc.Default.Unregister<EditDisciplineViewModel>(); // VM страницы редактирования дисциплины
            SimpleIoc.Default.Unregister<ScheduleViewModel>(); // VM страницы занятий групп
            SimpleIoc.Default.Unregister<SessionsViewModel>(); // VM страницы сессий аккаунта
            SimpleIoc.Default.Unregister<OrdersViewModel>(); // VM страницы приказов
            SimpleIoc.Default.Unregister<OrdersWatchViewModel>(); // VM страницы просмотра описания приказа

            // ViewModel Преподавателя
            SimpleIoc.Default.Unregister<TeacherViewModel>(); // ViewModel окна учителя
            SimpleIoc.Default.Unregister<JournalViewModel>(); // ViewModel страницы журнала выбранной группы
            SimpleIoc.Default.Unregister<GroupsJournalViewModel>(); // ViewModel страницы групп учителя
            SimpleIoc.Default.Unregister<DisciplinesTeacherViewModel>(); // ViewModel страницы дисциплин учителя

            // ViewModel Студента
            SimpleIoc.Default.Unregister<StudentViewModel>(); // ViewModel окна студента
            SimpleIoc.Default.Unregister<DiaryViewModel>(); // ViewModel дневника студента
        }

        #endregion

        // Для очистки ViewModel
        public static void Cleanup()
        {
            AdminDeinitiazalition(); // Деинициализируем
            AdminInitialization(); // Инициаилизируем
        }
    }
}