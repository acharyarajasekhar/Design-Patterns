using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class SingletonPattern
    {
        public static void ShowDemo()
        {
            PresidentOfIndia myPresident = PresidentOfIndia.Instance;
            PresidentOfIndia yourPresident = PresidentOfIndia.Instance;
            Console.WriteLine("My President: " + myPresident.Name + "; Your President:  " + yourPresident.Name);
        }
    }

    public class PresidentOfIndia
    {
        public string Name { get; set; }
        private static PresidentOfIndia _instance = new PresidentOfIndia();

        private PresidentOfIndia()
        {
            this.Name = "Pranab Mukharji";
            // Private constructor
        }

        public static PresidentOfIndia Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
