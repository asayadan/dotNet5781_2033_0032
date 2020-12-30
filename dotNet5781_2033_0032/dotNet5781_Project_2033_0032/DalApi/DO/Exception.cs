using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadUsernameOrPasswordException:Exception
    {
        public BadUsernameOrPasswordException() : base() { }
        public BadUsernameOrPasswordException(string message) : base(message) { }
        public BadUsernameOrPasswordException(string message, Exception inner) : base(message, inner) { }
    }
}
