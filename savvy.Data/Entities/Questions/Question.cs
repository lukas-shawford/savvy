using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace savvy.Data.Entities.Questions
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; }

        [Index("IX_Question_SequenceNum", 2, IsUnique = true)]
        public int SequenceNum { get; set; }

        public string QuestionHtml { get; set; }

        public string SupplementalInfoHtml { get; set; }

        [Index("IX_Question_SequenceNum", 1, IsUnique = true)]
        public int QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
