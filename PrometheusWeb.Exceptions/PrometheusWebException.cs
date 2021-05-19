using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusWeb.Exceptions
{
    public class PrometheusWebException : ApplicationException
    {
        public PrometheusWebException() : base()
        {

        }

        public PrometheusWebException(string message) : base(message)
        {

        }

        public PrometheusWebException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
