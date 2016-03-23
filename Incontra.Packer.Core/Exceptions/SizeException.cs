using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Incontra.Packer.Core.Exceptions
{
    public class SizeException: System.Exception
    {
            public SizeException(string msg): base(msg)
            {
                
            }
    }
}
