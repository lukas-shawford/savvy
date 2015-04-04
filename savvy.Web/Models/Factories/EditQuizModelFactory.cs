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
            return new Quiz
            {
                QuizId = model.QuizId,
                Name = model.Name,
                Description = model.Description,
                Questions = model.Questions.Select(Parse).ToList()
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
            model.QuestionHtml = question.QuestionHtml;
            model.SupplementalInfoHtml = question.SupplementalInfoHtml;

            return model;
        }

        public Question Parse(EditQuestionModel model)
        {
            switch (model.Type)
            {
                case QuestionType.FillIn:
                    return Parse(model, new FillInQuestion());
                case QuestionType.MultipleChoice:
                    return Parse(model, new MultipleChoiceQuestion());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Question Parse(EditQuestionModel model, Question question)
        {
            switch (model.Type)
            {
                case QuestionType.FillIn:
                    question = Parse((EditFillInQuestionModel) model, question as FillInQuestion ?? new FillInQuestion());
                    break;
                case QuestionType.MultipleChoice:
                    question = Parse((EditMultipleChoiceQuestionModel) model, question as MultipleChoiceQuestion ?? new MultipleChoiceQuestion());
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
            return Parse(model, new FillInQuestion());
        }

        private FillInQuestion Parse(EditFillInQuestionModel model, FillInQuestion question)
        {
            question.Answer = model.Answer;

            return question;
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
            return Parse(model, new MultipleChoiceQuestion());
        }

        private MultipleChoiceQuestion Parse(EditMultipleChoiceQuestionModel model, MultipleChoiceQuestion question)
        {
            UpdateChoices(model, question);
            question.CorrectAnswerIndex = model.CorrectAnswerIndex;

            return question;
        }

        private void UpdateChoices(EditMultipleChoiceQuestionModel model, MultipleChoiceQuestion question)
        {
            if (question.Choices == null)
            {
                question.Choices = new List<Choice>();
            }

            for (int i = 0; i < model.Choices.Count; i++)
            {
                var choice = (i >= question.Choices.Count) ? new Choice() : question.Choices[i];

                choice = ModelFactory.Parse(model.Choices[i], model.QuestionId, choice);

                if (i >= question.Choices.Count)
                {
                    question.Choices.Add(choice);
                }
                else
                {
                    question.Choices[i] = choice;
                }
            }

            if (question.Choices.Count > model.Choices.Count)
            {
                question.Choices.RemoveRange(model.Choices.Count, question.Choices.Count - model.Choices.Count);
            }
        }
    }
}
