using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class DecoratorPattern
    {
        public static void ShowDemo()
        {
            IMobileService sim = new BasicService() { MobileNumber = "9776543210" };
            IMobileService simWithPlans = new SMSPack(new GPRSPack(sim));
            simWithPlans.ShowBill();
        }
    }

    interface IMobileService
    {
        string MobileNumber { get; set; }
        decimal Price { get; set; }
        void ShowBill();
    }

    class BasicService : IMobileService
    {
        public string MobileNumber { get; set; }
        public decimal Price { get; set; }

        public void ShowBill()
        {
            Console.WriteLine("Mobile number " + MobileNumber + " basic services bill amount Rs." + Price);
        }
    }

    abstract class ValueAddedService : IMobileService
    {
        protected IMobileService simCard { get; set; }

        public ValueAddedService(IMobileService sim)
        {
            simCard = sim;
        }

        public string MobileNumber { get; set; }
        public decimal Price { get; set; }
        public abstract void ShowBill();
    }

    class SMSPack : ValueAddedService
    {
        public SMSPack(IMobileService sim) : base(sim)
        {
            Price = 10;
        }

        public override void ShowBill()
        {
            simCard.ShowBill();
            Console.WriteLine("Additional amount for SMS pack " + Price);
        }
    }

    class GPRSPack : ValueAddedService
    {
        public GPRSPack(IMobileService sim) : base(sim)
        {
            Price = 50;
        }

        public override void ShowBill()
        {
            simCard.ShowBill();
            Console.WriteLine("Additional amount for Internet pack " + Price);
        }
    }
}
