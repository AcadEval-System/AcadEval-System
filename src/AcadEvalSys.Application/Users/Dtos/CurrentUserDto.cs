namespace AcadEvalSys.Application.Users.Dtos;

public record CurrentUserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? UserName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = new List<string>();
}