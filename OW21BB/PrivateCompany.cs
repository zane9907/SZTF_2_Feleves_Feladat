using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class PrivateCompany : Company
    {
        string name;

        public PrivateCompany(string name, int spaceNeeded, int amountPayed) : base(spaceNeeded, amountPayed)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
