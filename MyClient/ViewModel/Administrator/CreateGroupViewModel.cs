using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator
{

    /// <summary>
    /// VM страницы создания группы
    /// </summary>

    public class CreateGroupViewModel : AdministratorViewModel
    {

        #region Свойства

        // Список кодов специальностей
        private List<MyModelLibrary.Speciality_codes> _speciality_list;
        public List<MyModelLibrary.Speciality_codes> speciality_list
        {
            get
            {
                return _speciality_list;
            }
            set
            {
                _speciality_list = value;
                RaisePropertyChanged("speciality_list");
            }
        }

        // Список существующих групп
        private List<MyModelLibrary.Groups> _groups;
        public List<MyModelLibrary.Groups> groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                RaisePropertyChanged("groups");
            }
        }


        private MyModelLibrary.Speciality_codes _SelectedSpeciality;
        public MyModelLibrary.Speciality_codes SelectedSpeciality
        {
            get
            {
                return _SelectedSpeciality;
            }
            set
            {
                _SelectedSpeciality = value;
                RaisePropertyChanged("SelectedSpeciality");
            }
        }

        // Название группы
        private string _GroupName;
        public string GroupName
        {
            get
            {
                return _GroupName;
            }
            set
            {                
                _GroupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

        #endregion

        #region Команды

        // Команда на создание группы
        public ICommand AddGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (GroupName != string.Empty && SelectedSpeciality != null)
                    {
                        // Создаем группу
                        bool GroupCreated = MyAdminLogic.CreateGroup(MyAcc, new MyModelLibrary.Groups(GroupName, SelectedSpeciality.idSpeciality));

                        // Если группа создана, то обнули свойства
                        if (GroupCreated == true)
                        {
                            DeInitialization();
                            groups = MyAdminLogic.GetGroups();
                        }                            
                    }                    
                });
            }
        }

        #endregion

        #region Конструктор

        // Метод для де инициализации свойств
        private void DeInitialization()
        {
            SelectedSpeciality = null;
            GroupName = string.Empty;
        }

        public CreateGroupViewModel()
         {
            // Получаем аккаунт из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new ProgramLogic.DialogServices.DialogService(); // Инициализируем диалоговые окна  
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            MyAcc = GetAcc.Content;
            speciality_list = MyAdminLogic.GetSpecialityCodes(); // Загрузить список специальностей
            groups = MyAdminLogic.GetGroups(); // Загрузить список групп
            DeInitialization();
        }

        #endregion

    }
}
