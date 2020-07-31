using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace CurrencyConversion.Service
{
    public class Server
    {
        public static void Main(string[] args)
        {
            CurrencyConversion conversion = new CurrencyConversion();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "x")
                    break;
                if(decimal.TryParse(input, out decimal result))
                    Console.WriteLine(conversion.Convert(result));
            }

            //Uri BaseAddress = new Uri("http://localhost:8000/ServiceConsole/Service");

            //ServiceHost SelfHost = new ServiceHost(typeof(CurrencyConversionService), BaseAddress);
            //try
            //{
            //    SelfHost.AddServiceEndpoint(typeof(ICurrencyConversionService), new WSHttpBinding(), "CalculatorService");

            //    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            //    smb.HttpGetEnabled = true;
            //    SelfHost.Description.Behaviors.Add(smb);

            //    SelfHost.Open();
            //    Console.WriteLine("The service is ready.");
            //    Console.WriteLine("Press <ENTER> to terminate service.");
            //    Console.WriteLine();
            //    Console.ReadLine();

            //    // Close the ServiceHostBase to shutdown the service.
            //    SelfHost.Close();
            //}
            //catch (CommunicationException ce)
            //{
            //    Console.WriteLine("An exception occured: {0}", ce.Message);
            //    Console.WriteLine("Press <ENTER> to terminate service.");
            //    Console.WriteLine();
            //    Console.ReadLine();
            //    SelfHost.Abort();
            //}
        }
    }
}
