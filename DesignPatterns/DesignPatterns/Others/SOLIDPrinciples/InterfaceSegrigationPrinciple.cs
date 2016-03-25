using System;
using System.Collections.Generic;

namespace DesignPatterns.Others.SOLIDPrinciples
{
    #region Example 1: Database

    public interface IDbService_WithoutISP<T, TKey> where T : IEntity
    {
        IList<T> GetAll();
        T GetById(TKey key);
        bool Update(T entity, TKey key);
        int Add(T entity);
        IList<Customer> GetCustomersByDeptID(int departmentId);
    }

    public interface IDbService<T, Tkey> where T : IEntity
    {
        IList<T> GetAll();
        T GetById(Tkey key);
        bool Update(T entity, Tkey key);
        Tkey Add(T entity);
    }

    public interface ICustomerDbService : IDbService<Customer, int>
    {
        IList<Customer> GetCustomersByDepID(int deptId);
    }

    #endregion

    #region Example 2 : Robot

    interface IWorker_WithoutISP
    {
        void Eat();
        void Work();
    }

    class HumanResource_WithoutISP : IWorker_WithoutISP
    {
        public void Eat()
        {
            Console.WriteLine("I am eating");
        }

        public void Work()
        {
            Console.WriteLine("I am working");
        }
    }

    class Robot_WithoutISP : IWorker_WithoutISP
    {
        public void Eat()
        {
            throw new NotSupportedException();
        }

        public void Work()
        {
            Console.WriteLine("I am working");
        }
    }

    interface IWorkable
    {
        void Work();
    }

    interface IEatable
    {
        void Eat();
    }

    class HumanResource : IWorkable, IEatable
    {
        public void Work()
        {
            Console.WriteLine("I am working");
        }

        public void Eat()
        {
            Console.WriteLine("I am eating");
        }
    }

    class Robot : IWorkable
    {
        public void Work()
        {
            Console.WriteLine("I am working");
        }
    }
    
    #endregion
}
