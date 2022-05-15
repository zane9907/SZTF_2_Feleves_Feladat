using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    interface ICustomer
    {
        int SpaceNeeded { get; set; }

        int AmountPayed { get; set; }
    }
}
