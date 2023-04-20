using System.ComponentModel.DataAnnotations;

namespace FrizerskiSalon.Models.DTOs;

public class UserDTO
{
    [Required(ErrorMessage ="Name is required")]
    [StringLength(50, MinimumLength =2, ErrorMessage ="Name must be between 2 and 50 character")] 
    public string? Name { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Pasword must be between 4 and 50 character")]
    public string? Password { get; set; }//password
    [RegularExpression(@"^[\d]{8,14}$", ErrorMessage = "Phone number should be between 8 and 14 digits")]
    public string? Phone { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
}
