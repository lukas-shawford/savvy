using System.Linq;
using System.Web.Http;
using savvy.Data;

namespace savvy.Web.Controllers
{
    public class ViewQuestionController : BaseController
    {
        public ViewQuestionController(ISavvyRepository repo) : base(repo)
        {
        }

        public IHttpActionResult Get(int quizId)
        {
            var quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz.Questions.Select(question => ModelFactory.View.Create(question)));
        }

        public IHttpActionResult Get(int quizId, int questionId)
        {
            var question = Repository.GetQuestion(questionId);

            if (question == null || question.QuizId != quizId)
            {
                return NotFound();
            }

            return Ok(ModelFactory.View.Create(question));
        }
    }
}
