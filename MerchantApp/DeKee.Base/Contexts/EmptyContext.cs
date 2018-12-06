using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Contexts
{
    public class EmptyContext : IContext
    {
        public IContext Bottom { get { return this; } }

        public void Init()
        {
            return;
        }        
    }
}
