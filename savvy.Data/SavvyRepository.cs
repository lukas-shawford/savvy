using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using savvy.Data.Entities;
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
            return _ctx.Quizzes.ToList();
        }

        public bool CreateQuiz(Quiz quiz)
        {
            _ctx.Quizzes.Add(quiz);
            return _ctx.SaveChanges() > 0;
        }

        public Quiz GetQuiz(int quizId)
        {
            return _ctx.Quizzes.FirstOrDefault(q => q.QuizId == quizId);
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