using DesignPatterns.Behavioral;
using DesignPatterns.Creational;
using DesignPatterns.Others;
using DesignPatterns.Structural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Creational Patterns - Abraham became first president of states (ABFPS)

            //AbstractFactoryPattern.ShowDemo();
            //BuilderPattern.ShowDemo();
            //FactoryMethodPattern.ShowDemo();
            //PrototypePattern.ShowDemo();
            //SingletonPattern.ShowDemo();

            #endregion

            #region Structural Patterns - ABCDFFP

            //AdapterPattern.ShowDemo();
            //BridgePattern.ShowDemo();
            //CompositePattern.ShowDemo();            
            //DecoratorPattern.ShowDemo();
            //FacadePattern.ShowDemo();
            //FlyweightPattern.ShowDemo();
            //ProxyPattern.ShowDemo();

            #endregion

            #region Behavioral Patterns - 2 MICS Of TV (MMIICCSSOTV)

            //MediatorPattern.ShowDemo();
            //MementoPattern.ShowDemo();
            //IteratorPattern.ShowDemo();
            //InterpreterPattern.ShowDemo();
            //CommandPattern.ShowDemo();
            //ChainOfResponsibilitiesPattern.ShowDemo();
            //StatePattern.ShowDemo();
            //StrategyPattern.ShowDemo();
            //ObserverPattern.ShowDemo();
            //TemplateMethodPattern.ShowDemo();
            //VisitorPattern.ShowDemo();

            #endregion

            #region Other usefull patterns

            UnitOfWorkPattern.ShowDemo();
            RepositoryPattern.ShowDemo();

            #endregion

            Console.Read();
        }
    }
}
