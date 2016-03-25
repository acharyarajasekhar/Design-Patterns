using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class FlyweightPattern
    {
        public static void ShowDemo()
        {
            List<ISoldier> soldiersWithFlyWeight = new List<ISoldier>();
            List<ISoldier> soldiersWithOutFlyWeight = new List<ISoldier>();
            SoldierBaseFactory factory = new SoldierBaseFactory();

            for (int i = 0; i < 10; i++)
            {
                ISoldier soldier = new Soldier();
                soldier.Name = "Soldier " + i;
                if(i%2 == 0)
                    soldier.Owner = new TeamA();
                else
                    soldier.Owner = new TeamB();
                soldiersWithFlyWeight.Add(soldier);
            }

            foreach (var soldier in soldiersWithFlyWeight)
            {
                soldier.Base = factory.GetSoldierBase();
                soldier.Base.DoWalk(soldier);
            }

            for (int i = 0; i < 10; i++)
            {
                ISoldier soldier = new Soldier();
                soldier.Name = "Soldier " + i;
                if (i % 2 == 0)
                    soldier.Owner = new TeamA();
                else
                    soldier.Owner = new TeamB();
                soldier.Base = new SoldierBase();
                soldiersWithOutFlyWeight.Add(soldier);
            }

            foreach (var soldier in soldiersWithFlyWeight)
            {
                soldier.Base.DoWalk(soldier);
            }

            Console.WriteLine("\nTotal bytes used with flyweight usage " + GetObjectSize(soldiersWithFlyWeight));
            Console.WriteLine("Total bytes used without flyweight usage " + GetObjectSize(soldiersWithOutFlyWeight));
        }

        /// <summary>
        /// Calculates the lenght in bytes of an object 
        /// and returns the size 
        /// </summary>
        /// <param name="TestObject"></param>
        /// <returns></returns>
        private static int GetObjectSize(object TestObject)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            bf.Serialize(ms, TestObject);
            Array = ms.ToArray();
            return Array.Length;
        }
    }

    interface ISoldier
    {
        #region Intrinsic Fields
        ISoldierBase Base { get; set; }
        #endregion

        #region Extrinsic Fields
        string Name { get; set; }
        ITeam Owner { get; set; }
        #endregion
    }

    interface ISoldierBase
    {
        int NoOfHeads { get; }
        int NoOfHands { get; }
        int NoOfLegs { get; }
        int NoOfBodies { get; }

        void DoWalk(ISoldier soldier);
        void DoRun(ISoldier soldier);
        void DoFire(ISoldier soldier);
    }

    [Serializable]
    class SoldierBase : ISoldierBase
    {
        public int NoOfHeads
        {
            get { return 1; }
        }

        public int NoOfHands
        {
            get { return 2; }
        }

        public int NoOfLegs
        {
            get { return 2; }
        }

        public int NoOfBodies
        {
            get { return 1; }
        }

        public void DoWalk(ISoldier soldier)
        {
            Console.WriteLine(GetSoldierDetails(soldier) + " is walking");
        }

        public void DoRun(ISoldier soldier)
        {
            Console.WriteLine(GetSoldierDetails(soldier) + " is running");
        }

        public void DoFire(ISoldier soldier)
        {
            Console.WriteLine(GetSoldierDetails(soldier) + " is firing");
        }

        private string GetSoldierDetails(ISoldier soldier)
        {
            return "Soldier named '" + soldier.Name
                + "' from team '" + soldier.Owner.Name
                + "' with dress color '" + soldier.Owner.DressColor + "'"
                + "' with no of heads '" + soldier.Base.NoOfHeads + "'"
                + "' with no of hands '" + soldier.Base.NoOfHands + "'"
                + "' with no of legs '" + soldier.Base.NoOfLegs + "'"
                + "' with no of bodies '" + soldier.Base.NoOfBodies + "'";
        }


        public char[] HeavyObject = new char[10000];
    }

    [Serializable]
    class Soldier : ISoldier
    {
        public ISoldierBase Base { get; set; }

        public string Name { get; set; }

        public ITeam Owner { get; set; }
    }

    interface ITeam
    {
        string Name { get; }
        string DressColor { get; }
    }

    [Serializable]
    class TeamA : ITeam
    {
        public string Name { get { return "A"; } }
        public string DressColor { get { return "Blue"; } }
    }

    [Serializable]
    class TeamB : ITeam
    {
        public string Name { get { return "B"; } }
        public string DressColor { get { return "Red"; } }
    }

    class SoldierBaseFactory
    {
        private ISoldierBase _soldierBase;

        public ISoldierBase GetSoldierBase()
        {
            if (_soldierBase == null)
                _soldierBase = new SoldierBase();

            return _soldierBase;
        }
    }
}
