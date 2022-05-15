using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class PrivatePerson : Company
    {
        string name;

        public PrivatePerson(string name, int amountPayed) : base(1, amountPayed)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
