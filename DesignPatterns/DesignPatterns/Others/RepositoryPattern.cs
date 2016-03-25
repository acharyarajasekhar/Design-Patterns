using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others
{
    class RepositoryPattern
    {
        public static void ShowDemo()
        {
            //using (var dataContext = new HotelsDataContext())
            //{
            //    var hotelRepository = new Repository_Linq2Sql<Hotel>(dataContext);
            //    var cityRepository = new Repository_Linq2Sql<City>(dataContext);

            //    City city = cityRepository
            //        .SearchFor(c => c.Name.StartsWith("Ams"))
            //        .Single();

            //    IEnumerable<Hotel> orderedHotels = hotelRepository
            //        .GetAll()
            //        .Where(c => c.City.Equals(city))
            //        .OrderBy(h => h.Name);

            //    Console.WriteLine("* Hotels in {0} *", city.Name);

            //    foreach (Hotel orderedHotel in orderedHotels)
            //    {
            //        Console.WriteLine(orderedHotel.Name);
            //    }

            //    Console.ReadKey();
            //}
            Console.WriteLine("Demo of Repository Pattern");
            Console.WriteLine("Read code snippet for understanding");
        }
    }

    interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(T entity);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
    }

    /// <summary>
    /// Repository implementation using Linq to SQL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Repository_Linq2Sql<T> : IRepository<T> where T : class, IMyEntity
    {
        protected Table<T> DataTable;

        public Repository_Linq2Sql(DataContext context)
        {
            DataTable = context.GetTable<T>();
        }

        #region Repository Methods

        public void Insert(T entity)
        {
            DataTable.InsertOnSubmit(entity);
        }

        public void Delete(T entity)
        {
            DataTable.DeleteOnSubmit(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DataTable.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DataTable;
        }

        public T GetById(int id)
        {
            // Sidenote: the == operator throws NotSupported Exception!
            // 'The Mapping of Interface Member is not supported'
            // Use .Equals() instead
            return DataTable.Single(e => e.ID.Equals(id));
        }

        #endregion
    }

    class Repository_EntityFramework<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;

        public Repository_EntityFramework(DbContext context)
        {
            DbSet = context.Set<T>();
        }

        #region Repository Methods

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        #endregion
    }

    interface IMyEntity
    {
        int ID { get; set; }
    }

    class City : IMyEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    class Hotel : IMyEntity
    {
        public int ID { get; set; }
    }


}
