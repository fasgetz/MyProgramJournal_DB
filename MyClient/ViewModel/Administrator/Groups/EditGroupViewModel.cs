using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу EditGroupPage
    /// </summary>

    public class EditGroupViewModel : CreateGroupViewModel
    {

        #region Команды

        public ICommand EditGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Проверяем: все ли входные данные выбраны
                    if (SelectedGroup != null)
                    {
                        // Инициализируем новые данные
                        SelectedGroup.GroupName = GroupName;
                        SelectedGroup.idSpeciality = SelectedSpeciality.idSpeciality;

                        // Если edit == true, то успешное редактирование
                        bool edit = MyAdminLogic.EditGroup(MyAcc, SelectedGroup);

                        // Если группа отредактирована успешно, то перейди на страницу всех групп
                        if (edit == true)
                        {
                            locator.MyCreateGroupVM.groups = MyAdminLogic.GetGroups();

                            // Перейди в Page групп
                            navigation.Navigate("View/Administrator/Groups/GroupsListPage.xaml");                            
                        }
                    }
                });
            }
        }

        #endregion

        #region Конструктор

        public EditGroupViewModel()
        {
            // Получаем аккаунт и группу из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.Groups>>(this, GetGroup);

            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic(); // Инициализируем логику администратора
            dialog = new DialogService(); // Инициализируем диалоговые окна  
        }

        //Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content; // Аккаунт
            speciality_list = MyAdminLogic.GetSpecialityCodes(); // Загрузить список специальностей
        }

        // Вспомогательный метод для мессенджера, который проинициализирует выбранную группу из прошлого vm при создании текущей vm
        private void GetGroup(GenericMessage<MyModelLibrary.Groups> GetGroup)
        {
            // Инициализируем свойства
            SelectedGroup = GetGroup.Content; // Свойство выбранной группы
            GroupName = SelectedGroup.GroupName;
            SelectedSpeciality = speciality_list.FirstOrDefault(i => i.idSpeciality == SelectedGroup.idSpeciality);
        }
        
        #endregion

    }
}
