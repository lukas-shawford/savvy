using System.Collections.Generic;
using savvy.Web.Models.Questions;

namespace savvy.Web.Models
{
    public class EditQuizModel
    {
        public int QuizId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EditQuestionModel> Questions { get; set; }
    }
}
