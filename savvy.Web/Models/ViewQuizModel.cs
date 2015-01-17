using System.Collections.Generic;

namespace savvy.Web.Models
{
    public class ViewQuizModel
    {
        public int QuizId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ViewQuestionModel> Questions { get; set; }
    }
}
