using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    class FactoryMethodPattern
    {
        public static void ShowDemo()
        {
            var factory = new WorkersFactory();
            var worker = factory.GetWorker("electric");
            worker.DoWork();
        }
    }

    interface IWorker
    {
        void DoWork();
    }

    class Plumber : IWorker
    {
        public void DoWork()
        {
            Console.WriteLine("Setup water pipe lines");
        }
    }

    class Electrician : IWorker
    {
        public void DoWork()
        {
            Console.WriteLine("Setup electric cables");
        }
    }

    class WorkersFactory
    {
        public IWorker GetWorker(string typeOfWorker)
        {
            switch (typeOfWorker)
            {
                case "pipes": return new Plumber();
                case "electric": return new Electrician();
            }
            throw new Exception("Not available");
        }        
    }
}
