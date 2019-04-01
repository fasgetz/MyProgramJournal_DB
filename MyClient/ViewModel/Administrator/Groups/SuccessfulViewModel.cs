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
    /// VM представляет страницу успеваемости группы
    /// </summary>

    public class SuccessfulViewModel : AddGroupDisciplineViewModel
    {

        #region Свойства

        // Список итоговых оценок
        private List<MyModelLibrary.FinalAttendances> _FinalAttendances;
        public List<MyModelLibrary.FinalAttendances> FinalAttendances
        {
            get => _FinalAttendances;
            set
            {
                _FinalAttendances = value;
                RaisePropertyChanged("FinalAttendances");
            }
        }

        // Количество студентов, которые учатся на 5
        private int _Mark5;
        public int Mark5
        {
            get => _Mark5;
            set
            {
                _Mark5 = value;
                RaisePropertyChanged("Mark5");
            }
        }

        // Количество студентов, которые учатся на 4
        private int _Mark4;
        public int Mark4
        {
            get => _Mark4;
            set
            {
                _Mark4 = value;
                RaisePropertyChanged("Mark4");
            }
        }

        // Количество студентов, которые учатся на 3
        private int _Mark3;
        public int Mark3
        {
            get => _Mark3;
            set
            {
                _Mark3 = value;
                RaisePropertyChanged("Mark3");
            }
        }

        // Количество студентов, которые учатся на 2
        private int _Mark2;
        public int Mark2
        {
            get => _Mark2;
            set
            {
                _Mark2 = value;
                RaisePropertyChanged("Mark2");
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
                    // Перейди в Page страницы дисциплин
                    navigation.Navigate("View/Administrator/Groups/AddGroupDisciplinesPage.xaml");
                });
            }
        }

        #endregion

        #region Конструктор        

        public SuccessfulViewModel()
        {
            // Получаем аккаунт и деятельность учителя из предыдущей VM
            Messenger.Default.Register<GenericMessage<MyModelLibrary.accounts>>(this, GetAccount);
            Messenger.Default.Register<GenericMessage<MyModelLibrary.GroupDisciplines>>(this, GetActivity);

            // Логика работы администратора с сервером
            MyAdminLogic = new ProgramLogic.ServiceLogic.AdministratorLogic();
        }

        // Вспомогательный метод для мессенджера, который проинициализирует аккаунт из прошлого vm при создании текущей vm
        protected new void GetAccount(GenericMessage<MyModelLibrary.accounts> GetAcc)
        {
            // Инициализируем свойства
            MyAcc = GetAcc.Content;
        }

        // Вспомогательный метод для мессенджера, который проинициализирует деятельность учителя из прошлого vm при создании текущей vm
        protected void GetActivity(GenericMessage<MyModelLibrary.GroupDisciplines> GetActivity)
        {
            // Инициализируем полученное свойство
            SelectedGroupDiscip = GetActivity.Content;

            // Загружаем список итоговых оценок по выбранной деятельности
            FinalAttendances = MyAdminLogic.GetFinalAttendances(MyAcc, SelectedGroupDiscip);

            // Свойство для отображения метки статистики
            text = $"Статистика группы «{SelectedGroupDiscip.GroupName}» Всего студентов {FinalAttendances.Count()}";
            
            // Инициализируем дополнительные свойства метки для отображения (Не значительно и ни на что не влияет)
            SelectedGroupDiscip.GroupName = $"Успеваемость группы «{SelectedGroupDiscip.GroupName}»";
            title = $"Дисциплина «{SelectedGroupDiscip.DiscipName}» семестр {SelectedGroupDiscip.NumberSemester}";

            Mark5 = FinalAttendances.Where(i => i.Mark == 5).Count();
            Mark4 = FinalAttendances.Where(i => i.Mark == 4).Count();
            Mark3 = FinalAttendances.Where(i => i.Mark == 3).Count();
            Mark2 = FinalAttendances.Where(i => i.Mark == 2).Count();
        }

        #endregion
    }
}
