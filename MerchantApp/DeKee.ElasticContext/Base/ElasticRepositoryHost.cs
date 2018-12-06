using DeKee.Base.Entities;
using DeKee.Base.Entities.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.ElasticContext.Base
{
    public class ElasticRepositoryHost
    {
        public static class SettingKeys
        {
            public static readonly string ElasticHost = "ElasticHost";
            public static readonly string ElasticPort = "ElasticPort";
        }
        public ElasticRepositoryHost()
        {
            settings = new Dictionary<string, string>();
        }
        protected Dictionary<string, string> settings = null;

        public string SessionId { get; set; }

        public void ApplySetting(string key, string value)
        {
            settings.Add(key, value);
        }
        public IRepostory<TData> GetRepostory<TData>()
            where TData : class, IRepositoryEntity<string>, new()
        {
            HandyElasticRepository<TData> handyElasticRepository = new HandyElasticRepository<TData>();
            handyElasticRepository.PreAction += HandyElasticRepository_PreAction;
            handyElasticRepository.PostAction += HandyElasticRepository_PostAction;
            handyElasticRepository.Error += HandyElasticRepository_Error;
            handyElasticRepository.SessionId = this.SessionId;
            return handyElasticRepository;
        }
        internal event Func<Exception, bool> LogError = null;

        private bool HandyElasticRepository_Error(Exception arg)
        {
            if (LogError != null)
            {
                LogError(arg);
            }
                //throw new NotImplementedException();
                return true;
        }

        public event Func<object, bool> LogRequest = null;

        private bool HandyElasticRepository_PostAction(object[] arg)
        {
            if (LogRequest != null)
            {
                foreach (var e in arg)
                {
                    LogRequest(e);
                }
            }
            //throw new NotImplementedException();
            return true;
        }

        private bool HandyElasticRepository_PreAction(object[] arg)
        {
            return true;
            //throw new NotImplementedException();
        }
    }
}
