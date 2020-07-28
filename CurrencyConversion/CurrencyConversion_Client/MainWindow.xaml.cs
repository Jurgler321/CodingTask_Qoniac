using CurrencyConversion_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConversion_Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost selfHost;
        public MainWindow()
        {
            InitializeComponent();
            //Closed += MainWindow_Closed;


            //Uri baseAddress = new Uri("http://localhost:52944");
            //selfHost = new ServiceHost(typeof(CurrencyConversion_Service.Service1), baseAddress);

            //try
            //{
            //    // Step 5: Start the service.
            //    selfHost.Open();
            //    Console.WriteLine("The service is ready.");
            //}
            //catch (CommunicationException ce)
            //{
            //    Console.WriteLine("An exception occurred: {0}", ce.Message);
            //    selfHost.Abort();
            //}
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            // Close the ServiceHost to stop the service.
            Console.WriteLine("Press <Enter> to terminate the service.");
            Console.WriteLine();
            Console.ReadLine();
            selfHost.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Step 1: Create an endpoint address and an instance of the WCF client
            CalculatorClient client = new CalculatorClient();

            // Step 2: Call the service operations.
            // Call Add
            double value1 = 100.00D;
            double value2 = 15.99D;
            double result = client.Add(value1, value2);
            MessageBox.Show(string.Format("Add({0},{1}) = {2}", value1, value2, result));

            // Call Subtract
            value1 = 145.00D;
            value2 = 76.54D;
            result = client.Subtract(value1, value2);
            MessageBox.Show(string.Format("Subtract({0},{1}) = {2}", value1, value2, result));

            // Call Multiply
            value1 = 9.00D;
            value2 = 81.25D;
            result = client.Multiply(value1, value2);
            MessageBox.Show(string.Format("Multiply({0},{1}) = {2}", value1, value2, result));

            // Call Divide
            value1 = 22.00D;
            value2 = 7.00D;
            result = client.Divide(value1, value2);
            MessageBox.Show(string.Format("Divide({0},{1}) = {2}", value1, value2, result));

            // Step 3: Close the client gracefully.
            // This closes the connection and cleans up resources.
            client.Close();

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to terminate client.");
            Console.ReadLine();
        }
    }
}
