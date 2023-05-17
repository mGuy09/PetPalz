namespace PetPalz.Models.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfilePicUrl { get; set; }
    public string UserType { get; set; }
    public ServiceType ServiceType { get; set; }
    public List<Qualification>? Qualifications { get; set; }
    public UserRating Rating { get; set; }
    public int? YearsOfExperience { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
}