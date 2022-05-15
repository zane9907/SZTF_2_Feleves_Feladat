using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class Company : ICustomer
    {
        public int SpaceNeeded { get; set; }
        public int AmountPayed { get; set; }

        public Company(int spaceNeeded, int amountPayed)
        {
            SpaceNeeded = spaceNeeded;
            AmountPayed = amountPayed;
        }
    }
}
