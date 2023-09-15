using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class AddPersonModel
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50, ErrorMessage = "Cannot accept more than 50 characters")]
        [RegularExpression("^[a-zA-Z''-'\\s]{1,50}$", ErrorMessage = "Invalid name")]
        [MinLength(1, ErrorMessage = "Should have at least 1 character")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Cannot accept more than 50 characters")]
        [RegularExpression("^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
    }
}
