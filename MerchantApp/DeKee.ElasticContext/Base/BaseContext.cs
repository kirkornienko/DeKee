using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeKee.Base.Contexts;

namespace DeKee.ElasticContext.Base
{
    public abstract class BaseContext : IElasticContext
    {
        private ElasticRepositoryHost _elasticRepositoryHost;

        public IContext Bottom => new EmptyContext();

        public ElasticRepositoryHost ElasticRepositoryHost => _elasticRepositoryHost;

        public void Init()
        {
            //throw new NotImplementedException();
            this._elasticRepositoryHost = new ElasticRepositoryHost();
        }


    }

    public class HandyContext : BaseContext
    {
        public HandyContext()
        {
            Init();
        }
    }
}
