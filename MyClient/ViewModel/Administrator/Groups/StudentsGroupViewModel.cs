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

        // Список студентов без группы
        private ObservableCollection<MyModelLibrary.Users> _NotGrouppedStudents;
        public ObservableCollection<MyModelLibrary.Users> NotGroupedStudents
        {
            get
            {
                return _NotGrouppedStudents;
            }
            set
            {
                _NotGrouppedStudents = value;
                RaisePropertyChanged("NotGroupedStudents");
            }
        }

        // Список студентов группы
        private ObservableCollection<MyModelLibrary.Users> _StudentsList;
        public ObservableCollection<MyModelLibrary.Users> StudentsList
        {
            get
            {
                return _StudentsList;
            }
            set
            {
                _StudentsList = value;
                RaisePropertyChanged("StudentsList");
            }
        }


        #endregion

        #region Команды

        // Команда фильтрации списка поиска в ComboBoxe's
        public ICommand SearchedInCombobox
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // Если в поле ComboBox что-то ввели, то произведи поиск
                    if (text != null)
                    {
                        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(NotGroupedStudents);
                        cv.Filter = s =>
                        {
                            MyModelLibrary.Users u = s as MyModelLibrary.Users;
                            return (u.GetFIO.StartsWith(text));
                        };
                    }

                });
            }
        }

        // Команда удаления студенты из группы


        // Команда добавления студента в группу
        public ICommand AddStudentInGroup
        {
            get
            {
                return new RelayCommand(() =>
                {
                    //Если выбранный текущий аккаунт != null и студента выбрали в ComboBox, то приступи к добавлению
                    if (MyAcc != null && SelectedUser != null)
                    {
                        // Если студент добавлен в группу, то true, иначе false
                        bool AddedStudent = MyAdminLogic.AddStudentInGroup(MyAcc, new MyModelLibrary.StudentsGroup(SelectedUser.idUser, SelectedGroup.idGroup));

                        // Если студент добавлен в группу, то обнови список студентов
                        if (AddedStudent == true)
                        {
                            // Обновить списки
                            StudentsList = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, SelectedGroup.idGroup).OrderBy(i => i.StudentsGroup.NumberInJournal)); // Список студентов группы, отсортированный по номеру в журнале
                            NotGroupedStudents = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, 0)); // Список студентов без группы   

                        }
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

            try
            {
                // Получаем список студентов
                StudentsList = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, SelectedGroup.idGroup).OrderBy(i => i.StudentsGroup.NumberInJournal)); // Список студентов группы, отсортированный по номеру в журнале
                NotGroupedStudents = new ObservableCollection<MyModelLibrary.Users>(MyAdminLogic.GetStudentsInGroup(MyAcc, 0)); // Список студентов без группы   
            }
            catch(ArgumentNullException)
            {
                dialog.ShowMessage($"Ошибка: вы получили пустые списки");
            }
       
        }

        #endregion
    }
}
