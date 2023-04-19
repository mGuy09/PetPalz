using System.ComponentModel.DataAnnotations;

namespace PetPalz.Models;

public class BaseAuth
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }
}