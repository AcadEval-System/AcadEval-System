namespace AcadEvalSys.Application.Users.Dtos;

public class UserInfoDto
{
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}