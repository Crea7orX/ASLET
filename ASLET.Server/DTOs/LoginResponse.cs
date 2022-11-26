namespace ASLET.Server.DTOs;

public class LoginResponseDto
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public  string School { get; set; }
    public DateTime TokenExpires { get; set; }
    public string Token { get; set; }
}