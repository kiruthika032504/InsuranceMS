using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceMS.exception
{
    using System;

    public class PolicyNotFoundException : Exception
    {
        // Constructor with a message
        public PolicyNotFoundException(string message) : base(message)
        {
        }

        // Constructor with a message and an inner exception
        public PolicyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

}