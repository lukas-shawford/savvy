using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace savvy.Data.Entities.Questions
{
    public abstract class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }
        public string QuestionHtml { get; set; }
        public string SupplementalInfoHtml { get; set; }
        public int QuizId { get; set; }
        public virtual Quiz Quiz { get; set; }
    }
}
