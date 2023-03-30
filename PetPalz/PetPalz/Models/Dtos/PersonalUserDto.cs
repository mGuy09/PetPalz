namespace PetPalz.Models.Dtos;

public class PersonalUserDto:UserDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}