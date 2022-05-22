namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMatriculaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculaciones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                        Fecha = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupos", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculaciones", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Matriculaciones", "GrupoId", "dbo.Grupos");
            DropForeignKey("dbo.Matriculaciones", "CursoId", "dbo.Cursos");
            DropIndex("dbo.Matriculaciones", new[] { "GrupoId" });
            DropIndex("dbo.Matriculaciones", new[] { "CursoId" });
            DropIndex("dbo.Matriculaciones", new[] { "UserId" });
            DropTable("dbo.Matriculaciones");
        }
    }
}
