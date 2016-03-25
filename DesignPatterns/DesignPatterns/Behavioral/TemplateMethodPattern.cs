using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class TemplateMethodPattern
    {
        public static void ShowDemo()
        {
            MemberAddRequest request = new MemberAddRequest();
            var operation = new MemberAddOperation();
            var res = operation.Execute(request);
        }
    }

    interface IRequest
    {
        string Input { get; set; }
    }

    interface IResult
    {
        string Output { get; set; }
    }

    abstract class Operation<TRequest, TResult>
        where TRequest : IRequest
        where TResult : IResult
    {
        TRequest Request { get; set; }
        TResult Result { get; set; }

        public Operation(TRequest req, TResult res)
        {
            Request = req;
            Result = res;
        }

        public Operation() { }

        public TResult Execute(TRequest request)
        {
            if (!ValidateInput())
                return Result;

            Connect();

            PrepareData();

            if (!ValidateBusinessRules())
                return Result;

            Process();

            return Result;
        }

        private void Connect()
        {
            Console.WriteLine("Connection to DB established");
        }

        protected abstract bool ValidateInput();
        protected abstract void PrepareData();
        protected abstract bool ValidateBusinessRules();
        protected abstract void Process();
    }

    class MemberAddOperation : Operation<MemberAddRequest, MemberAddResult>
    {
        public MemberAddOperation() : base() { }

        protected override bool ValidateInput()
        {
            Console.WriteLine("Member Add Validate Input");
            return true;
        }

        protected override void PrepareData()
        {
            Console.WriteLine("Member Add Prepare Data");
        }

        protected override bool ValidateBusinessRules()
        {
            Console.WriteLine("Member Add Validate Business Rules");
            return true;
        }

        protected override void Process()
        {
            Console.WriteLine("Member Add Process");
        }
    }

    public class MemberAddRequest : IRequest
    {
        public string Input
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public class MemberAddResult : IResult
    {
        public string Output
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
