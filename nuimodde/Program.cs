using NDde.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace nuimodde
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Raw command-line: \n\t" + Environment.CommandLine);
            if (args.Length == 1)
            {
                Uri uri = new Uri(args[0]);
                var foo = HttpUtility.ParseQueryString(uri.Query);
                string server = foo.Get("server");
                string topic = foo.Get("topic");
                string request = foo.Get("request");

                if (!String.IsNullOrEmpty(server) && !String.IsNullOrEmpty(topic) && !String.IsNullOrEmpty(request)) {

                    using (var client = new DdeClient(server, topic))
                    {
                        client.Connect();
                        client.Request(request, 5);
                        client.Disconnect();
                    }
                }
                else
                {
                    Console.WriteLine("Paramter which was expected: nuimodde:?server=...&topic=...&request=...");
                }

            }
            else
            {
                Console.WriteLine("wrong parameter count");
            }
        }
    }
}
