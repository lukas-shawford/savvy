using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using savvy.Data.Entities;

namespace savvy.Data
{
    public class SavvyContext : DbContext
    {
        public SavvyContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        static SavvyContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SavvyContext, MigrationConfiguration>());

            //Database.SetInitializer(new DropCreateDatabaseAlways<SavvyContext>());
        }

        public DbSet<Quiz> Quizzes { get; set; }
    }
}
