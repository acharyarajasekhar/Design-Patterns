using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class MediatorPattern
    {
        public static void ShowDemo()
        {
            AirTrafficController controller = new AirTrafficController();

            Flight toUS = new Flight(controller, "US Flight");            
            Flight toUK = new Flight(controller, "UK Flight");
            Flight toUE = new Flight(controller, "UE Flight");

            controller.Register(toUS);
            controller.Register(toUK);
            controller.Register(toUE);

            toUK.UpdateStatus("I am taking off");
            toUS.UpdateStatus("I landed on runway");
            toUE.UpdateStatus("I am flying at 38000 ft altitude");            
        }
    }

    class AirTrafficController
    {
        public List<Flight> Flights = new List<Flight>();

        public void Register(Flight flight)
        {
            Flights.Add(flight);
        }

        public void AnnounceTrafficUpdate(string message, Flight flight)
        {
            foreach (var flght in Flights.Where(f => f.Name != flight.Name))
            {
                flight.ReceiveOtherFlghitsUpdate(flight.Name + " : " + message + "\n");
            }
            
        }
    }

    class Flight
    {
        public string Name { get; set; }
        public AirTrafficController _controller { get; set; }
        public string Status { get; set; }

        public Flight(AirTrafficController controller, string name)
        {
            _controller = controller;
            Name = name;
        }

        public void UpdateStatus(string update)
        {
            Status = update;
            _controller.AnnounceTrafficUpdate(Status, this);
        }

        public void ReceiveOtherFlghitsUpdate(string msg)
        {
            Console.WriteLine("This is "
                    + Name
                    + "\nTraffic update from controller to me reg. " + msg);
        }
    }
}
