namespace savvy.Web.Models
{
    public abstract class QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionHtml { get; set; }
        public string SupplementalInfoHtml { get; set; }
    }
}
