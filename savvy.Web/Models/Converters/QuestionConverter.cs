using System;
using Newtonsoft.Json.Linq;
using savvy.Web.Models.Questions;

namespace savvy.Web.Models.Converters
{
    public class QuestionConverter : JsonCreationConverter<EditQuestionModel>
    {
        protected override EditQuestionModel Create(Type objectType, JObject jObject)
        {
            switch (jObject.GetValue("Type", StringComparison.OrdinalIgnoreCase).Value<string>())
            {
                case "FillIn":
                    return new EditFillInQuestionModel();
                case "MultipleChoice":
                    return new EditMultipleChoiceQuestionModel();
                default:
                    return null;
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }
    }
}
