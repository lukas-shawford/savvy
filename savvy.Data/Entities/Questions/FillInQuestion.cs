using System.ComponentModel.DataAnnotations.Schema;

namespace savvy.Data.Entities.Questions
{
    [Table("FillInQuestion")]
    public class FillInQuestion : Question
    {
        public string Answer { get; set; }
    }
}
