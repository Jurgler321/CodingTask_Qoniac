﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Service
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        public string Convert(string input)
        {
            return input + input;
        }
    }
}