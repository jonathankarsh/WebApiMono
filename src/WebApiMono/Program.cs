using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace WebApiMono {
    class Program {
        static void Main(string[] args) {
            const string listenAddress = "http://*:9000/";
            const string sendAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(listenAddress)) {
                Console.WriteLine("Server started listening at {0}", listenAddress);

                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();

                var response = client.GetAsync(sendAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.ReadLine(); 
            }
        }
    }
}
