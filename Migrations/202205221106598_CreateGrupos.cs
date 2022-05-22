namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGrupos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Tipo = c.Int(nullable: false),
                        Creditos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Grupos");
        }
    }
}
