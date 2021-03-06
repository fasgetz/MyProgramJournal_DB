﻿using GalaSoft.MvvmLight.Messaging;
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
    /// VM страницы создания группы
    /// </summary>

    public class CreateGroupViewModel : AdministratorViewModel
    {

        #region Свойства

        // Для вывода названия группы 
        private string _MyGroupName;
        public string MyGroupName
        {
            get
            {
                return _MyGroupName;
            }
            set
            {
                _MyGroupName = value;
                RaisePropertyChanged("GroupName");
            }
        }

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

        // Команда редактирования группы
        public ICommand OpenEditGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали группу в списке групп, то открой страницу для редактирования группы
                    if (SelectedGroup != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.MyEditGroupVM == null)
                            locator.MyEditGroupVM = new EditGroupViewModel();

                        // Инициализируем группу
                        SelectedGroup = new MyModelLibrary.Groups(SelectedGroup.idGroup, SelectedGroup.GroupName, Convert.ToInt16(SelectedGroup.idSpeciality));
                        SelectedGroup.StudentsCount = groups.FirstOrDefault(i => i.idGroup == SelectedGroup.idGroup).StudentsCount; // Инициализируем количество студентов

                        // Мессенджер: передай в StudentsGroupPage наш MyAcc и SelectedGroup
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.Groups>(SelectedGroup)); // Отправляем в следующий DataContext группу

                        // Перейди в Page редактирования группы
                        navigation.Navigate("View/Administrator/Groups/EditGroupPage.xaml");
                    }
                });
            }
        }

        // Команда перехода на страницу добавления дисциплины группе
        public ICommand OpenAddGroupDiscipline
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали группу в списке групп, то открой страницу студентов выбранной группы
                    if (SelectedGroup != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.AddGroupDisciplineVM == null)
                            locator.AddGroupDisciplineVM = new AddGroupDisciplineViewModel();


                        // Инициализируем группу
                        SelectedGroup = new MyModelLibrary.Groups(SelectedGroup.idGroup, SelectedGroup.GroupName, Convert.ToInt16(SelectedGroup.idSpeciality));

                        // Мессенджер: передай в StudentsGroupPage наш MyAcc и SelectedGroup
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.Groups>(SelectedGroup)); // Отправляем в следующий DataContext группу

                        // Перейди в Page страницы дисциплин
                        navigation.Navigate("View/Administrator/Groups/AddGroupDisciplinesPage.xaml");
                    }
                });
            }
        }

        // Команда на открытие страницы списка студентов выбранной группы
        public ICommand OpenStudentsPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали группу в списке групп, то открой страницу студентов выбранной группы
                    if (SelectedGroup != null)
                    {
                        // Если наша vm = null, то проинициализируй ее
                        if (locator.MyStudentsGroupVM == null)
                            locator.MyStudentsGroupVM = new StudentsGroupViewModel();

                        // Инициализируем группу
                        SelectedGroup = new MyModelLibrary.Groups(SelectedGroup.idGroup, SelectedGroup.GroupName, Convert.ToInt16(SelectedGroup.idSpeciality));

                        // Мессенджер: передай в StudentsGroupPage наш MyAcc и SelectedGroup
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.accounts>(MyAcc)); // Отправляем в следующий DataContext аккаунт
                        Messenger.Default.Send(new GenericMessage<MyModelLibrary.Groups>(SelectedGroup)); // Отправляем в следующий DataContext группу

                        // Перейди в Page группы студентов
                        navigation.Navigate("View/Administrator/Groups/StudentsGroupPage.xaml");
                    }
                });
            }
        }

        // Команда на удаление группы
        public ICommand RemoveGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если мы выбрали группу в списке групп, то отправь команду на удаление группы
                    if (SelectedGroup != null)
                    {
                        // Отправляем данные на удаление группы
                        bool GroupDeleted = MyAdminLogic.RemoveGroup(MyAcc, SelectedGroup);

                        // Если группа удалена успешно
                        if (GroupDeleted == true)
                        {
                            groups = MyAdminLogic.GetGroups(); // Загрузить список групп
                        }
                    }
                });
            }
        }

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
                    else
                    {
                        dialog.ShowMessage("Заполните все данные!");
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
