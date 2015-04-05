using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using savvy.Data;
using savvy.Data.Entities;
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
            Quiz quiz;
            IHttpActionResult responseMessage;

            // Validate
            if (!ValidateQuestion(quizId, sequenceNum, questionModel, out quiz, out responseMessage))
            {
                return responseMessage;
            }

            // Get the old question and delete it
            int index = sequenceNum - 1;
            var oldQuestion = quiz.Questions[index];
            if (!Repository.DeleteQuestion(oldQuestion.QuestionId))
            {
                return InternalServerError();
            }

            // Parse the new question
            var question = ModelFactory.Edit.Parse(questionModel);

            // Disallow changing certain fields
            question.QuestionId = 0;
            question.QuizId = quizId;
            question.SequenceNum = sequenceNum;

            // Add the new question
            if (Repository.CreateQuestion(question))
            {
                return Ok(ModelFactory.Edit.Create(question));
            }
            
            return InternalServerError();
        }

        public IHttpActionResult Delete(int quizId, int sequenceNum)
        {
            var quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid quiz."));
            }

            var index = sequenceNum - 1;
            if (index < 0 || index >= quiz.Questions.Count)
            {
                return NotFound();
            }

            if (Repository.DeleteQuestion(quiz.Questions[index].QuestionId))
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }

            return InternalServerError();
        }

        private bool ValidateQuestion(int quizId, int sequenceNum, EditQuestionModel questionModel, out Quiz quiz, out IHttpActionResult response)
        {
            quiz = null;
            response = null;

            if (questionModel == null)
            {
                response = BadRequest("Question is missing.");
                return false;
            }

            quiz = Repository.GetQuiz(quizId);

            if (quiz == null)
            {
                response = ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Invalid quiz."));
                return false;
            }

            var index = sequenceNum - 1;
            if (index < 0 || index >= quiz.Questions.Count)
            {
                response = NotFound();
                return false;
            }

            return true;
        }
    }
}
