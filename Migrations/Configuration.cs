namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AppGestionEMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppGestionEMS.Models.ApplicationDbContext context)
        {
            string roleAdmin = "admin";
            string roleProfe = "profesor";
            string roleAlum = "alumno";
            // Adaptar a los requisitos del cliente
            AddRole(context, roleAdmin);

            AddRole(context, roleProfe);
            AddRole(context, roleAlum);
            // Es obligatorio mantener el email que se proporciona en este código y que se
            // asigna en el método AddUser a la propiedad UserName para que los profesores
            // puedan probar la práctica.
            AddUser(context, "admin", "admin", "admin@upm.es", roleAdmin);
            AddUser(context, "profesor1", "apell", "profesor1@upm.es", roleProfe);
            AddUser(context, "profesor2", "apell", "profesor2@upm.es", roleProfe);
            AddUser(context, "ficitio1", "apell", "ficticio1@alumnos.upm.es", roleAlum);
            AddUser(context, "ficitio2", "apell", "ficticio2@alumnos.upm.es", roleAlum);
            AddUser(context, "ficitio3", "apell", "ficticio3@alumnos.upm.es", roleAlum);
        }
        public void AddRole(ApplicationDbContext context, String role)
        {
            IdentityResult IdRoleResult;
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleMgr = new RoleManager<IdentityRole>(roleStore);
            if (!roleMgr.RoleExists(role))
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = role });
        }
        // Adaptar a los atributos definidos en ApplicationUser
        public void AddUser(ApplicationDbContext context, String name, String surname,
        String email, String role)
        {
            IdentityResult IdUserResult;
            var userMgr = new UserManager<ApplicationUser>(new
            UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                Name = name,
                Surname = surname,
                UserName = email,
                Email = email,
            };
            // Es imprescindible que NO se cambie la contraseña para que los profesores
            // puedan probar la práctica con esta misma
            IdUserResult = userMgr.Create(appUser, "123456Aa!");
            // Asociar usuario con role
            if (!userMgr.IsInRole(userMgr.FindByEmail(email).Id, role))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail(email).Id, role);
            }
        }
    }
}
