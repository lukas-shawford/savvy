using System.Collections.Generic;

namespace savvy.Data.Entities.Questions
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Choices { get; set; }
        public ushort CorrectAnswerIndex { get; set; }
    }
}