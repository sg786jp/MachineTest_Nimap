using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MachineTest.Models
{
    public class ViewModel
    {
        [Key]
        public int productid { get; set; }
        [Required(ErrorMessage ="Please Enter Product Name")]
        [Display(Name = "Product Name :")]
        public string productname { get; set; }

        [Display(Name = "Category ID :")]
        [Required(ErrorMessage = "Please Select Category")]
        public int categoryid { get; set; }
        [ForeignKey("categoryid")]
       

        public category category { get; set; } = default!;

        [Required]
        [Display(Name = "Category Name :")]
        public string categoryname { get; set; } = default!;
       
    }
}

