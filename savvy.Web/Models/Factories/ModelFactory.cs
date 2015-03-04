using savvy.Data.Entities.Questions;
using savvy.Web.Models.Questions;

namespace savvy.Web.Models.Factories
{
    public class ModelFactory
    {
        public ViewQuizModelFactory View;
        public EditQuizModelFactory Edit;

        public ModelFactory()
        {
            View = new ViewQuizModelFactory(this);
            Edit = new EditQuizModelFactory(this);
        }

        public ChoiceModel Create(Choice choice)
        {
            return new ChoiceModel
            {
                ChoiceId = choice.ChoiceId,
                ChoiceText = choice.ChoiceText
            };
        }

        public Choice Parse(ChoiceModel model, int questionId)
        {
            return Parse(model, questionId, new Choice());
        }

        public Choice Parse(ChoiceModel model, int questionId, Choice choice)
        {
            choice.QuestionId = questionId;
            choice.ChoiceId = model.ChoiceId;
            choice.ChoiceText = model.ChoiceText;

            return choice;
        }
    }
}
