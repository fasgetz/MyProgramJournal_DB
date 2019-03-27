using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using MyClient.ProgramLogic.ServiceLogic;
using MyClient.ViewModel._Navigation;
using MyClient.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Windows.Input;


namespace MyClient.ViewModel._VMCommon
{

    /// <summary>
    /// VM представляет общую страницу расписаний занятий
    /// </summary>

    public class AllScheduleViewModel : AccountVM
    {

        // Выбранная дата
        private DateTime _SelectDate;
        public DateTime SelectDate
        {
            get
            {
                return _SelectDate;
            }
            set
            {
                _SelectDate = value;
                if (SelectedGroup != null)
                    SelectedGroup = null;
                RaisePropertyChanged($"SelectDate");
            }
        }

        // Выбранная группа
        private MyModelLibrary.Groups _SelectedGroup;
        public MyModelLibrary.Groups SelectedGroup
        {
            get
            {
                return _SelectedGroup;
            }
            set
            {
                _SelectedGroup = value;

                // Если группу выбрали, загрузи список занятий
                if (value != null && SelectDate != null)
                    lessons = new CommonLogic().GetLessons(value, SelectDate);
                else if (value == null && lessons != null)
                    lessons = null;

                RaisePropertyChanged("SelectedGroup");
            }
        }

        public AllScheduleViewModel()
        {
            SelectDate = DateTime.Now;

            // Регистрируем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании новой vm
        public void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            groups = new CommonLogic().GetGroups();
            lessons = null;
            //groups.ElementAt(0).GroupName
        }
    }
}
