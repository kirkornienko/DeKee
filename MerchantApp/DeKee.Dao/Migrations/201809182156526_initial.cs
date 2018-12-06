namespace DeKee.Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            Down();
            return;
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Transfers");
        }
    }
}
