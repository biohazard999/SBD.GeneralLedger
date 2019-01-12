namespace SBD.GL.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "CreditAccount_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "DebitAccount_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "CreditAccount_Id" });
            DropIndex("dbo.Transactions", new[] { "DebitAccount_Id" });
            DropColumn("dbo.Transactions", "CreditAccount_Id");
            DropColumn("dbo.Transactions", "DebitAccount_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "DebitAccount_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "CreditAccount_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Transactions", "DebitAccount_Id");
            CreateIndex("dbo.Transactions", "CreditAccount_Id");
            AddForeignKey("dbo.Transactions", "DebitAccount_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Transactions", "CreditAccount_Id", "dbo.Accounts", "Id");
        }
    }
}
