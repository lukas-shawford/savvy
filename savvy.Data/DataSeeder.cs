//#define TEST_SEED
//#define FORCE_RECREATE

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;

namespace savvy.Data
{
    public class DataSeeder
    {
        SavvyContext _ctx;
        public DataSeeder(SavvyContext ctx)
        {
            _ctx = ctx;
        }

        public void Seed()
        {
#if !(TEST_SEED || FORCE_RECREATE)
            if (_ctx.Quizzes.Any())
            {
                return;
            }
#endif

#if TEST_SEED || FORCE_RECREATE

            /*
            ExecuteQueries(
                "DELETE FROM dbo.Quizzes",
                "DELETE FROM dbo.Questions",
                etc...
            );
            */
#endif

            SeedQuizzes();
        }

        private void SeedQuizzes()
        {
            _ctx.Quizzes.Add(new Quiz()
            {
                Name = "Sample Quiz",
                Description = "Sample quiz for testing.",
                Questions = new List<Question>
                {
                    new FillInQuestion
                    {
                        SequenceNum = 1,
                        QuestionHtml = "In what year did the Battle of Hastings take place?",
                        Answer = "1066"
                    },
                    new MultipleChoiceQuestion
                    {
                        SequenceNum = 2,
                        QuestionHtml = "How many words are in the US constitution? (Pick the closest answer.)",
                        Choices = new List<Choice>
                        {
                            new Choice { ChoiceText = "1,000" },
                            new Choice { ChoiceText = "5,000" },
                            new Choice { ChoiceText = "10,000" },
                            new Choice { ChoiceText = "20,000" }
                        },
                        CorrectAnswerIndex = 1
                    },
                    new FillInQuestion
                    {
                        SequenceNum = 3,
                        QuestionHtml = "What is the default squawk code for VFR aircraft in the United States?",
                        Answer = "1200"
                    }
                }
            });
        }
    }
}
