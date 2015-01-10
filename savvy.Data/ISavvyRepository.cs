using System.Collections.Generic;
using savvy.Data.Entities;

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
    }
}
