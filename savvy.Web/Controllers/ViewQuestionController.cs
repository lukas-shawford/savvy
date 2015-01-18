using System.Linq;
using System.Web.Http;
using Microsoft.CSharp.RuntimeBinder;
using savvy.Data;
using savvy.Data.Entities.Questions;

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

        public IHttpActionResult Get(int quizId, int sequenceNum)
        {
            var question = Repository.GetQuestion(quizId, sequenceNum);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.View.Create(question));
        }

        public IHttpActionResult Post(int quizId, int sequenceNum, [FromBody] dynamic submission)
        {
            // Find the question
            var question = Repository.GetQuestion(quizId, sequenceNum);
            if (question == null)
            {
                return NotFound();
            }

            // Perform some basic validation on the submission
            try
            {
                if (submission == null || submission.answer == null)
                {
                    return BadRequest("The body should contain an \"answer\" property containing the user's submission.");
                }
            }
            catch (RuntimeBinderException)
            {
                return BadRequest("submission should be a JSON object");
            }

            // Grade fill-in
            var fillInQuestion = question as FillInQuestion;
            if (fillInQuestion != null)
            {
                return Ok(fillInQuestion.IsCorrect(submission.answer.ToString()));
            }

            // Grade multiple choice
            var multipleChoiceQuestion = question as MultipleChoiceQuestion;
            if (multipleChoiceQuestion != null)
            {
                short answer;
                if (short.TryParse(submission.answer.ToString(), out answer))
                {
                    return Ok(multipleChoiceQuestion.IsCorrect(answer));
                }
                
                return BadRequest("The \"answer\" should be a number corresponding to the correct answer index (starting with zero).");
            }

            // Grading logic not defined
            return InternalServerError();
        }
    }
}
