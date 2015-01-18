namespace savvy.Web.Models.Questions
{
    public abstract class ViewQuestionModel
    {
        public int QuestionId { get; set; }
        public int SequenceNum { get; set; }
        public string QuestionHtml { get; set; }
    }
}
