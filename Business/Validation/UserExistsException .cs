using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    [Serializable]
    public class UserExistsException : Exception
    {
        public UserExistsException()
        {
        }

        public UserExistsException(string message) : base(message)
        {
        }

        public UserExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
