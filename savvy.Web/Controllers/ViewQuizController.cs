using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using savvy.Data;
using savvy.Web.Models;

namespace savvy.Web.Controllers
{
    public class ViewQuizController : BaseController
    {
        public ViewQuizController(ISavvyRepository repo) : base(repo)
        {
        }

        public List<ViewQuizModel> Get()
        {
            return Repository.GetAllQuizzes().Select(quiz => ModelFactory.View.Create(quiz)).ToList();
        }

        public IHttpActionResult Get(int id)
        {
            var quiz = Repository.GetQuiz(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(ModelFactory.View.Create(quiz));
        }
    }
}
