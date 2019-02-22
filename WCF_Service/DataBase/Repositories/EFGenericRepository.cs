using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF_Service.DataBase.Repositories
{
    // Класс, который реализует интерфейс
    class EFGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        MyDB _db;
        DbSet<TEntity> _dbSet;

        // Конструктор, который инициализирует подключение db и ее контекст
        public EFGenericRepository(MyDB db)
        {
            _db = db; // Задаем подключение к бд
            _dbSet = db.Set<TEntity>(); // Задаем контекст базы данных по полученному обобщению
        }


        // Метод для добавления сущности
        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _db.SaveChanges();
        }


        // Метод для редактирования сущности
        public void Edit(TEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
        }

        // Метод поиска сущности по первичному ключу
        public TEntity FindById(int id)
        {
            return _dbSet.Find(id); // Возвращаем сущность найденную по айди
        }

        // Метод для поиска сущности по предикату (условию)
        public TEntity FindQueryEntity(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault(); // Возвращает самую первую сущность
        }


        // Метод для получения всего списка
        public List<TEntity> GetAllList()
        {
            return _dbSet.AsNoTracking().ToList(); // Возвращаем весь список из контекста
        }

        // Метод для поиска сущности по предикату (условию)
        public List<TEntity> GetQueryList(Func<TEntity, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList(); // Вернуть список
        }

        // Метод для удаления сущности
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _db.SaveChanges();
        }
    }
}
