using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace savvy.Data.Entities.Questions
{
    [Table("MultipleChoiceQuestion")]
    public class MultipleChoiceQuestion : Question
    {
        public virtual List<Choice> Choices { get; set; }
        public short CorrectAnswerIndex { get; set; }

        public bool IsCorrect(short Submission)
        {
            return Submission == CorrectAnswerIndex;
        }
    }
}
