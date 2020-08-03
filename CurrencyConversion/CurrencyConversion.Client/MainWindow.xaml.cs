using CurrencyConversion.Client.ServiceReference;
using System;
using System.Windows;

namespace CurrencyConversion.Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CurrencyConversionClientVM ClientVM { get; }
        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindow_Closing;
            ClientVM = new CurrencyConversionClientVM();
            DataContext = ClientVM;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClientVM.Dispose();
        }
    }
}
