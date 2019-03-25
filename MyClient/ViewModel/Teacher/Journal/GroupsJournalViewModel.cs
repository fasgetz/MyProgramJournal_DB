using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Teacher.Journal
{
    public class GroupsJournalViewModel : TeacherViewModel
    {
        #region Свойства

        private MyModelLibrary.GroupDisciplines _SelectedGroup;
        public MyModelLibrary.GroupDisciplines SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                _SelectedGroup = value;
                RaisePropertyChanged("SelectedGroup");
            }
        }

        private List<MyModelLibrary.GroupDisciplines> _groups;
        public List<MyModelLibrary.GroupDisciplines> groups
        {
            get => _groups;
            set
            {
                _groups = value;
                RaisePropertyChanged("groups");
            }
        }

        #endregion

        #region Команды

        // Метод открытия журнала выбранной деятельности учителя
        public ICommand OpenJournalActivity
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (locator.JournalVM == null)
                        locator.JournalVM = new Journal.JournalViewModel();


                    if (SelectedGroup != null)
                    {
                        SelectedGroup = new MyModelLibrary.GroupDisciplines(SelectedGroup.DiscipName, SelectedGroup.GroupName, SelectedGroup.idTeacherActivities, SelectedGroup.IdGroup, SelectedGroup.NumberSemester);

                        // Передаем в следующий контекст аккаунт
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт     
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.GroupDisciplines>(SelectedGroup)); // Отправляем в следующий DataContext группу

                        // Переходим на страницу расписания
                        navigation.Navigate("View/Teacher/Journal/JournalPage.xaml");
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        public GroupsJournalViewModel()
        {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;

            // Получаем список групп с сервера
            groups = new ProgramLogic.ServiceLogic.TeacherLogic().GetTeacherDiscipline(MyAcc);      
        }

        #endregion
    }
}
