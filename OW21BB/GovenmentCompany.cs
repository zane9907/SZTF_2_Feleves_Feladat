using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class GovernmentCompany : Company
    {
        string name;

        public GovernmentCompany(string name, int spaceNeeded, int amountPayed) : base(spaceNeeded, amountPayed)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
