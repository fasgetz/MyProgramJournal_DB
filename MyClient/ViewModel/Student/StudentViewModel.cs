using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;

namespace MyClient.ViewModel.Student
{
    public class StudentViewModel : AccountVM
    {


        #region Команды

        // Команда на открытие страницы дневника студента
        public ICommand OpenDiary
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (locator.DiaryVM == null)
                        locator.DiaryVM = new Diary.DiaryViewModel();

                    // Передаем в следующий контекст аккаунт
                    Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт     

                    // Переходим на страницу расписания
                    navigation.Navigate("View/Student/Diary/DiaryPage.xaml");
                });
            }
        }

        #endregion

        #region Конструктор

        // Конструктор класса
        public StudentViewModel()
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
