using System.Collections.Generic;

namespace savvy.Web.Models.Questions
{
    public class EditMultipleChoiceQuestionModel : EditQuestionModel
    {
        public virtual List<ChoiceModel> Choices { get; set; }
        public short CorrectAnswerIndex { get; set; }
    }
}
