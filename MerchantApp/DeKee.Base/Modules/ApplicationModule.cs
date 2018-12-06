using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Modules
{
    public abstract class ApplicationModule<TContext> where TContext: Base.Contexts.IContext, new()
    {
        protected ApplicationModule()
        {
            Context = new TContext();
        }
        TContext Context { get; set; }
        public abstract void InitModule();
    }
}
