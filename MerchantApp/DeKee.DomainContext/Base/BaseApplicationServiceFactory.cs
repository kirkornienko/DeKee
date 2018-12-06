using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Base
{
    public abstract class BaseApplicationServiceFactory
    {
        public string SessionId { get; set; }

        public TService GetService<TService>()
            where TService : BaseApplicationService, IApplicatonService, new()
        {
            var result = new TService();
            result.Init();
            result.SessionIdFunc = () => this.SessionId;
            return result;
        }
    }

    public class HandyApplicationServiceFactory : BaseApplicationServiceFactory
    {

    }
}
