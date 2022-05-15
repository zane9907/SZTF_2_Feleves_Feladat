using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    class NoTypeException : Exception
    {
        public NoTypeException(string type) : base($"There's no type of company, such as: {type}")
        {

        }
    }
}
