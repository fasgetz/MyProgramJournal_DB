using GalaSoft.MvvmLight;
using MyClient.ProgramLogic.DialogServices;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace MyClient.ProgramLogic.ServiceLogic
{

    /// <summary>
    /// Класс предназначен для организации логики работы студентов с сервером
    /// </summary>

    internal class StudentLogic : CommonLogic
    {

        // Метод, который прогружает список дисциплин студента в семестре
        public List<MyModelLibrary.GroupDisciplines> GetStudentsDiscipline(MyModelLibrary.accounts MyAcc, int? semestr)
        {
            try
            {
                using (client = new MyService.TransferServiceClient())
                    return client.GetStudentsDiscipline(MyAcc, semestr);
            }
            catch (FaultException<AccountService.AccountConnectedException> ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MyDialog.ShowMessage(ex.Message);
            }

            return null; // Возвращаем null, если не удалось получить список
        }


        // Конструктор
        public StudentLogic()
        {
            MyDialog = new DialogService(); // Инициализируем диалог
        }
    }
}
