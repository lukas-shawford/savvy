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
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SavvyContext, MigrationConfiguration>());

            Database.SetInitializer(new SavvyDBInitializer());
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
