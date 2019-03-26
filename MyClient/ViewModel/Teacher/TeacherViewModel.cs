using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;

namespace MyClient.ViewModel.Teacher
{
    public class TeacherViewModel : AccountVM
    {

        // Команда на открытие странпицы групп учителя
        public ICommand OpenTeacherGroups
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Инициализируем VM
                    if (locator.TeacherGroupsVM == null)
                        locator.TeacherGroupsVM = new Journal.GroupsJournalViewModel();

                    // Передаем в следующий контекст аккаунт
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт     

                    // Переходим на страницу расписания
                    navigation.Navigate("View/Teacher/Journal/GroupsPage.xaml");
                });
            }
        }

        // Команда открытия дисциплин учителя
        // Команда на октрытие журнала
        public ICommand OpenTeacherDisciplines
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (locator.DisciplinesTeacherVM == null)
                        locator.DisciplinesTeacherVM = new TeacherDisciplines.DisciplinesTeacherViewModel();

                    // Передаем в следующий контекст аккаунт
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт     

                    // Переходим на страницу расписания
                    navigation.Navigate("View/Teacher/TeacherDisciplines/TeacherDisciplinesPage.xaml");
                });
            }
        }

        // Команда на октрытие журнала
        public ICommand OpenJournal
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (locator.JournalVM == null)
                        locator.JournalVM = new Journal.JournalViewModel();

                    // Передаем в следующий контекст аккаунт
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт     

                    // Переходим на страницу расписания
                    navigation.Navigate("View/Teacher/Journal/JournalPage.xaml");
                });
            }
        }

        #region Конструктор

        // Конструктор класса
        public TeacherViewModel()
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
        protected void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion


    }
}
