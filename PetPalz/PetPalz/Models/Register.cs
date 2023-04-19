using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetPalz.Models;

public class Register : BaseAuth
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "FirstName")]
    public string FirstName { get; set; }
    [Required]
    [StringLength(50)]
    [Display(Name = "LastName")]
    public string LastName { get; set; }

    [Required]
    [Display(Name = "PhoneNumber")]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 6)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}