using DeKee.Base.Entities.Custom;
using DeKee.Base.Entities.Customer;
using DeKee.Base.Entities.Globalization;
using DeKee.Dao.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DeKee.Base.Entities.Organization;

namespace CreditFront.AdminPanel
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DataSeed();            
        }

        private void DataSeed()
        {
            OrganizationUsers();
            CustomerApplications();

        }

        private void OrganizationUsers()
        {
            using (var data = new BaseDataContext())
            {
                if(data.OrganizationUsers.Any())
                {

                }
                else
                {
                    Branch root = new Branch()
                    {
                        Address = "123",
                        BranchType = new BranchType()
                        {Code = "root", Name="root" },
                        CloseDate = null,
                        Code = "RootBranch",
                        DateCreated = DateTime.Now,
                        FirstPerson = "Root",
                        IsTemporarilySuspended = false,
                        Name = "Root",
                        ParrentBranch = null,
                        Phone = "+3801111111111",
                        RegionCode = 1,
                        SecondPerson = "SecondRootPerson",
                        ShortName = "Root",
                        TaxId = "000000000"
                    };
                    Position headPos = new Position()
                    {
                        Code = "head",
                        Name = "Head"
                    };
                    var user = new OrganizationUser()
                    {
                        Login = "test@dk.com",
                        Branch = root,
                        CloseDate = null,
                        Code ="test",
                        DateCreated = DateTime.Now,
                        Email = "test@dk.com",
                        IsLocked = false,
                        IsNeedChangePassword = false,
                        Name = "Test",
                        PasswordHash = Web.Utils.SecurityHelper.GetHash("123456"),
                        Position = headPos,
                        Status = OrganizationUser.Statuses.Active
                    };
                    data.OrganizationUsers.Add(user);
                    data.SaveChanges();
                }
            }
        }

        private void CustomerApplications()
        {
            using (var data = new BaseDataContext())
            {
                if (data.CustomerApplications.Any())
                {
                    //var apps = data.CustomerApplications
                    //    .Include(c => c.CustomPropertyRegestries)
                    //    .Include(c => c.CustomPropertyRegestries.Select(cp => cp.CustomProperty))
                    //    .Include(c => c.CustomPropertyRegestries.Select(cp => cp.CustomValue))
                    //    .ToArray();
                    return;
                }
                else
                {
                    var country = new Country() { Code = "1", Name = "UU", Alpha2 = "1", Alpha3 = "3" };
                    var idDoc = new IdentityDocument() { Code = "12", Name = "12" };
                    var customer = new Customer()
                    {
                        Name = "12",
                        Code = "1",
                        Sex = DeKee.Base.Enums.Sex.Male,
                        Citizenship = country,
                        IdentityDocument = idDoc
                    };
                    var application = new CustomerApplication()
                    {
                        Customer = customer,
                        DateCreated = DateTime.Now
                    };
                    data.CustomerApplications.Add(application);
                    data.SaveChanges();

                    var customPropertyRegestry = new List<CustomProperty>();
                    foreach (var item in new[] { 1, 2, 3 })
                    {
                        customPropertyRegestry.Add(new CustomProperty()
                        {
                            DateCreated = DateTime.Now,
                            CustomType = new CustomType()
                            {
                                Code = "Salary",
                                DateCreated = DateTime.Now,
                                ModifiedDate = DateTime.Now,
                                Name = "Name " + item
                            },
                            CustomValue = new CustomValue()
                            {
                                DateCreated = DateTime.Now,
                                Value = item.ToString(),
                            }
                        });
                    }

                    application.CustomProperties = customPropertyRegestry;
                    data.SaveChanges();
                }
            }
        }
    }
}
