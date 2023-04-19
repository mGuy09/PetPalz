using Microsoft.AspNetCore.Identity;

namespace PetPalz.Models.Dtos;

public class PersonalUserDto:UserDto
{
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}