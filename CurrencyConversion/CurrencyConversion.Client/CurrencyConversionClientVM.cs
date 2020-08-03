using CurrencyConversion.Client.ServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CurrencyConversion.Client
{
    public class CurrencyConversionClientVM : INotifyPropertyChanged, IDisposable
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;

        private const string ConversionServiceFileName = "CurrencyConversion.Service.exe";

        private string _currentUserInput;
        private string _currentServiceResult;
        private bool _isConnectedToService;
        #endregion

        #region Properties
        public CurrencyConversionServiceClient Client { get; private set; }

        public string CurrentUserInput
        {
            get => _currentUserInput;
            set
            {
                _currentUserInput = value;
                OnPropertyChanged();
            }
        }
        public string CurrentServiceResult
        {
            get => _currentServiceResult;
            set
            {
                _currentServiceResult = value;
                OnPropertyChanged();
            }
        }
        public bool IsConnectedToService
        {
            get => _isConnectedToService;
            private set
            {
                _isConnectedToService = value;
                OnPropertyChanged();
            }
        }
        public ICommand StartNewServiceCommand
        {
            get => new RelayCommand(e =>
            {
                StartService();
            });
        }
        public ICommand ConvertCommand
        {
            get => new RelayCommand(e => Convert());
        }
        #endregion

        #region ConstructorAndDeconstructor
        public CurrencyConversionClientVM()
        {
            ConnectToService();
        }
        ~CurrencyConversionClientVM()
        {
            Dispose();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Connects to the service
        /// </summary>
        public void ConnectToService()
        {
            try
            {
                Client = new CurrencyConversionServiceClient();
                Client.Open();
                IsConnectedToService = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Connecting to the service failed.\n" + ex.Message);
                IsConnectedToService = false;
            }
        }


        /// <summary>
        /// Starts the conversion service
        /// </summary>
        private async void StartService()
        {
            try
            {
                Process.Start(ConversionServiceFileName);
                int tryCount = 5;
                while (tryCount > 0 && !IsConnectedToService)
                {
                    ConnectToService();
                    await Task.Delay(200);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if(!IsConnectedToService)
            {
                MessageBox.Show("Connecting to service failed");
            }
        }
        
        /// <summary>
        /// Converts the value current user input
        /// </summary>
        private async void Convert()
        {
           
            if(decimal.TryParse(CurrentUserInput, NumberStyles.Float, new CultureInfo("de-DE"), out decimal value))
            {
                try
                {
                    CurrentServiceResult = await Client.ConvertAsync(value);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    IsConnectedToService = false;
                }
            }
        }

        /// <summary>
        /// Invokes the property changed evend to notify the ui
        /// </summary>
        /// <param name="sender">the name of the sender property</param>
        private void OnPropertyChanged([CallerMemberName]string sender = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(sender));
        }

        /// <summary>
        /// Closes the service client
        /// </summary>
        public void Dispose()
        {
            Client?.Close();
        }

        #endregion
    }
}
