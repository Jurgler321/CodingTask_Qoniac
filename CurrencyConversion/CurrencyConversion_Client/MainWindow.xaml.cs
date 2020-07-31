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
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Step 1: Create an endpoint address and an instance of the WCF client
            CurrencyConversionServiceClient client = new CurrencyConversionServiceClient();
            if(double.TryParse(TB_Input.Text, out double value))
            {
                string result = client.Convert(value);
                MessageBox.Show(result);
                client.Close();
            }
        }
    }
}
