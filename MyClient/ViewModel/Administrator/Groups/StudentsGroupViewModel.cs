using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace MyClient.ViewModel.Administrator.Groups
{

    /// <summary>
    /// VM представляет страницу StudentsGroupPage
    /// </summary>

    public class StudentsGroupViewModel : CreateGroupViewModel
    {

        #region Свойства

        // Тест поиска в combobox
        private List<string> _MyList;
        public List<string> MyList
        {
            get
            {
                return _MyList;
            }
            set
            {
                _MyList = value;
                RaisePropertyChanged("MyList");
            }
        }

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

        #endregion        



        #region Команды

        public ICommand SearchedInCombobox
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (text != null)
                    {                        
                        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(MyList);
                        cv.Filter = s =>
                            ((string)s).IndexOf("a", StringComparison.CurrentCultureIgnoreCase) >= 0;
                    }

                });
            }
        }

        #endregion

        #region Конструктор

        public StudentsGroupViewModel()
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
            MyGroupName = $"Группа {SelectedGroup.GroupName}";

            MyList = new List<string>() { "kek", "kekkka", "alalsa", "korol" };
            MyList.Add("kek");
        }

        #endregion
    }
}
