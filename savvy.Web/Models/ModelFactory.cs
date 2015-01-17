using savvy.Data.Entities.Questions;

namespace savvy.Web.Models
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
    }
}
