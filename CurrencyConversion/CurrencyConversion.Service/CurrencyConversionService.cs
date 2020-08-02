using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Service
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        CurrencyConversion conversion = new CurrencyConversion();
        public string Convert(decimal input)
        {
            try
            {
                return conversion.Convert(input);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
