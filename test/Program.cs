using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


using TinyFox;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            TinyFoxService.Port = 8080;

            var startup = new Startup();

            TinyFoxService.Start(startup.OwinMain);

            while (true) {
                Thread.Sleep(1000);
            }
        }
    }
}
