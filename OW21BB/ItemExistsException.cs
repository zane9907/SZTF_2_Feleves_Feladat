using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    class ItemExistsException : Exception
    {
        public ItemExistsException(string item) : base($"This item already exists: {item}")
        {

        }
    }
}
