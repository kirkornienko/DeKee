using DeKee.Base.Entities;
using PlainElastic.Net;
using PlainElastic.Net.Serialization;
using PlainSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.ElasticContext.Base
{
    public abstract class BaseElasticRepository<TData> 
        where TData : class, new()
    {
        protected ElasticClient<TData> _elasticClient;//TODO make private
        protected string _index;

        internal event Func<object[], bool> PreAction = null;
        internal event Func<object[], bool> PostAction = null;
        internal event Func<Exception, bool> Error = null;

        object[] bag;

        protected BaseElasticRepository()
        {
            _elasticClient = new ElasticClient<TData>("localhost", 9200);
            _index = typeof(TData).Name;
        }

        public TResult Perform<TResult>(Func<ElasticClient<TData>, TResult> action, params object[] additionalData)
        {
            try
            {
                if (PreAction != null) PreAction(additionalData);
                //bag = null;
                var result = action(_elasticClient);
                if (PostAction != null) PostAction(new object[] { _elasticClient.LastCommand , result });
                return result;
            }
            catch(Exception ex)
            {
                if (Error != null) Error(ex);
                throw ex;
            }
        }
    }

    public class HandyElasticRepository<TData> : BaseElasticRepository<TData>, IRepostory<TData>
        where TData : class, IRepositoryEntity<string>, new()
    {
        
        public HandyElasticRepository()
        {
            
        }

        public string SessionId { get; set; }

        public TResult CustomAction<TResult>(Func<ElasticClient<TData>, TResult> action, string actionName = null)
        {
            return this.Perform(action, actionName);
        }

        public void Delete(string id)
        {
            DeleteCommand command = new DeleteCommand(_index, "_doc", id);
            var result = Perform(ec => ec.Delete(command), "delete", id);            
        }

        public object GetIndexData()
        {
            try
            {
                var result = Perform(ec => ec.IndexExists(new IndexExistsCommand(_index)));
                return result ? (object)result : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TData GetInstance(string id)
        {
            var result = Perform(ec => ec.Get(new GetCommand(_index, "_doc", id)), "get", id);
            return result._source;
        }

        public IEnumerable<TData> GetList()
        {
            var command = new SearchCommand(_index, "_doc");
            PlainElastic.Net.Queries.QueryBuilder<TData> query = new PlainElastic.Net.Queries.QueryBuilder<TData>();

            query.Query(q => q.MatchAll());

            var result = Perform(ec => ec.Search(command, query), "getList");
            return result.Documents;
        }

        public string Push(TData instance)
        {
            var result = Perform(ec => ec.Index(new IndexCommand(_index, "_doc", instance.Id), instance), "push", instance);
            return result._id;
        }

        public IEnumerable<TData> Search(string searchQuery)
        {
            var command = new SearchCommand(_index, "_doc");
            PlainElastic.Net.Queries.QueryBuilder<TData> query = new PlainElastic.Net.Queries.QueryBuilder<TData>();

            query
                .Size(10000)
                .Query(q => 
                q.QueryString(qs => 
                    qs.Query(searchQuery)
                )
            );

           var result = Perform(ec => ec.Search(command, query), "getList");
            return result.Documents;
        }

        public void Update(TData instance, string id)
        {
            var result = Perform(ec => ec.Index(new IndexCommand(_index, "_doc", id), instance), "update", id, instance);
        }
    }
    
}
