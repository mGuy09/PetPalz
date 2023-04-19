using System.ComponentModel.DataAnnotations;

namespace PetPalz.Models;

public class Login
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Username { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}