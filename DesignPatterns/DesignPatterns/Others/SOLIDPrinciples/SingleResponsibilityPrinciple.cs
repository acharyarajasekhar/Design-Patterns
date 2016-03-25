using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.SOLIDPrinciples
{
    class CustomerService_WithoutSRP
    {
        public void RegisterCustomer(Customer costomer)
        {
            // Some logic
            ValidateEmail("");
            SendEmail("", "");
        }

        public bool ValidateEmail(string emailId)
        {
             // Some logic
            return true;
        }

        public void SendEmail(string emailId, string message)
        {
            // Some logic
        }
    }

    class CusomterService
    {
        IEmailService _emailService;

        public CusomterService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void RegisterCustomer(Customer costomer)
        {
            // Some logic
            _emailService.ValidateEmail("");
            _emailService.SendEmail("", "");
        }
    }

    interface IEmailService
    {
        bool ValidateEmail(string emailId);
        void SendEmail(string emailId, string message);
    }
}
