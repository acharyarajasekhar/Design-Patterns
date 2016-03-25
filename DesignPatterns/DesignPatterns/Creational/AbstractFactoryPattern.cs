using DesignPatterns.Creational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class AbstractFactoryPattern
    {
        public static void ShowDemo()
        {
            IComputerFactory factory = UniversalFactory.GetFactory("HP");
            IComputer computer = factory.GetComputer("NoteBook");
        }
    }

    public interface IComputer { }

    public class Desktop : IComputer
    {
        public Desktop(string brand)
        {
            Console.WriteLine("Desktop product by " + brand);
        }
    }

    public class Laptop : IComputer
    {
        public Laptop(string brand)
        {
            Console.WriteLine("Laptop product by " + brand);
        }
    }

    public class NoteBook : IComputer
    {
        public NoteBook(string brand)
        {
            Console.WriteLine("NoteBook product by " + brand);
        }
    }

    public interface IComputerFactory
    {
        IComputer GetComputer(string model);
    }

    public class LenovoFactory : IComputerFactory
    {
        public IComputer GetComputer(string model)
        {
            string brand = "Lenovo";

            switch (model)
            {
                case "Desktop": return new Desktop(brand);
                case "Laptop": return new Laptop(brand);
                case "NoteBook": return new NoteBook(brand);                
            }
            throw new Exception("Invalid model from " + brand);
        }
    }

    public class HPFactory : IComputerFactory
    {
        public IComputer GetComputer(string model)
        {
            string brand = "HP";

            switch (model)
            {
                case "Desktop": return new Desktop(brand);
                case "Laptop": return new Laptop(brand);
                case "NoteBook": return new NoteBook(brand);
            }
            throw new Exception("Invalid model from " + brand);
        }
    }

    public class UniversalFactory
    {
        public static IComputerFactory GetFactory(string brand)
        {
            switch (brand)
            {
                case "HP": return new HPFactory();
                case "Lenovo": return new LenovoFactory();
            }
            throw new Exception("Invalid " + brand);
        }
    }
}
