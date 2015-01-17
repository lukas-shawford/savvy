using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace savvy.Data.Entities.Questions
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public int QuestionId { get; set; }
        public virtual MultipleChoiceQuestion Question { get; set; }
        
    }
}
