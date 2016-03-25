using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class StrategyPattern
    {
        public static void ShowDemo()
        {
            ContactDetails obj1 = new ContactDetails { EmailID = "FF" };
            AddressDetails obj2 = new AddressDetails { PinCode = "123456" };
            Strategy<IValidator, bool> s1 
                = new Strategy<IValidator, bool>(input => input != null, (input) => input.Validate());
            StrategyContext<IValidator, bool> context = new StrategyContext<IValidator, bool>(s1);

            if (context.Evaluate(new ContactDetailsValidator(obj1)))
                Console.WriteLine("Contact details validation passed");
            else
                Console.WriteLine("Contact details validation failed");
            if (context.Evaluate(new AddressDetailsValidator(obj2)))
                Console.WriteLine("Contact details validation passed");
            else
                Console.WriteLine("Contact details validation failed");
        }
    }

    class Strategy<T, V>
    {
        public Predicate<T> Condition { get; private set; }
        public Func<T, V> Result { get; private set; }

        public Strategy(Predicate<T> condition, Func<T, V> result)
        {
            Condition = condition;
            Result = result;
        }

        public V IsValid(T input)
        {
            if (Condition(input))
                return Result(input);
            return default(V);
        }
    }

    class StrategyContext<T, V>
    {
        private List<Strategy<T, V>> _Strategies { get; set; }

        public StrategyContext(params Strategy<T, V>[] strategies)
        {
            _Strategies = strategies.ToList();
        }

        public V Evaluate(T input)
        {
            var result = default(V);

            foreach (var strategy in _Strategies)
            {
                result = strategy.IsValid(input);
                if (result != null)
                    break;
            }

            return result;
        }
    }

    interface IValidator
    {
        bool Validate();
    }

    class ContactDetailsValidator : IValidator
    {
        private ContactDetails objContactDetails;

        public ContactDetailsValidator(ContactDetails obj1)
        {
            this.objContactDetails = obj1;
        }

        public bool Validate()
        {
            return objContactDetails.EmailID.Contains('@');
        }
    }

    class AddressDetailsValidator : IValidator
    {
        private AddressDetails objAddressDetails;

        public AddressDetailsValidator(AddressDetails obj2)
        {
            this.objAddressDetails = obj2;
        }

        public bool Validate()
        {
            return objAddressDetails.PinCode.Length == 6;
        }
    }

    public class ContactDetails
    {
        public string EmailID { get; set; }
    }

    public class AddressDetails
    {
        public string PinCode { get; set; }
    }
}
