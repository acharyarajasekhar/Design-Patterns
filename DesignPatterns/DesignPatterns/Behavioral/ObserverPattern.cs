using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class ObserverPattern
    {
        public static void ShowDemo()
        {
            Contract c = new Contract { InitialPricingPlan = new PricingPlan { Dues = 50 } };

            Signee s1 = new Signee();
            Signee s2 = new Signee();
            Signee s3 = new Signee();

            var d1 = c.AddSignee(s1);
            var d2 = c.AddSignee(s2);
            var d3 = c.AddSignee(s3);

            Console.WriteLine("S1 current dues : " + s1.CurrentDues);
            Console.WriteLine("S2 current dues : " + s2.CurrentDues);
            Console.WriteLine("S3 current dues : " + s3.CurrentDues);

            c.InitialPricingPlan = new PricingPlan { Dues = 100 };

            Console.WriteLine("S1 current dues : " + s1.CurrentDues);
            Console.WriteLine("S2 current dues : " + s2.CurrentDues);
            Console.WriteLine("S3 current dues : " + s3.CurrentDues);

            c.InitialPricingPlan = new PricingPlan { Dues = -10 };

            Console.WriteLine("S1 current dues : " + s1.CurrentDues);
            Console.WriteLine("S2 current dues : " + s2.CurrentDues);
            Console.WriteLine("S3 current dues : " + s3.CurrentDues);

            d1.Dispose();
            d3.Dispose();

            c.InitialPricingPlan = new PricingPlan { Dues = 150 };

            Console.WriteLine("S1 current dues : " + s1.CurrentDues);
            Console.WriteLine("S2 current dues : " + s2.CurrentDues);
            Console.WriteLine("S3 current dues : " + s3.CurrentDues);
        }
    }

    class PricingPlan
    {
        public decimal Dues { get; set; }
    }

    class Contract
    {
        private List<Signee> _signees;
        private ContractTracker _tracker;

        public Contract()
        {
            _signees = new List<Signee>();
            _tracker = new ContractTracker();
        }

        public IDisposable AddSignee(Signee signee)
        {
            _signees.Add(signee);
            var retVal = _tracker.Subscribe(signee);
            _tracker.NotifySubscribers(this);
            return retVal;
        }

        private PricingPlan _initialPricingPlan;

        public PricingPlan InitialPricingPlan
        {
            get { return _initialPricingPlan; }
            set
            {
                _initialPricingPlan = value;
                _tracker.NotifySubscribers(this);
            }
        }
    }

    class ContractTracker : IObservable<Contract>
    {
        private List<IObserver<Contract>> _observers;

        public ContractTracker()
        {
            _observers = new List<IObserver<Contract>>();
        }

        public IDisposable Subscribe(IObserver<Contract> observer)
        {
            if (!this._observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber(this._observers, observer);
        }

        class Unsubscriber : IDisposable
        {
            private List<IObserver<Contract>> _observers;
            private IObserver<Contract> _observer;

            public Unsubscriber(List<IObserver<Contract>> observers, IObserver<Contract> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (this._observer != null && this._observers.Contains(_observer))
                {
                    this._observers.Remove(this._observer);
                }
            }

            private void StopReading()
            {
                foreach (var observer in this._observers)
                {
                    if (this._observers.Contains(observer))
                    {
                        observer.OnCompleted();
                    }
                    _observers.Clear(); // No more dangling events!
                }
            }
        }

        internal void NotifySubscribers(Contract contract)
        {
            if (contract.InitialPricingPlan.Dues <= 0)
            {
                _observers.ForEach(x => x.OnError(new Exception("Something wrong")));
            }
            _observers.ForEach(x => x.OnNext(contract));
        }
    }

    class Signee : IObserver<Contract>
    {
        public decimal? CurrentDues { get; set; }

        public void OnCompleted()
        {
            Console.WriteLine("Observation completed");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Error : " + error.Message);
        }

        public void OnNext(Contract value)
        {
            if (value != null && value.InitialPricingPlan != null)
            {
                CurrentDues = value.InitialPricingPlan.Dues;
            }
        }
    }
}
