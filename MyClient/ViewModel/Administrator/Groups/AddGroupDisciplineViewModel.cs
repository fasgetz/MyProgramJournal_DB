using GalaSoft.MvvmLight.Messaging;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу AddGroupDisciplinesPage
    /// </summary>

    public class AddGroupDisciplineViewModel : CreateGroupViewModel
    {

        #region Свойства

        // Список дисциплин учителя
        private List<MyModelLibrary.GroupDisciplines> _GroupDisciplines;
        public List<MyModelLibrary.GroupDisciplines> GroupDisciplines
        {
            get
            {
                return _GroupDisciplines;
            }
            set
            {
                _GroupDisciplines = value;                
                RaisePropertyChanged("GroupDisciplines");
            }
        }

        // Свойство выбранный семестр
        private int? _SelectedSemestr;
        public int? SelectedSemestr
        {
            get
            {
                return _SelectedSemestr;
            }
            set
            {
                _SelectedSemestr = value;

                // Если выбрали семестр, то прогрузи данные по нему
                if (value != null)
                {
                    // Список дисциплин группы в семестре
                    GroupDisciplines = MyAdminLogic.GetGroupDisciplines(MyAcc, SelectedGroup, value);

                    // Список дисциплин, которые можно добавить в семестре (То есть которые еще не добавлены)
                    NotAddedDisciplines = MyAdminLogic.GetNotAddedGroupDisciplines(MyAcc, SelectedGroup, Convert.ToByte(value));


                    
                }



                RaisePropertyChanged("SelectedSemestr");
            }
        }

        // Семестры
        private List<int?> _Semesters;
        public List<int?> Semester
        {
            get
            {
                return _Semesters;
            }
            set
            {
                _Semesters = value;
                RaisePropertyChanged("Semester");
            }
        }


        #endregion

        public AddGroupDisciplineViewModel()
        {
            // Получаем аккаунт и группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.Groups>>(this, GetGroup);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new DialogService(); // Инициализируем диалоговые окна  
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content;
        }

        // Вспомогательный метод для мессенджера, который проинициализирует выбранную группу из прошлого vm при создании текущей vm
        private void GetGroup(GenericMessage<MyModelLibrary.Groups> GetGroup)
        {
            // Инициализируем свойства
            SelectedGroup = GetGroup.Content;
            MyGroupName = $"Дисциплины группы {SelectedGroup.GroupName}";
            SelectedSemestr = null;

            Semester = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8 }; // Инициализируем список семестров            
        }
    }
}
