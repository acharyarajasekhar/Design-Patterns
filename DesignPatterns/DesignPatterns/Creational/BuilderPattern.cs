using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Creational
{
    public class BuilderPattern
    {
        public static void ShowDemo()
        {
            var Contractor = new Contractor();
            Contractor.BuildHome(new LowCostHome());
            Contractor.BuildHome(new LuxuryHome());
        }
    }

    public class Contractor
    {
        public void BuildHome(IHome home)
        {
            home.PrepareDesign();
            home.BuildBasement();
            home.BuildPillars();
            home.BuildSeeling();
            home.BuildWalls();
            home.BuildFloor();
        }
    }

    public interface IHome
    {
        void PrepareDesign();
        void BuildBasement();
        void BuildPillars();
        void BuildSeeling();
        void BuildWalls();
        void BuildFloor();
    }

    public class LowCostHome : IHome
    {
        public void PrepareDesign()
        {
            Console.WriteLine("No design");
        }

        public void BuildBasement()
        {
            Console.WriteLine("Build basic basement");
        }

        public void BuildPillars()
        {
            Console.WriteLine("No pillars");
        }

        public void BuildSeeling()
        {
            Console.WriteLine("Build seeling with palm leaves");
        }

        public void BuildFloor()
        {
            Console.WriteLine("Build floor with low cost stones");
        }

        public void BuildWalls()
        {
            Console.WriteLine("Build normal walls");
        }
    }

    public class LuxuryHome : IHome
    {
        public void PrepareDesign()
        {
            Console.WriteLine("Prepare design with engineer");
        }

        public void BuildBasement()
        {
            Console.WriteLine("Build strong basement with concrete");
        }

        public void BuildPillars()
        {
            Console.WriteLine("Build strong pillars with concrete");
        }

        public void BuildSeeling()
        {
            Console.WriteLine("Build strong seeling with concrete");
        }

        public void BuildFloor()
        {
            Console.WriteLine("Build marble floor");
        }

        public void BuildWalls()
        {
            Console.WriteLine("Build stylish walls with showcase");
        }
    }
}
