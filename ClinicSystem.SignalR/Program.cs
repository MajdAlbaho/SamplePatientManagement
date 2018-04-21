using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicSystem.SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://localhost:8080/";
            using (WebApp.Start<StartUp>(url))
            {
                Console.WriteLine($"Server running at {url}");
                Console.ReadLine();
            }
        }

        public class StartUp
        {
            public void Configuration(IAppBuilder app)
            {
                app.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration();
                hubConfiguration.EnableDetailedErrors = true;
                app.MapSignalR(hubConfiguration);
            }
        }
    }
}
