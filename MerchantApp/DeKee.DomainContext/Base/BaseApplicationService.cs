using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Base
{
    public abstract class BaseApplicationService : IApplicatonService
    {
        protected string getSessionId()
        {
            if(SessionIdFunc != null)
            {
                return SessionIdFunc();
            }
            return null;
        }
        public Func<string> SessionIdFunc { get; set; }
        public abstract void Init();
    }
}
