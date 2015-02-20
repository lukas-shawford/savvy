using System.Linq;
using System.Web.Http;
using savvy.Data;
using savvy.Data.Entities.Questions;

namespace savvy.Web.Controllers
{
    public class EditQuestionController : BaseController
    {
        public EditQuestionController(ISavvyRepository repo) : base(repo)
        {
        }

        public IHttpActionResult Get(int quizId)
        {
            var quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz.Questions.Select(question => ModelFactory.Edit.Create(question)));
        }

        public IHttpActionResult Get(int quizId, int sequenceNum)
        {
            var question = Repository.GetQuestion(quizId, sequenceNum);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.Edit.Create(question));
        }
    }
}
