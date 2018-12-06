using DeKee.Base.Entities.Address;
using DeKee.Base.Entities.Customer;
using DeKee.Base.Entities.DataStorage;
using DeKee.Base.Entities.Globalization;
using DeKee.Base.Entities.Organization;
using DeKee.Base.Entities.Transfer;
using DeKee.Base.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Dao.Base
{
    public class BaseDataContext : DbContext
    {
        public BaseDataContext() : base("Default")
        {
        }

        //public DbSet<Transfer> Transfers { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<UserFile> UserFiles { get; set; }
        //public DbSet<Branch> Branches { get; set; }
        //public DbSet<Division> Divisions { get; set; }

        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerApplication> CustomerApplications { get; set; }
        public DbSet<CustomerFile> CustomerFiles { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Level1> Regions { get; set; }
        public DbSet<Level2> Cities { get; set; }
        public DbSet<Level3> Districts { get; set; }
        public DbSet<Level4> Points { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set;}

        public DbSet<FileType> FileTypes{ get; set; }
    }
}
