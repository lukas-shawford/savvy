using System.Collections.Generic;

namespace savvy.Web.Models
{
    public class ViewMultipleChoiceQuestionModel : ViewQuestionModel
    {
        public virtual List<ChoiceModel> Choices { get; set; }
    }
}
