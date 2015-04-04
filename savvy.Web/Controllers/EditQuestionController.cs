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
            question.SequenceNum = quiz.Questions.Max(q => q.SequenceNum) + 1;

            if (Repository.CreateQuestion(question))
            {
                return Ok(ModelFactory.Edit.Create(question));
            }

            return InternalServerError();
        }

        public IHttpActionResult Put(int quizId, int sequenceNum, EditQuestionModel questionModel)
        {
            // Validate

            if (questionModel == null)
            {
                return BadRequest("Question is missing.");
            }

            var quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid quiz."));
            }

            var index = sequenceNum - 1;
            if (index < 0 || index >= quiz.Questions.Count)
            {
                return BadRequest("Question number out of range.");
            }

            // Get the old question
            var question = quiz.Questions[index];

            // Don't allow changing the QuestionID
            questionModel.QuestionId = question.QuestionId;

            // Update the old question with the new values
            question = ModelFactory.Edit.Parse(questionModel, question);

            // Don't allow changing the QuizID or the sequence number
            question.QuizId = quiz.QuizId;
            question.SequenceNum = sequenceNum;

            // Save changes
            if (Repository.UpdateQuestion(question))
            {
                return Ok(ModelFactory.Edit.Create(question));
            }

            return InternalServerError();
        }
    }
}
