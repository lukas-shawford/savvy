using System.Collections.Generic;

namespace savvy.Web.Models.Questions
{
    public class ViewMultipleChoiceQuestionModel : ViewQuestionModel
    {
        public virtual List<ChoiceModel> Choices { get; set; }
    }
}
