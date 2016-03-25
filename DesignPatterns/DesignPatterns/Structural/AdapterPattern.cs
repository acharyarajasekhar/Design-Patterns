using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class AdapterPattern
    {
        public static void ShowDemo()
        {
            ISocket socket = new IndianPlug2AmericanSocketAdapter();
            socket.SupplyPower();
        }
    }

    interface ISocket
    {
        void SupplyPower();
    }

    class AmericanSocket : ISocket
    {
        public void SupplyPower()
        {
            Console.WriteLine("Supplying power to american plug");
        }
    }

    interface IPlug
    {
        void RecievePower();
    }

    class IndianPlug : IPlug
    {
        public void RecievePower()
        {
            Console.WriteLine("Recieving power from Indian socket");
        }
    }

    class IndianPlug2AmericanSocketAdapter : AmericanSocket, ISocket
    {
        IPlug plug = new IndianPlug();

        public new void SupplyPower()
        {
            base.SupplyPower();
            Console.WriteLine("Recieving power from american socket");
            Console.WriteLine("Supplying power to indian socket");
            plug.RecievePower();
        }
    }
}
