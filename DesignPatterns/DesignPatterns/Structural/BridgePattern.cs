using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class BridgePattern
    {
        public static void ShowDemo()
        {
            IMessage message =
            new CriticalErrorMessage
            {
                From = "Raja",
                Message = "Server is down. Not able to process your request",
                Subject = "Transaction Failed"
            };
            message.Sender = new SMSSender("9876543210");
            message.Send();
            message.Sender = new EmailSender("sekhar@gmail.com");
            message.Send();
        }
    }

    interface IMessage
    {
        IMessageSender Sender { get; set; }
        string Subject { get; set; }
        string Message { get; set; }
        string From { get; set; }
        void Send();
    }

    class CriticalErrorMessage : IMessage
    {
        public IMessageSender Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string From { get; set; }

        public void Send()
        {
            if (Sender == null)
                throw new Exception("You have not selected sender");
            Sender.Send(this);
        }
    }

    class AlertMessage : IMessage
    {
        public IMessageSender Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string From { get; set; }

        public void Send()
        {
            if (Sender == null)
                throw new Exception("You have not selected sender");
            Sender.Send(this);
        }
    }

    class GeneralMessage : IMessage
    {
        public IMessageSender Sender { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string From { get; set; }

        public void Send()
        {
            if (Sender == null)
                throw new Exception("You have not selected sender");
            Sender.Send(this);
        }
    }

    interface IMessageSender
    {
        void Send(IMessage message);
    }

    class EmailSender : IMessageSender
    {
        private string _toEmailID { get; set; }

        public EmailSender(string toEmailID)
        {
            _toEmailID = toEmailID;
        }

        public void Send(IMessage message)
        {
            Console.WriteLine("\nMessage");
            Console.WriteLine("Subject : " + message.Subject);
            Console.WriteLine("Message : " + message.Message);
            Console.WriteLine("From : " + message.From);
            Console.WriteLine("Sent to email " + _toEmailID);
        }
    }

    class SMSSender : IMessageSender
    {
        private string _toMobileNo { get; set; }

        public SMSSender(string toMobileNo)
        {
            _toMobileNo = toMobileNo;
        }

        public void Send(IMessage message)
        {
            Console.WriteLine("\nMessage");
            Console.WriteLine("Subject : " + message.Subject);
            Console.WriteLine("Message : " + message.Message);
            Console.WriteLine("From : " + message.From);
            Console.WriteLine("Sent to mobile number " + _toMobileNo);
        }
    }

}
