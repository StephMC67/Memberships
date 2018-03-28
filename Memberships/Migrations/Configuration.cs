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

            // cette classe "Memberships.Migrations a �t� cr�e apres la commande
            //  enable-migrations (dans la console du gestionnaire de packahe (lancment rapide / package )
            // possible d'ajouter les bool�ens de migration automatique ou de perte et
            // de cr�er des donn�es dans les tables dans la m�thode surcharg�e "seed"

            // pour cr�er la base de donn�es proprement dit, il faut lancer la commande
            // update-databe ==> lance une mise � jour et cr�er la base de donn�es.

            //// par defaut � false... 
            ///AutomaticMigrationsEnabled = false;
            AutomaticMigrationsEnabled = true;

            //ajout : permet de dire pas de perte de donn�es (� true pour ce projet example)
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
