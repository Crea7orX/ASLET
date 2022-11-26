using System.ComponentModel.DataAnnotations;

namespace ASLET.Server.DTOs;

public class LoginDto
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}