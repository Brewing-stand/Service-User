namespace Service_User.DTOs;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public long GitId { get; set; }
    public string Username { get; set; }
    public string Avatar { get; set; }
}
