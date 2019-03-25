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
            lessons = new ProgramLogic.ServiceLogic.TeacherLogic().GetAttendancesFromJournal(MyAcc, SelectedGroup);
            dialog.ShowMessage(SelectedGroup.DiscipName);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        private void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
        }
    }
}
