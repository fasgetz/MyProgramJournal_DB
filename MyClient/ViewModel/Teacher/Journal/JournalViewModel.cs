using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System.Linq;
namespace MyClient.ViewModel.Teacher.Journal
{

    /// <summary>
    /// VM представляет страницу журнала выбранной группы
    /// </summary>

    public class JournalViewModel : GroupsJournalViewModel
    {

        #region Свойства

        private MyModelLibrary.Attendance _select;
        public MyModelLibrary.Attendance select
        {
            get
            {
                return _select;
            }
            set
            {
                _select = value;
                dialog.ShowMessage("Ура блять наконецто");
                RaisePropertyChanged("select");
            }
        }

        private List<MyModelLibrary.Users> _Students;
        public List<MyModelLibrary.Users> Students
        {
            get
            {
                return _Students;
            }
            set
            {
                _Students = value;
                RaisePropertyChanged("Students");
            }
        }

        private List<MyModelLibrary.LessonsDate> _lessons;
        public List<MyModelLibrary.LessonsDate> lessons
        {
            get
            {
                return _lessons;
            }
            set
            {
                _lessons = value;
                RaisePropertyChanged("lessons");
            }
        }

        #endregion


        #region Команды

        // Команда перехода на предыдущую страницу
        public ICommand GoBack
        {
            get
            {
                return new RelayCommand(() =>
                {
                    dialog.ShowMessage("kek");

                    // Переходим на страницу расписания
                    navigation.Navigate("View/Teacher/Journal/JournalPage.xaml");
                });
            }
        }

        #endregion


        #region Конструктор

        public JournalViewModel()
        {

            // Получаем выбранную группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.GroupDisciplines>>(this, GetActivity);

            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует деятельность учителя из прошлого vm при создании текущей vm
        private void GetActivity(GenericMessage<MyModelLibrary.GroupDisciplines> GetActivityes)
        {
            SelectedGroup = GetActivityes.Content;
            
            // Получаем список занятий
            lessons = new ProgramLogic.ServiceLogic.TeacherLogic().GetAttendancesFromJournal(MyAcc, SelectedGroup).OrderBy(i => i.DateLesson).OrderBy(i => i.LessonNumber).ToList();

            // Получает список студентов
            Students = new ProgramLogic.ServiceLogic.CommonLogic().GetStudentsInGroup(MyAcc, SelectedGroup.IdGroup).OrderBy(i => i.StudentsGroup.NumberInJournal).ToList();
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }

        #endregion
    }
}
