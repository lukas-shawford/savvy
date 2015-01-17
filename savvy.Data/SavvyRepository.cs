using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;
using savvy.Data.Extensions;

namespace savvy.Data
{
    public class SavvyRepository : ISavvyRepository
    {
        private SavvyContext _ctx;

        public SavvyRepository(SavvyContext ctx)
        {
            _ctx = ctx;
        }

        public List<Quiz> GetAllQuizzes()
        {
            List<Quiz> quizzes = _ctx.Quizzes.Include(quiz => quiz.Questions).ToList();
            
            foreach (var quiz in quizzes)
            {
                foreach (var question in quiz.Questions)
                {
                    LoadQuestion(question);
                }
            }

            return quizzes;
        }

        /// <summary>
        /// Fully loads all properties for the given question.
        /// </summary>
        /// <param name="question">Partially-loaded question.</param>
        /// <remarks>
        /// Entity Framework does not have a good way to eager load navigation properties of derived classes when
        /// using Table Per Type inheritance. This method is a work around to load any additional properties that
        /// we may need for all the different kinds of questions (for example, loading all the choices for a
        /// multiple choice question).
        /// 
        /// See:
        /// https://connect.microsoft.com/VisualStudio/feedback/details/594289/in-entity-framework-there-should-be-a-way-to-eager-load-include-navigation-properties-of-a-derived-class
        /// </remarks>
        public void LoadQuestion(Question question)
        {
            var mc = question as MultipleChoiceQuestion;
            if (mc != null && mc.Choices == null)
            {
                mc.Choices = _ctx.Choices.Where(c => c.QuestionId == question.QuestionId).ToList();
            }
        }

        public bool CreateQuiz(Quiz quiz)
        {
            _ctx.Quizzes.Add(quiz);
            return _ctx.SaveChanges() > 0;
        }

        public Quiz GetQuiz(int quizId)
        {
            var quiz = _ctx.Quizzes.Include(q => q.Questions).FirstOrDefault(q => q.QuizId == quizId);

            if (quiz == null)
            {
                return null;
            }
            
            foreach (var question in quiz.Questions)
            {
                LoadQuestion(question);
            }

            return quiz;
        }

        public bool UpdateQuiz(Quiz quiz)
        {
            _ctx.Quizzes.AttachAsModified(quiz, _ctx);
            return _ctx.SaveChanges() > 0;
        }

        public bool DeleteQuiz(int quizId)
        {
            var quiz = _ctx.Quizzes.First(q => q.QuizId == quizId);
            _ctx.Quizzes.Remove(quiz);
            return _ctx.SaveChanges() > 0;
        }

        // Helper to update objects in context
        protected void UpdateEntity<T>(DbSet<T> dbSet, T entity) where T : class
        {
            dbSet.AttachAsModified(entity, _ctx);
        }
    }
}