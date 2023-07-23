using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MachineTest.Models
{
    public class product
    {
        [Key]
        public int productid { get; set; }
        public string productname { get; set; } = default!;

        public int categoryid { get; set; }
        [ForeignKey("categoryid")]
        public category category { get; set; } = default!;
    }
}

