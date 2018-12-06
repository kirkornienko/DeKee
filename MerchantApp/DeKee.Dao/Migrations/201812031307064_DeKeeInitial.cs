namespace DeKee.Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeKeeInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Level2",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Level1Id = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level1", t => t.Level1Id)
                .Index(t => t.Level1Id);
            
            CreateTable(
                "dbo.Level1",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Level3",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Level2Id = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level2", t => t.Level2Id)
                .Index(t => t.Level2Id);
            
            CreateTable(
                "dbo.Level4",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Level3Id = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level3", t => t.Level3Id)
                .Index(t => t.Level3Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Alpha2 = c.String(),
                        Alpha3 = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerAddresses",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PointId = c.Long(),
                        DistrictId = c.Long(),
                        CityId = c.Long(),
                        RegionId = c.Long(),
                        CustomerId = c.Long(nullable: false),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Districts", t => t.DistrictId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .ForeignKey("dbo.Points", t => t.PointId)
                .ForeignKey("dbo.Regions", t => t.RegionId)
                .Index(t => t.PointId)
                .Index(t => t.DistrictId)
                .Index(t => t.CityId)
                .Index(t => t.RegionId)
                .Index(t => t.CustomerId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.OrganizationUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        CloseDate = c.DateTime(),
                        IsLocked = c.Boolean(nullable: false),
                        IsNeedChangePassword = c.Boolean(nullable: false),
                        LastSignInDate = c.DateTime(),
                        LastChangePassword = c.DateTime(),
                        PositionId = c.Long(nullable: false),
                        BranchId = c.Long(nullable: false),
                        Login = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                        Address = c.String(),
                        RegionCode = c.Int(nullable: false),
                        TaxId = c.String(),
                        FirstPerson = c.String(),
                        SecondPerson = c.String(),
                        Phone = c.String(),
                        CloseDate = c.DateTime(),
                        IsTemporarilySuspended = c.Boolean(nullable: false),
                        ParrentId = c.Long(),
                        BranchTypeId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BranchTypes", t => t.BranchTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Branches", t => t.ParrentId)
                .Index(t => t.ParrentId)
                .Index(t => t.BranchTypeId);
            
            CreateTable(
                "dbo.BranchTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Surname = c.String(),
                        Name = c.String(),
                        Patronymic = c.String(),
                        ShortName = c.String(),
                        TaxCode = c.String(),
                        IsTaxCode = c.Boolean(nullable: false),
                        Sex = c.Int(nullable: false),
                        CitizenshipId = c.Long(nullable: false),
                        IdentityDocumentId = c.Long(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CitizenshipId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.IdentityDocuments", t => t.IdentityDocumentId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CitizenshipId)
                .Index(t => t.IdentityDocumentId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.CustomerFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FileTypeId = c.Long(nullable: false),
                        CustomerId = c.Long(nullable: false),
                        Data = c.Binary(),
                        Url = c.String(),
                        FileName = c.String(),
                        ContentType = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.FileTypes", t => t.FileTypeId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.FileTypeId)
                .Index(t => t.CustomerId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.FileTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityDocuments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.Points",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        CreateUserId = c.Long(),
                        ModifyUsedId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUsers", t => t.CreateUserId)
                .ForeignKey("dbo.OrganizationUsers", t => t.ModifyUsedId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUsedId);
            
            CreateTable(
                "dbo.CustomerApplications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CustomerId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.CustomProperties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                        CustomType_Id = c.Long(),
                        CustomValue_Id = c.Long(),
                        CustomerApplication_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomTypes", t => t.CustomType_Id)
                .ForeignKey("dbo.CustomValues", t => t.CustomValue_Id)
                .ForeignKey("dbo.CustomerApplications", t => t.CustomerApplication_Id)
                .Index(t => t.CustomType_Id)
                .Index(t => t.CustomValue_Id)
                .Index(t => t.CustomerApplication_Id);
            
            CreateTable(
                "dbo.CustomTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        AggrigateTypeId = c.Long(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomTypes", t => t.AggrigateTypeId)
                .Index(t => t.AggrigateTypeId);
            
            CreateTable(
                "dbo.CustomValues",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EntityId = c.Long(nullable: false),
                        Value = c.String(),
                        ModifiedDate = c.DateTime(),
                        DateCreated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.OrganizationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Transfers");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsPrimary = c.Boolean(nullable: false),
                        PrimaryKey = c.String(),
                        Login = c.String(),
                        Password = c.String(),
                        Confirm = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        B4UPassword = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WalletAddress = c.String(),
                        PrivateKey = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        B4UHash = c.String(),
                        GasAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BTCAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BFYAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sender = c.String(),
                        Receiver = c.String(),
                        Currency = c.String(),
                        TransactionCode = c.String(),
                        ApiId = c.String(),
                        Status = c.String(),
                        MerchantAutoSign = c.String(),
                        CustomersAutoSign = c.String(),
                        GasLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ModifiedDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.CustomProperties", "CustomerApplication_Id", "dbo.CustomerApplications");
            DropForeignKey("dbo.CustomProperties", "CustomValue_Id", "dbo.CustomValues");
            DropForeignKey("dbo.CustomProperties", "CustomType_Id", "dbo.CustomTypes");
            DropForeignKey("dbo.CustomTypes", "AggrigateTypeId", "dbo.CustomTypes");
            DropForeignKey("dbo.CustomerApplications", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerAddresses", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Regions", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerAddresses", "PointId", "dbo.Points");
            DropForeignKey("dbo.Points", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Points", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerAddresses", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerAddresses", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.Districts", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Districts", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Customers", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Customers", "IdentityDocumentId", "dbo.IdentityDocuments");
            DropForeignKey("dbo.IdentityDocuments", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.IdentityDocuments", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerFiles", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerFiles", "FileTypeId", "dbo.FileTypes");
            DropForeignKey("dbo.CustomerFiles", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerFiles", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerAddresses", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Customers", "CitizenshipId", "dbo.Countries");
            DropForeignKey("dbo.CustomerAddresses", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.CustomerAddresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "ModifyUsedId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.Cities", "CreateUserId", "dbo.OrganizationUsers");
            DropForeignKey("dbo.OrganizationUsers", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.OrganizationUsers", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Branches", "ParrentId", "dbo.Branches");
            DropForeignKey("dbo.Branches", "BranchTypeId", "dbo.BranchTypes");
            DropForeignKey("dbo.Level4", "Level3Id", "dbo.Level3");
            DropForeignKey("dbo.Level3", "Level2Id", "dbo.Level2");
            DropForeignKey("dbo.Level2", "Level1Id", "dbo.Level1");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.CustomTypes", new[] { "AggrigateTypeId" });
            DropIndex("dbo.CustomProperties", new[] { "CustomerApplication_Id" });
            DropIndex("dbo.CustomProperties", new[] { "CustomValue_Id" });
            DropIndex("dbo.CustomProperties", new[] { "CustomType_Id" });
            DropIndex("dbo.CustomerApplications", new[] { "CustomerId" });
            DropIndex("dbo.Regions", new[] { "ModifyUsedId" });
            DropIndex("dbo.Regions", new[] { "CreateUserId" });
            DropIndex("dbo.Points", new[] { "ModifyUsedId" });
            DropIndex("dbo.Points", new[] { "CreateUserId" });
            DropIndex("dbo.Districts", new[] { "ModifyUsedId" });
            DropIndex("dbo.Districts", new[] { "CreateUserId" });
            DropIndex("dbo.IdentityDocuments", new[] { "ModifyUsedId" });
            DropIndex("dbo.IdentityDocuments", new[] { "CreateUserId" });
            DropIndex("dbo.CustomerFiles", new[] { "ModifyUsedId" });
            DropIndex("dbo.CustomerFiles", new[] { "CreateUserId" });
            DropIndex("dbo.CustomerFiles", new[] { "CustomerId" });
            DropIndex("dbo.CustomerFiles", new[] { "FileTypeId" });
            DropIndex("dbo.Customers", new[] { "ModifyUsedId" });
            DropIndex("dbo.Customers", new[] { "CreateUserId" });
            DropIndex("dbo.Customers", new[] { "IdentityDocumentId" });
            DropIndex("dbo.Customers", new[] { "CitizenshipId" });
            DropIndex("dbo.Branches", new[] { "BranchTypeId" });
            DropIndex("dbo.Branches", new[] { "ParrentId" });
            DropIndex("dbo.OrganizationUsers", new[] { "BranchId" });
            DropIndex("dbo.OrganizationUsers", new[] { "PositionId" });
            DropIndex("dbo.Cities", new[] { "ModifyUsedId" });
            DropIndex("dbo.Cities", new[] { "CreateUserId" });
            DropIndex("dbo.CustomerAddresses", new[] { "ModifyUsedId" });
            DropIndex("dbo.CustomerAddresses", new[] { "CreateUserId" });
            DropIndex("dbo.CustomerAddresses", new[] { "CustomerId" });
            DropIndex("dbo.CustomerAddresses", new[] { "RegionId" });
            DropIndex("dbo.CustomerAddresses", new[] { "CityId" });
            DropIndex("dbo.CustomerAddresses", new[] { "DistrictId" });
            DropIndex("dbo.CustomerAddresses", new[] { "PointId" });
            DropIndex("dbo.Level4", new[] { "Level3Id" });
            DropIndex("dbo.Level3", new[] { "Level2Id" });
            DropIndex("dbo.Level2", new[] { "Level1Id" });
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.CustomValues");
            DropTable("dbo.CustomTypes");
            DropTable("dbo.CustomProperties");
            DropTable("dbo.CustomerApplications");
            DropTable("dbo.Regions");
            DropTable("dbo.Points");
            DropTable("dbo.Districts");
            DropTable("dbo.IdentityDocuments");
            DropTable("dbo.FileTypes");
            DropTable("dbo.CustomerFiles");
            DropTable("dbo.Customers");
            DropTable("dbo.Positions");
            DropTable("dbo.BranchTypes");
            DropTable("dbo.Branches");
            DropTable("dbo.OrganizationUsers");
            DropTable("dbo.Cities");
            DropTable("dbo.CustomerAddresses");
            DropTable("dbo.Countries");
            DropTable("dbo.Level4");
            DropTable("dbo.Level3");
            DropTable("dbo.Level1");
            DropTable("dbo.Level2");
        }
    }
}
