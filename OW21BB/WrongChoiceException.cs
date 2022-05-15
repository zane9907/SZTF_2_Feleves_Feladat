using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    class WrongChoiceException : Exception
    {
        public WrongChoiceException() : base("Please choose from the available options!")
        {

        }
    }
}
