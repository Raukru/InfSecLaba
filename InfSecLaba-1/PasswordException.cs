using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfSecLaba_1
{
    class PasswordException : Exception
    {
        public PasswordException(string message) : base(message)
        {

        }
    }
}
