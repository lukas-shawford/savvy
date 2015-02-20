using System.Collections.Generic;
using savvy.Data.Entities;
using savvy.Data.Entities.Questions;

namespace savvy.Data
{
    public interface ISavvyRepository
    {
        #region Quizzes

        List<Quiz> GetAllQuizzes();

        bool CreateQuiz(Quiz quiz);

        Quiz GetQuiz(int quizId);

        bool UpdateQuiz(Quiz quiz);

        bool DeleteQuiz(int quizId);

        #endregion

        #region Questions

        bool CreateQuestion(Question question);

        Question GetQuestion(int questionId);

        Question GetQuestion(int quizId, int sequenceNum);

        bool UpdateQuestion(Question question);

        #endregion
    }
}
