using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.DataBase.Repositories
{
    /// <summary>
    /// Паттерн Unit of Work, задача которого заключается в управлении доступом к источнику данных. 
    /// Класс, реализующий данный паттерн, не содержит бизнесс-логику, не управляет бизнес-процессами, он только содержит операции над данными
    /// </summary>


    interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity item); // Метод добавления итема

        void Remove(TEntity item); // Метод для удаления итема

        void Edit(TEntity item); // Метод для редактирования итема

        TEntity FindById(int id); // Метод для возвращения итема по айди

        TEntity FindQueryEntity(Func<TEntity, bool> predicate); // Метод для поиска сущности по предикату (условию)

        List<TEntity> GetAllList(); // Метод для получения всего списка

        List<TEntity> GetQueryList(Func<TEntity, bool> predicate); // Метод для получения списка по предикату (условию)

    }
}
