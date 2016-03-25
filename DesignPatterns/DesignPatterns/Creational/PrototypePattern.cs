using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    class PrototypePattern
    {
        public static void ShowDemo()
        {
            Console.WriteLine("\nOriginal object");
            RocketEngine original = new RocketEngine();
            original.DisplayDetails();

            Console.WriteLine("\nCloned object");
            var cloned = (RocketEngine)original.Clone();
            cloned.DisplayDetails();

            Console.WriteLine("\nCloned object after modified");
            cloned.Part2.Part2B = "Black Petrol";
            cloned.DisplayDetails();

            Console.WriteLine("\nOriginal object after cloned one changed");
            original.DisplayDetails();
            
        }
    }

    class RocketEngine : ICloneable
    {
        public EnginePart1 Part1 { get; set; }
        public EnginePart2 Part2 { get; set; }

        public RocketEngine()
        {
            Part1 = new EnginePart1();
            Part2 = new EnginePart2();
        }

        public RocketEngine(RocketEngine engine)
        {
            Part1 = (EnginePart1)engine.Part1.Clone();
            Part2 = (EnginePart2)engine.Part2.Clone();
        }

        public object Clone()
        {
            return new RocketEngine(this);
        }

        public void DisplayDetails()
        {
            Console.WriteLine(Part1.Part1A);
            Console.WriteLine(Part2.Part2A);
            Console.WriteLine(Part2.Part2B);
        }
    }

    class EnginePart1 : ICloneable
    {
        public string Part1A = "Satelite";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    class EnginePart2
    {
        public string Part2A = "Nitro Boosters";
        public string Part2B = "White Petrol";

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
