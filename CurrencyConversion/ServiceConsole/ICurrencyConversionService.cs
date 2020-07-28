using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Service
{
    [ServiceContract(Namespace = "CurrencyConversion.Service")]
    public interface ICurrencyConversionService
    {
        [OperationContract]
        string Convert(string input);
    }
}
