using DeKee.Base.Entities.Audit;
using DeKee.DomainContext.Base;
using DeKee.ElasticContext.Base;
using PlainElastic.Net.Serialization;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services
{
    public class BaseAPI : BaseApplicationService
    {
        private const string tranferToUrl = "https://airtime.transferto.com/cgi-bin/shop/topup";
        HandyElasticRepository<Event> repository = new HandyElasticRepository<Event>();

        //protected HttpClient _httpClient;
        //protected string Get(string uri)
        //{
        //    return "";
        //}
        protected T GETJsonDto<T>(string uri, Func<string> getData)
        {
            var data = getData();
            var url = getBaseUrlFromConfig();
            System.Diagnostics.Debug.WriteLine("URL: " + url);
            System.Diagnostics.Debug.WriteLine("Method: Get " + uri);
            System.Diagnostics.Debug.WriteLine("Request: " + data);
            var sessionId = getSessionId() ?? Guid.NewGuid().ToString();
            repository.Push(new Event() { DateCreated = DateTime.Now, Message = $"URL: {url}  {data}", Type = "Request", Level = "info", Module = GetType().Name, SessonId = sessionId });

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback += (q, w, e, r) => true;

            string requestUriString = string.Format("{0}/{1}", url, uri);

            if(!string.IsNullOrWhiteSpace(data))
            {
                requestUriString += "?" + data;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUriString);
            req.Method = "GET";
            req.ContentType = "text/plain";
            req.Timeout = 120000;

            try
            {
                var res = req.GetResponse();

                using (var stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        var dto = InitJsonSerializer().Deserialize<T>(reader.ReadToEnd());
                        repository.Push(new Event() { DateCreated = DateTime.Now, Message = "", Item = dto, Type = "Response", Level = "info", Module = GetType().Name, SessonId = sessionId });

                        return dto;
                    }
                }
            }
            catch (Exception e)
            {
                repository.Push(new Event() { DateCreated = DateTime.Now, Message = e.Message, Item = e, Type = "Response", Level = "info", Module = GetType().Name, SessonId = sessionId });
                throw;
                //return default(OutType);
            }
        }

        

        public string GetData<TData>(TData data)
            where TData : class, new()
        {
            return InitJsonSerializer().Serialize(data);
        }

        protected OutType POSTJsonDto<OutType>(string uri, Func<string> getData)
        {
            var data = getData();
            var url = getBaseUrlFromConfig();
            System.Diagnostics.Debug.WriteLine("URL: " + url);
            System.Diagnostics.Debug.WriteLine("Method: POST " + uri);
            System.Diagnostics.Debug.WriteLine("Request: " + data);
            var sessionId = getSessionId() ?? Guid.NewGuid().ToString();
            repository.Push(new Event() { DateCreated = DateTime.Now, Message = $"URL: {url}\n{data}", Type="Request", Level = "info", Module = GetType().Name, SessonId = sessionId });

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback += (q, w, e, r) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", url, uri));
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Timeout = 120000;

            using (var stream = req.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }

            try
            {
                var res = req.GetResponse();

                using (var stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string value = reader.ReadToEnd();
                        repository.Push(new Event() { DateCreated = DateTime.Now, Message = value, Type="Response", Level = "info", Module = GetType().Name, SessonId = sessionId });
                        var dto = InitJsonSerializer().Deserialize<OutType>(value);
                        return dto;
                    }
                }
            }
            catch (Exception)
            {
                throw;
                //return default(OutType);
            }
        }
        protected OutType POSTXmlDto<OutType>(string uri, string data)
            where OutType: class, new()
        {
            var url = getBaseUrl();
            System.Diagnostics.Debug.WriteLine("URL: " + url);
            System.Diagnostics.Debug.WriteLine("Method: " + uri);
            System.Diagnostics.Debug.WriteLine("Request: " + data);
            var sessionId = getSessionId() ?? Guid.NewGuid().ToString();
            repository.Push(new Event() { DateCreated = DateTime.Now, Message = $"URL: {url}\n{data}", Type = "Request", Level = "info", Module = GetType().Name, SessonId = sessionId });

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback += (q, w, e, r) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", url, uri));
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.Timeout = 120000;

            using (var stream = req.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }

            try
            {
                var res = req.GetResponse();

                using (var stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        
                        var dto = InitXmlSerializer<OutType>().Deserialize(reader) as OutType;

                        repository.Push(new Event() { DateCreated = DateTime.Now, Message = "", Item = dto, Type = "Response", Level = "info", Module = GetType().Name, SessonId = sessionId });

                        return dto;
                    }
                }
            }
            catch (Exception e)
            {
                repository.Push(new Event() { DateCreated = DateTime.Now, Message = e.Message, Item = e, Type = "Response", Level = "info", Module = GetType().Name, SessonId = sessionId });
                throw;
                //return default(OutType);
            }
        }
        protected static IJsonSerializer InitJsonSerializer()
        {
            return new JsonNetSerializer();
        }
        private static System.Xml.Serialization.XmlSerializer InitXmlSerializer<Type>()
        {
            return new System.Xml.Serialization.XmlSerializer(typeof(Type));
        }
        protected string POST(string uri, string data)
        {
            var url = getBaseUrlFromConfig();
            System.Diagnostics.Debug.WriteLine("URL: " + url);
            System.Diagnostics.Debug.WriteLine("Method: " + uri);
            System.Diagnostics.Debug.WriteLine("Request: " + data);

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback += (q, w, e, r) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", url, uri));
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.Timeout = 120000;

            using (var stream = req.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(data);
                }
            }

            try
            {
                var res = req.GetResponse();

                using (var stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                return "Technical error;";
            }
        }

        protected virtual string getBaseUrlFromConfig()
        {
            return ConfigurationManager.AppSettings[GetType().Name + ".Url"] ?? getBaseUrl();
        }

        protected virtual string getBaseUrl()
        {
            return tranferToUrl;
        }

        protected string GET(string uri)
        {
            var url = tranferToUrl;
            Console.WriteLine("URL: " + url);
            Console.WriteLine("Request: " + uri);

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback += (q, w, e, r) => true;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(string.Format("{0}/{1}", url, uri));
            req.Method = "GET";
            req.Timeout = 120000;

            try
            {
                var res = req.GetResponse();

                using (var stream = res.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch(Exception)
            {
                return "";
            }
        }

        public override void Init()
        {
            
        }
    }
}