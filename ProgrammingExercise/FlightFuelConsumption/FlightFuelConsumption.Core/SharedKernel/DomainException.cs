using System;
using System.Collections.Generic;
using System.Text;

namespace FlightFuelConsumption.Core.SharedKernel
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }
    }
}
