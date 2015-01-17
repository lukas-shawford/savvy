namespace savvy.Web.Models.Questions
{
    public abstract class EditQuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionHtml { get; set; }
        public string SupplementalInfoHtml { get; set; }
    }
}
