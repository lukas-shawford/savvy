using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;

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
            // Set database initializer - uncomment one of the initializers below.

            // SavvyDBInitializer: Currently, this will drop and recreate the database, and also seed it with sample data.
            // This is a bit slow, so if you know the data model has not changed, you can comment this out and uncomment
            // the next one instead (the one with the null initializer).
            Database.SetInitializer(new SavvyDBInitializer());

            // Null initializer (does nothing). If the data model hasn't changed, use this one (quicker for development).
            //Database.SetInitializer<SavvyContext>(null);

            // Migrate to latest version. Once the data model / API have stabilized a bit and we have actual data that
            // we need to worry about preserving, this is going to become the preferred scheme to use. This will
            // require writing migration scripts every time we want to make a change to the data model. Read up on
            // Code First Migrations when we're ready to switch over to this scheme.
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SavvyContext, MigrationConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<FillInQuestion> FillInQuestions { get; set; }

        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }

        public DbSet<Choice> Choices { get; set; }
    }

    public class SavvyDBInitializer : DropCreateDatabaseAlways<SavvyContext>
    {
        protected override void Seed(SavvyContext context)
        {
            new DataSeeder(context).Seed();
        }
    }
}
