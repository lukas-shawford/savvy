using System;
using System.Linq;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;

namespace savvy.Web.Models
{
    public class ViewQuizModelFactory
    {
        protected ModelFactory ModelFactory;

        public ViewQuizModelFactory(ModelFactory ModelFactory)
        {
            this.ModelFactory = ModelFactory;
        }

        public ViewQuizModel Create(Quiz quiz)
        {
            return new ViewQuizModel
            {
                QuizId = quiz.QuizId,
                Name = quiz.Name,
                Description = quiz.Description,
                Questions = quiz.Questions.Select(Create).ToList()
            };
        }

        public ViewQuestionModel Create(Question question)
        {
            ViewQuestionModel model;

            if (question is FillInQuestion)
            {
                model = Create((FillInQuestion)question);
            }
            else if (question is MultipleChoiceQuestion)
            {
                model = Create((MultipleChoiceQuestion)question);
            }
            else
            {
                throw new ArgumentException("Unrecognized question type: " + question.GetType().Name, "question");
            }

            model.QuestionId = question.QuestionId;
            model.QuestionHtml = question.QuestionHtml;

            return model;
        }

        public ViewFillInQuestionModel Create(FillInQuestion question)
        {
            return new ViewFillInQuestionModel();
        }

        public ViewMultipleChoiceQuestionModel Create(MultipleChoiceQuestion question)
        {
            return new ViewMultipleChoiceQuestionModel
            {
                Choices = question.Choices.Select(ModelFactory.Create).ToList()
            };
        }
    }
}
