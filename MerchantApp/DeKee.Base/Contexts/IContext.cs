using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Contexts
{
    public interface IContext
    {
        void Init();
        IContext Bottom { get; }

    }
}
