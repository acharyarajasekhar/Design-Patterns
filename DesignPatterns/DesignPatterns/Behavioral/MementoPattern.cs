using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class MementoPattern
    {
        public static void ShowDemo()
        {
            CareTaker<MyClass> careTaker = new CareTaker<MyClass>();
            MyObject<MyClass> obj1 = new MyObject<MyClass>();
            obj1.State = new MyClass { Name = "abc" };
            Console.WriteLine(obj1.State.Name);
            careTaker.LastCommitedState = obj1.CommitState();
            obj1.State.Name = "xyz";
            Console.WriteLine(obj1.State.Name);
            obj1.State.Name = "pqr";
            Console.WriteLine(obj1.State.Name);
            obj1.RestoreState(careTaker.LastCommitedState);
            Console.WriteLine(obj1.State.Name);
        }
    }

    class MyClass : ICloneable
    {
        public string Name { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class MyObject<T> where T : ICloneable
    {
        public T State { get; set; }

        public Memento<T> CommitState()
        {
            return new Memento<T>((T)State.Clone());
        }

        public void RestoreState(Memento<T> memento)
        {
            State = memento.State;
        }
    }

    class Memento<T> where T : ICloneable
    {
        public T State { get; private set; }

        public Memento(T State)
        {
            this.State = State;
        }
    }

    class CareTaker<T> where T : ICloneable
    {
        public Memento<T> LastCommitedState { get; set; }
    }
}
