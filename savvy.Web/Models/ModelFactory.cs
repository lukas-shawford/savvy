using System;
using System.Linq;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;

namespace savvy.Web.Models
{
    public class ModelFactory
    {
        public QuizModel Create(Quiz quiz)
        {
            return new QuizModel
            {
                QuizId = quiz.QuizId,
                Name = quiz.Name,
                Description = quiz.Description,
                Questions = quiz.Questions.Select(Create).ToList()
            };
        }

        public QuestionModel Create(Question question)
        {
            QuestionModel model;

            if (question is FillInQuestion)
            {
                model = Create((FillInQuestion) question);
            }
            else if (question is MultipleChoiceQuestion)
            {
                model = Create((MultipleChoiceQuestion) question);
            }
            else
            {
                throw new ArgumentException("Unrecognized question type: " + question.GetType().Name, "question");
            }

            model.QuestionId = question.QuestionId;
            model.QuestionHtml = question.QuestionHtml;
            model.SupplementalInfoHtml = question.SupplementalInfoHtml;

            return model;
        }

        public FillInQuestionModel Create(FillInQuestion question)
        {
            return new FillInQuestionModel
            {
                Answer = question.Answer
            };
        }

        public MultipleChoiceQuestionModel Create(MultipleChoiceQuestion question)
        {
            return new MultipleChoiceQuestionModel
            {
                Choices = question.Choices.Select(Create).ToList(),
                CorrectAnswerIndex = question.CorrectAnswerIndex
            };
        }

        public ChoiceModel Create(Choice choice)
        {
            return new ChoiceModel
            {
                ChoiceId = choice.ChoiceId,
                ChoiceText = choice.ChoiceText
            };
        }
    }
}
