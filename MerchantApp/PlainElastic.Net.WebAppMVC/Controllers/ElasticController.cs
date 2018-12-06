using PlainElastic.Net.Base.Entities;
using PlainElastic.Net.IndexSettings;
using PlainElastic.Net.Queries;
using PlainElastic.Net.WebAppMVC.Models;
using PlainSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Controllers
{
    public class ElasticController : BaseController
    {

        private ElasticClient<UserModel> _elasticClient;


        public ElasticController()
        {
            _elasticClient = new ElasticClient<UserModel>("localhost", 9200);
        }
        // GET: Elastic
        public ActionResult Index()
        {
            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        [HttpPost]
        public ActionResult InitElasticIndex(string name, int shards, int replicas)
        {
            var settings = new IndexSettingsBuilder()
                    .MaxResultWindow(int.MaxValue)
                    .NumberOfReplicas(replicas)
                    .NumberOfShards(shards);

            var result = _elasticClient.CreateIndex(
                new IndexCommand(name),//.WithParameter("max_result_window",(int.MaxValue-200).ToString())
                settings
            );
            //result.
            return Redirect("/");
        }

        [HttpPost]
        public ActionResult SearchFunctionalScoreResult(SearchValues SearchValues, string indexName = null)
        {
            var searchQuery = new QueryBuilder<UserModel>().
                Size(200).
                Query(q => q.
                     Bool(b => b.
                        Must(m =>
                        {
                                    
                            var query = m.MatchAll();

                            return query;
                        })               
                    )
                );

            return DocumentsResult(searchQuery);
        }

        [HttpPost]
        public ActionResult SearchDocuments(SearchValues SearchValues, string indexName = null)
        {

            return SearchFunctionalScoreResult(SearchValues, indexName);

            //var searchQuery = new QueryBuilder<SearchTenderElasticDocument>()
            //        .Size(200)
            //        //.Sort(s => s.Field(st => st.tender.DateModified).Custom("\"_score\""))
            //        .Query(q => q
            //            .Bool(b => b
            //               .Must(m =>
            //               {
            //                   Query<SearchTenderElasticDocument> query = null;

            //                   if (!string.IsNullOrEmpty(SearchValues.TenderId))
            //                   {
            //                       query = (query ?? m).Prefix(f => f.Custom("\"{0}\" : \"{1}\"", "tender.TenderId", SearchValues.TenderId));// .Field(st => st.tender.TenderId).Custom($"\"{SearchValues.TenderId}\""));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.StatusId))
            //                   {
            //                       var statuses = SearchValues.StatusId.Split(',');
            //                       query = (query ?? m).Terms(t => t.Field(st => st.tender.StatusId).Values(statuses));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.ProcedureType))
            //                   {
            //                       query = (query ?? m).Match(f => f.Field(st => st.tender.ProcedureModeId).Query(SearchValues.ProcedureType));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.RegionId))
            //                   {
            //                       //var regions = SearchValues.RegionId.Split(',');
            //                       query = (query ?? m).Match(f => f.Field(st => st.tender.Region.Title).Query(SearchValues.RegionId));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.TenderValueAmountFrom) && !string.IsNullOrEmpty(SearchValues.TenderValueAmountTill))
            //                   {
            //                       query = (query ?? m).Range(rng => rng.
            //                           Field(st => st.tender.Value.Amount).
            //                           Gte(SearchValues.TenderValueAmountFrom).
            //                           Lte(SearchValues.TenderValueAmountTill)
            //                        );
            //                       //(f => f.Field(st => st.tender.Value.Amount).Query(SearchValues.TenderValueAmountFrom));
            //                   }
            //                   if (SearchValues.PeriodEndFrom != default(DateTime) && SearchValues.PeriodEndTill != default(DateTime))
            //                   {
            //                       query = (query ?? m).Range(rng => rng.
            //                           Field(st => st.tender.Period.EndDate).
            //                           Gte(SearchValues.PeriodEndFrom.ToString("o")).
            //                           Lte(SearchValues.PeriodEndTill.ToString("o"))
            //                        );
            //                       //(f => f.Field(st => st.tender.Value.Amount).Query(SearchValues.TenderValueAmountFrom));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.TaxId))
            //                   {
            //                       query = (query ?? m).Match(f => f.Field(st => st.tender.Organization.TaxId).Query(SearchValues.TaxId));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.Title))
            //                   {
            //                       query = (query ?? m).Match(f => f.Field(st => st.tender.Title).Query(SearchValues.Title));
            //                   }
            //                   if (!string.IsNullOrEmpty(SearchValues.Description))
            //                   {
            //                       query = (query ?? m).Match(f => f.Field(st => st.tender.Description).Query(SearchValues.Description));
            //                   }

            //                   query = query ?? m.MatchAll();

            //                   return query;
            //               }
            //            )));

            //return DocumentsResult(searchQuery, indexName);            
        }

        private ActionResult DocumentsResult(QueryBuilder<UserModel> queryBuilder, string indexName = null)
        {
            var searchResult = _elasticClient.Search(
                new SearchCommand(indexName ?? "usermodel"),
                queryBuilder);
            
            return View(searchResult.Documents.Any()
                ? searchResult.Documents
                : new UserModel[] { });
        }
        [HttpGet]
        public ActionResult SearchDocuments(string searchValue = null, string indexName = "usermodel")
        {
            var searchResult = _elasticClient.Search(
                new SearchCommand(indexName),
                new QueryBuilder<UserModel>()
                    .Size(100)
                    .Query(q => q
                        .MatchAll()
                        //.Bool(b => b
                        //   .Must(m => m
                        //        .Fuzzy(f => f.Field(art => art.Body).Value(searchValue))))
                               //.Match(mt => mt.Field(art => art.Body).Query(searchValue))))
                        //.Term(t => t
                        //    .Field(tweet => tweet.Body)
                        //    .Value(searchValue)
                        //    .Boost(2)
                        )
                    );

            return View(searchResult.Documents.Any()
                ? searchResult.Documents                    
                : new UserModel[] { });
        }

        [HttpGet]
        public ActionResult AddDocument(string indexName = "articles")
        {
            var salt = Guid.NewGuid().ToString();
            return View(new ArticleModel()
            {
                Body = $"{salt}\n{salt}\n{salt}\n{salt}",
                CreatedDate = DateTime.Now,
                Id = salt,
                Status = 1,
                Title = salt,
                Index = indexName
            });
        }

        [HttpPost]
        public ActionResult AddDocument(ArticleModel model)
        {
            _elasticClient.Index(
                new IndexCommand(model.Index, "article", model.Id).Refresh(),
                new ArticleElasticDocument()
                {
                    Body = model.Body,
                    CreatedDate = model.CreatedDate,
                    Id = model.Id,
                    Status = model.Status,
                    Title = model.Title
                });
            return Redirect($"AddDocument?indexName={model.Index}");
        }
    }
}