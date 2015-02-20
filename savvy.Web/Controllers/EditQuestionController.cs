using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using savvy.Data;
using savvy.Web.Models.Questions;

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

        public IHttpActionResult Post(int quizId, EditQuestionModel questionModel)
        {
            if (questionModel == null)
            {
                return BadRequest("Question is missing.");
            }

            var quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid quiz."));
            }

            var question = ModelFactory.Edit.Parse(questionModel);
            
            question.QuizId = quiz.QuizId;

            if (Repository.CreateQuestion(question))
            {
                return Ok(ModelFactory.Edit.Create(question));
            }

            return InternalServerError();
        }
    }
}
