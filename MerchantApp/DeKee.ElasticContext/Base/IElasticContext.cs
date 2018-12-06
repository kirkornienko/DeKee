using DeKee.Base.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.ElasticContext.Base
{
    public interface IElasticContext: IContext
    {
        ElasticRepositoryHost ElasticRepositoryHost { get; }
    }
}
