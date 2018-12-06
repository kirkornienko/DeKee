using DeKee.Base.Entities;
using PlainSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.ElasticContext.Base
{
    public interface IRepostory<TData> 
        where TData : IRepositoryEntity<string>
    {
        TData GetInstance(string id);
        IEnumerable<TData> GetList();
        string Push(TData instance);
        void Delete(string id);
        void Update(TData instance, string id);
        TResult CustomAction<TResult>(Func<ElasticClient<TData>, TResult> action, string actionName = null);
        IEnumerable<TData> Search(string searchQuery);
        object GetIndexData();
    }
}
