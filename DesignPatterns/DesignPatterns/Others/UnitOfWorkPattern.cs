using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others
{
    class UnitOfWorkPattern
    {
        public static void ShowDemo()
        {
            IEntity Customerobj1 = new Customer();// record 1 Customer
            Customerobj1.Id = 1001;
            IEntity Customerobj2 = new Customer();// record 1 Customer
            Customerobj2.Id = 1002;
            IEntity Customerobj3 = new Customer();// record 1 Customer
            Customerobj3.Id = 1003;

            UnitOfWork db = new UnitOfWork();
            db.AddToDB(Customerobj1);
            db.UpdateInDB(Customerobj2);
            db.DeleteFromDB(Customerobj3);
            db.Commit(); // The full inmemory collection is sent for final committ 
        }
    }

    public interface IEntity
    {
        int Id { get; set; }
        void Insert();
        void Update();
        void Delete();
        List<IEntity> Load();
    }

    public class Customer : IEntity
    {
        public int Id { get; set; }

        public void Insert()
        {
            Console.WriteLine("Customer " + Id + " inserted");
        }

        public void Update()
        {
            Console.WriteLine("Customer " + Id + " updated");
        }

        public void Delete()
        {
            Console.WriteLine("Customer " + Id + " deleted");
        }

        public List<IEntity> Load()
        {
            Console.WriteLine("Customer " + Id + " loaded");
            return null;
        }
    }

    class UnitOfWork
    {
        List<IEntity> New = new List<IEntity>();
        List<IEntity> Changed = new List<IEntity>();
        List<IEntity> Deleted = new List<IEntity>();

        public void AddToDB(IEntity obj)
        {
            New.Add(obj);
        }

        public void UpdateInDB(IEntity obj)
        {
            Changed.Add(obj);
        }

        public void DeleteFromDB(IEntity obj)
        {
            Deleted.Add(obj);
        }

        public void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var o in New)
                    o.Insert();
                foreach (var o in Changed)
                    o.Update();
                foreach (var o in Deleted)
                    o.Delete();

                scope.Complete();
            }
        }
    }

    class TransactionScope : IDisposable
    {
        // Check for actual classes in internet
        public void Dispose()
        {
            
        }

        internal void Complete()
        {
            Console.WriteLine("Transaction completed successfully");
        }
    }
}
