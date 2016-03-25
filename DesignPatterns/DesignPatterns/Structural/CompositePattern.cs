using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class CompositePattern
    {
        public static void ShowDemo()
        {
            IHuman obj1 = new Human("Child 1", null);
            IHuman obj2 = new Human("Child 2", null);
            IHuman obj3 = new Human("Parent 1", new List<IHuman> { obj1, obj2 });

            IHuman obj4 = new Human("Child 3", null);
            IHuman obj5 = new Human("Child 4", null);
            IHuman obj6 = new Human("Parent 2", new List<IHuman> { obj4, obj4 });

            IHuman obj7 = new Human("Grand Parent 1", new List<IHuman> { obj3, obj6 });

            obj7.DisplayFamily();
        }
    }

    interface IHuman
    {
        string Name { get; set; }
        List<IHuman> Children { get; set; }
        void DisplayFamily();
    }

    class Human : IHuman
    {
        public string Name { get; set; }
        public List<IHuman> Children { get; set; }

        public Human(string name, List<IHuman> children)
        {
            Name = name;
            Children = children;
        }

        public void DisplayFamily()
        {
            Console.WriteLine(Name);
            if (Children != null)
            {
                foreach (var child in Children)
                    child.DisplayFamily();
            }
        }
    }
}
