namespace savvy.Web.Models.Questions
{
    public abstract class ViewQuestionModel
    {
        public int QuestionId { get; set; }
        public QuestionType Type { get; set; }
        public string QuestionHtml { get; set; }
    }
}
