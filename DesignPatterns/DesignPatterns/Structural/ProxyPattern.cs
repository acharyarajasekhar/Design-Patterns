using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Structural
{
    class ProxyPattern
    {
        public static void ShowDemo()
        {
            IServer server = new Proxy();
            server.RequestedPage(1);
            server.RequestedPage(2);
            server.RequestedPage(1);
            server.RequestedPage(2);
            server.RequestedPage(3);
            server.RequestedPage(2);
        }
    }

    interface IServer
    {
        void RequestedPage(int pageNo);
    }

    class Server : IServer
    {
        public void RequestedPage(int pageNo)
        {
            Console.WriteLine("Request served by server " + pageNo);
        }
    }

    class Proxy : IServer
    {
        int[] cachedItems = new int[2];
        int index = 0;
        IServer server;

        public Proxy()
        {
            server = new Server();
        }

        public void RequestedPage(int pageNo)
        {
            if(cachedItems.Contains(pageNo))
                Console.WriteLine("Request served by proxy " + pageNo);
            else
            {
                server.RequestedPage(pageNo);
                cachedItems[index] = pageNo;
                if (index == 0)
                    index++;
                else
                    index--;
            }
        }
    }
}
