using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    class EmptyArrayOrListException : Exception
    {
        public EmptyArrayOrListException() : base("This list/array is empty!")
        {

        }
    }
}
