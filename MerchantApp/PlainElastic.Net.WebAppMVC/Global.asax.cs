using AutoMapper;
using B4U.Base.Entities.User;
using B4U.Dao.Base;
using B4U.ElasticContext.Base;
using PlainElastic.Net.WebAppMVC.Models;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PlainElastic.Net.WebAppMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mappings();
            Seeds();
        }

        private void Mappings()
        {
            Mapper.Initialize(cfg => initMappings(cfg));
            //Mapper.Initialize(cfg => cfg.CreateMap<TransferModel, B4U.Base.Entities.Transfer.Transfer>());

            //Mapper.Initialize(cfg => cfg.CreateMap<UserModel, B4U.Base.Entities.User.User>());

        }

        private static void initMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<B4U.Base.Entities.Transfer.Transfer, TransferModel>();
            cfg.CreateMap<B4U.Base.Entities.User.User, UserModel>();
            cfg.CreateMap<TransferModel, B4U.Base.Entities.Transfer.Transfer>();
            cfg.CreateMap<UserModel, B4U.Base.Entities.User.User>();
        }

        private void Seeds()
        {//TODO
            var HandyContext = new HandyContext();
            RegionSeed(HandyContext);
            BranchSeed(HandyContext);
            MerchantSeed(HandyContext);

        }

        private static void MerchantSeed(HandyContext HandyContext)
        {
            var _merchRepository = HandyContext.ElasticRepositoryHost.GetRepostory<UserModel>();
            if (_merchRepository.GetIndexData() == null)
            {
                foreach (var item in _merches)
                {
                    var id = _merchRepository.Push(item);
                    item.Id = id;
                }
            }

            using (var data = new BaseDataContext())
            {
                foreach (var item in _merches)
                {
                    if(!data.Users.Any(u => u.Login == item.Login))
                    {
                        var e = Mapper.Map<User>(item);
                        data.Users.Add(e);
                    }
                }
                data.SaveChanges();
            }
        }


        private static List<UserModel> _merches = new UserModel[] {
            new UserModel()
            {
                FirstName = "TechMerch",
                CreatedDate= DateTime.Now,
                ModifiedDate = DateTime.Now,
                Login = "tech@b4u.io",
                Password = "123456",
                B4UPassword = "T+E??D7J8VXwEhy+",
                PhoneNumber = "380731111111",
                Confirm = true,
                IsPrimary = true
            }
        }.ToList();

        private static List<Branch> _branches = new Branch[] {
            new Branch()
            {
                Address = "address 1",
                Code = "Axf-143/1d",
                Id = "1",
                Name = "Main branch",
                RegionId = "1"
            },
            new Branch()
            {
                Address = "address 1",
                Code = "Aff-144/1d",
                Id = "sqed",
                Name = "Main branch",
                RegionId = "2"
            },
            new Branch()
            {
                Address = "address 1",
                Code = "Axf-145/1d",
                Id = "afer",
                Name = "Main branch",
                RegionId = "3"
            },
            new Branch()
            {
                Address = "address 1",
                Code = "Axf-146/1d",
                Id = "wrwedfsd",
                Name = "Main branch",
                RegionId = "4"
            },

            new Branch()
            {
                Address = "address 1",
                Code = "Add-145/1d",
                Id = "afer",
                Name = "Main branch",
                RegionId = "5"
            },
            new Branch()
            {
                Address = "address 1",
                Code = "Aedaf-146/1d",
                Id = "wrwedfsd",
                Name = "Main branch",
                RegionId = "6"
            },

            new Branch()
            {
                Address = "address 2",
                Code = "Axf-143/1d",
                Id = "rtrwaewf",
                Name = "Main Kenya branch",
                RegionId = "7"
            },new Branch()
            {
                Address = "address 3",
                Code = "Axf-143/1d",
                Id = "3",
                Name = "Second Kenya branch",
                RegionId = "7"
            },new Branch()
            {
                Address = "address 4",
                Code = "Axf-143/1d",
                Id = "4",
                Name = "One more Kenya branch",
                RegionId = "7"
            },
        }.ToList();

        private static List<Region> _regions = new Region[] {
            new Region()
            {
                Code = "UK",
                Name = "United Kingdom of Great Britain", Id = "1"
            },
            new Region()
            {
                Code = "DE",
                Name = "Germany", Id = "2"
            },
            new Region()
            {
                Code = "IT",
                Name = "Italy", Id = "3"
            },
            new Region()
            {
                Code = "DU",
                Name = "Netherlands", Id = "4"
            },
            new Region()
            {
                Code = "JP",
                Name = "Japan", Id = "5"
            },new Region()
            {
                Code = "CH",
                Name = "China", Id = "6"
            },new Region()
            {
                Code = "KN",
                Name = "Kenya", Id = "7"
            }
        }.ToList();

        private static void RegionSeed(HandyContext HandyContext)
        {
            var _regionRepository = HandyContext.ElasticRepositoryHost.GetRepostory<Region>();
            if (_regionRepository.GetIndexData() == null)
            {
                foreach (var item in _regions)
                {
                    _regionRepository.Push(item);
                }
            }
        }

        private static void BranchSeed(HandyContext HandyContext)
        {
            var _branchRepository = HandyContext.ElasticRepositoryHost.GetRepostory<Branch>();
            if (_branchRepository.GetIndexData() == null)
            {
                foreach (var item in _branches)
                {
                    _branchRepository.Push(item);
                }
            }
        }

        protected void Application_End()
        {

        }
    }
}
