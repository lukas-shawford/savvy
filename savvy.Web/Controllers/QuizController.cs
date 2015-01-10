using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using savvy.Data;
using savvy.Data.Entities;

namespace savvy.Web.Controllers
{
    public class QuizController : BaseController
    {
        public QuizController(ISavvyRepository repo) : base(repo)
        {
        }

        public List<Quiz> Get()
        {
            return Repository.GetAllQuizzes();
        }

        public Quiz Get(int id)
        {
            return Repository.GetQuiz(id);
        }
    }
}
