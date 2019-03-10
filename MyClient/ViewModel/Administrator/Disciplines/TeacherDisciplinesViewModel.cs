using GalaSoft.MvvmLight.Messaging;
using MyClient.ViewModel._VMCommon;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using MyClient.ViewModel._Navigation;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace MyClient.ViewModel.Administrator.Disciplines
{

    /// <summary>
    /// VM представляет страницу TeachersDisciplinesPage
    /// </summary>

    public class TeacherDisciplinesViewModel : DisciplinesListViewModel
    {


        #region Свойства


        // Список дисциплин, которые ведет учитель
        private List<MyModelLibrary.Discipline> _TeacherDisciplines;
        public List<MyModelLibrary.Discipline> TeacherDisciplines
        {
            get
            {
                return _TeacherDisciplines;
            }
            set
            {
                _TeacherDisciplines = value;
                RaisePropertyChanged("TeacherDisciplines");
            }
        }

        // Выбранный учитель
        private MyModelLibrary.Users _SelectedTeacher;
        public MyModelLibrary.Users SelectedTeacher
        {
            get
            {
                return _SelectedTeacher;
            }
            set
            {
                _SelectedTeacher = value;

                // Если юзера выбрали, то прогрузи его дисциплины (которые он ведет, и которые можно добавить в качестве ведомых дисциплин)
                if (value != null)
                {
                    // Прогружаем список дисциплин, которые ведет учитель
                    TeacherDisciplines = MyAdminLogic.GetTeacherDisciplines(
                        MyAcc,
                        value,
                        1); // 1 значит, что загружаем все дисциплины, которые ведет преподаватель

                    //  Прогружаем список дисциплин, которые не ведет учитель (Для того, чтобы можно было их добавить
                }
                RaisePropertyChanged("SelectedTeacher");
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
                        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(UsersList);
                        cv.Filter = s =>
                        {
                            MyModelLibrary.Users u = s as MyModelLibrary.Users;
                            return (u.GetFIO.StartsWith(text));
                        };
                    }

                });
            }
        }

        #endregion

        #region Свойства

        public TeacherDisciplinesViewModel()
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
            UsersList = MyAdminLogic.GetTeachersList(MyAcc); // Прогружаем список учителей
            TeacherDisciplines = null;
        }

        #endregion

    }
}
