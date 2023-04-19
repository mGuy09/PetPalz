namespace PetPalz.Models.Dtos;

public class UserDetailsDto
{
    public string Role { get; set; }
    public List<int> QualificationIds { get; set; }
    public int ServiceTypeId { get; set; }
    public string? description { get; set; }
}