using System.Collections.Generic;

namespace savvy.Web.Models
{
    public class QuizModel
    {
        public int QuizId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
