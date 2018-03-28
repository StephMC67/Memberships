namespace Memberships.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Memberships.Models.ApplicationDbContext>
    {
        public Configuration()
        {

            // cette classe "Memberships.Migrations a été crée apres la commande
            //  enable-migrations (dans la console du gestionnaire de packahe (lancment rapide / package )
            // possible d'ajouter les booléens de migration automatique ou de perte et
            // de créer des données dans les tables dans la méthode surchargée "seed"

            // pour créer la base de données proprement dit, il faut lancer la commande
            // update-databe ==> lance une mise à jour et créer la base de données.

            //// par defaut à false... 
            ///AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;

            //ajout : permet de dire pas de perte de données (à true pour ce projet example)
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Memberships.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
