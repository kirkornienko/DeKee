using DeKee.Base.Entities.Custom;
using DeKee.ElasticContext.Base;
using CreditFront.AdminPanel.Utils;
using CreditFront.Web.Controllers;
using CreditFront.Web.Dto.Api.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditFront.AdminPanel.Controllers.Api
{
    public class SchemeApiController : BaseController
    {
        IRepostory<CustomDataScheme> _customeDataSchemeRepository;

        public SchemeApiController()
        {
            _customeDataSchemeRepository = HandyContext.ElasticRepositoryHost.GetRepostory<CustomDataScheme>();
        }

        [HttpPost]
        [AdminPanelAuthorize]
        public JsonResult GetItem(string id)
        {
            return ApiResult(new CustomDataScheme()
            {
                Code = "1",
                Id = "1",
                Name = "IdentityDocument Details",
                DateCreated = DateTime.Now,
                Properties = (new[]{
                        new CustomDataProperty(){
                            Code ="title",
                            Name = "Title",
                            MaxLength = 100,
                            MinLength = 0,
                            Required = false,
                            Type = typeof(string).Name
                        },
                        new CustomDataProperty(){
                            Code ="body",
                            Name = "Body",
                            MaxLength = 100,
                            MinLength = 0,
                            Required = false,
                            Type = typeof(string).Name
                        }

                    }).ToList()
            });
        }
        [HttpPost]
        [AdminPanelAuthorize]
        public JsonResult GetItems(GetItemsInput input)
        {   
            return ApiResult(new[] { new CustomDataScheme()
                {
                    Code = "1",
                    Id = "1",
                    Name = "IdentityDocument",
                    DateCreated = DateTime.Now,
                    Properties = (new[]{
                        new CustomDataProperty(){
                            Code ="title",
                            Name = "Title",
                            MaxLength = 100,
                            MinLength = 0,
                            Required = false,
                            Type = typeof(string).Name
                        },
                        new CustomDataProperty(){
                            Code ="body",
                            Name = "Body",
                            MaxLength = 100,
                            MinLength = 0,
                            Required = false,
                            Type = typeof(string).Name
                        }

                    }).ToList()
                }
            });
        }
    }
}