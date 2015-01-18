using System;
using System.Linq;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;
using savvy.Web.Models.Questions;

namespace savvy.Web.Models.Factories
{
    public class EditQuizModelFactory
    {
        protected ModelFactory ModelFactory;

        public EditQuizModelFactory(ModelFactory ModelFactory)
        {
            this.ModelFactory = ModelFactory;
        }

        public EditQuizModel Create(Quiz quiz)
        {
            return new EditQuizModel
            {
                QuizId = quiz.QuizId,
                Name = quiz.Name,
                Description = quiz.Description,
                Questions = quiz.Questions.Select(Create).ToList()
            };
        }

        public EditQuestionModel Create(Question question)
        {
            EditQuestionModel model;

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
            model.SequenceNum = question.SequenceNum;
            model.QuestionHtml = question.QuestionHtml;
            model.SupplementalInfoHtml = question.SupplementalInfoHtml;

            return model;
        }

        public EditFillInQuestionModel Create(FillInQuestion question)
        {
            return new EditFillInQuestionModel
            {
                Answer = question.Answer
            };
        }

        public EditMultipleChoiceQuestionModel Create(MultipleChoiceQuestion question)
        {
            return new EditMultipleChoiceQuestionModel
            {
                Choices = question.Choices.Select(ModelFactory.Create).ToList(),
                CorrectAnswerIndex = question.CorrectAnswerIndex
            };
        }
    }
}
