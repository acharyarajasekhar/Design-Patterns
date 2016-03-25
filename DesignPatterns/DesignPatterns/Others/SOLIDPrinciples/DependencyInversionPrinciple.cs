using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Others.SOLIDPrinciples
{
    class CustomerRegistrationService_WithoutDIP
    {
        public void Register(Customer customer)
        {
            try
            {
            }
            catch (Exception exc)
            {
                // File writing logic or db insert logic to log error
            }
        }
    }

    class CustomerRegistrationService
    {
        ILog logger;

        public CustomerRegistrationService(ILog log)
        {
            logger = log;
        }

        public void Register(Customer customer)
        {
            try
            {
            }
            catch (Exception exc)
            {
                // File writing logic or db insert logic to log error
                logger.Log(exc.Message);
            }
        }
    }

    interface ILog
    {
        void Log(string msg);
    }

    abstract class Log2DB : ILog
    {
        public void Log(string msg)
        {
            throw new NotImplementedException();
        }
    }

    abstract class Log2File : ILog
    {
        public void Log(string msg)
        {
            throw new NotImplementedException();
        }
    }
}
