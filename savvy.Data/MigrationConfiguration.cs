using System.Data.Entity.Migrations;

namespace savvy.Data
{
    public class MigrationConfiguration : DbMigrationsConfiguration<SavvyContext>
    {
        public MigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

#if DEBUG
        protected override void Seed(SavvyContext context)
        {
            // Seed the database if necessary
            new DataSeeder(context).Seed();
        }
#endif
    }
}
