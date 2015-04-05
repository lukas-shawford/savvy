using System;
using System.Collections.Generic;
using System.Linq;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;
using savvy.Web.Models.Questions;

namespace savvy.Web.Models.Factories
{
    public class EditQuizModelFactory
    {
        protected ModelFactory ModelFactory;

        public EditQuizModelFactory(ModelFactory modelFactory)
        {
            ModelFactory = modelFactory;
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

        public Quiz Parse(EditQuizModel model)
        {
            var quiz = new Quiz
            {
                QuizId = model.QuizId,
                Name = model.Name,
                Description = model.Description,
                Questions = (model.Questions ?? new List<EditQuestionModel>()).Select(Parse).ToList()
            };

            // Set sequence numbers for each question
            for (int i = 0; i < quiz.Questions.Count; i++)
            {
                quiz.Questions[i].SequenceNum = i + 1;
            }

            return quiz;
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
            model.QuestionHtml = question.QuestionHtml;
            model.SupplementalInfoHtml = question.SupplementalInfoHtml;

            return model;
        }

        public Question Parse(EditQuestionModel model)
        {
            Question question;

            switch (model.Type)
            {
                case QuestionType.FillIn:
                    question = Parse((EditFillInQuestionModel) model);
                    break;
                case QuestionType.MultipleChoice:
                    question = Parse((EditMultipleChoiceQuestionModel) model);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            question.QuestionId = model.QuestionId;
            question.QuestionHtml = model.QuestionHtml;
            question.SupplementalInfoHtml = model.SupplementalInfoHtml;

            return question;
        }

        private EditFillInQuestionModel Create(FillInQuestion question)
        {
            return new EditFillInQuestionModel
            {
                Type = QuestionType.FillIn,
                Answer = question.Answer
            };
        }

        private FillInQuestion Parse(EditFillInQuestionModel model)
        {
            return new FillInQuestion
            {
                Answer = model.Answer
            };
        }

        private EditMultipleChoiceQuestionModel Create(MultipleChoiceQuestion question)
        {
            return new EditMultipleChoiceQuestionModel
            {
                Type = QuestionType.MultipleChoice,
                Choices = question.Choices.Select(ModelFactory.Create).ToList(),
                CorrectAnswerIndex = question.CorrectAnswerIndex
            };
        }

        private MultipleChoiceQuestion Parse(EditMultipleChoiceQuestionModel model)
        {
            return new MultipleChoiceQuestion
            {
                Choices = model.Choices.Select(c => new Choice
                {
                    ChoiceId = c.ChoiceId,
                    ChoiceText = c.ChoiceText
                }).ToList(),
                CorrectAnswerIndex = model.CorrectAnswerIndex
            };
        }
    }
}
