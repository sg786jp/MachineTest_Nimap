using System.ComponentModel.DataAnnotations;

namespace MachineTest.Models
{
    public class category
    {
        [Key]
        public int categoryid { get; set; }
        [Required(ErrorMessage = "Please Enter ID")]
        public string categoryname { get; set; } = default!;
    }
}

