using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProgramJournal_DB.Program_logic.FilterData
{

    /// <summary>
    /// Класс предоставляет методы для фильтрации для DataGrid
    /// </summary>
    public static class FilterDataGrid<T>
        where T: class
    {

        // Метод, который передает предикат (Делегат, который разрешает передавать лямда выражения) и возвращает обобщенный список
        public static List<T> GetFilteredList(List<T> list, Func<T, bool> predicate)
        {
            return list.Where(predicate).ToList();
        }

    }
}
