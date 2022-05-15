using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    class ItemNotFound : Exception
    {
        public ItemNotFound(string item) : base($"{item} item is not found!")
        {

        }

    }
}
