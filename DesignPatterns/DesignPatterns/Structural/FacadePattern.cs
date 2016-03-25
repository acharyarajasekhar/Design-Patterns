using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class FacadePattern
    {
        public static void ShowDemo()
        {
            IMobileSettings settings = new MobileSettings();
            settings.TurnOnMobileData();
            settings.TurnOnWifi();            
            settings.TurnOffWifi();
            settings.TurnOffMobileData();
        }
    }

    interface IMobileSettings
    {
        void TurnOnWifi();
        void TurnOffWifi();
        void TurnOnMobileData();
        void TurnOffMobileData();
    }

    class MobileSettings : IMobileSettings
    {
        private IWifiSettings wifi = new WifiSettings();
        private IMobileDataSettings mobileData = new MobileDataSettings();

        public void TurnOnWifi()
        {
            wifi.Status = true;
            wifi.DisplayStatus();

            if (mobileData.Status)
                Console.WriteLine("Mobile data is enabled but will not be used upon Wifi enable");
            else
                Console.WriteLine("Wifi will be used for internet access");            
        }

        public void TurnOffWifi()
        {
            wifi.Status = false;
            wifi.DisplayStatus();

            if (mobileData.Status)
                Console.WriteLine("Mobile data is enabled and will be used upon Wifi disable");
            else
                Console.WriteLine("No internet access");            
        }

        public void TurnOnMobileData()
        {
            mobileData.Status = true;
            mobileData.DisplayStatus();

            if (wifi.Status)
                Console.WriteLine("Wifi is on and mobile data will not be used");
            else
                Console.WriteLine("Mobile data will be used for internet access");            
        }

        public void TurnOffMobileData()
        {
            mobileData.Status = false;
            mobileData.DisplayStatus();

            if (wifi.Status)
                Console.WriteLine("Wifi is on and will be used for internet access");
            else
                Console.WriteLine("No internet access");            
        }
    }

    interface IWifiSettings
    {
        bool Status { get; set; }
        void DisplayStatus();
    }

    class WifiSettings : IWifiSettings
    {
        public bool Status { get; set; }

        public void DisplayStatus()
        {
            Console.WriteLine("\nWifi is switched " + (Status ? "On" : "Off"));
        }
    }

    interface IMobileDataSettings
    {
        bool Status { get; set; }
        void DisplayStatus();
    }

    class MobileDataSettings : IMobileDataSettings
    {
        public bool Status { get; set; }

        public void DisplayStatus()
        {
            Console.WriteLine("\nMobile Data is switched " + (Status ? "On" : "Off"));
        }
    }
}
