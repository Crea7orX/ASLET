using System.ComponentModel.DataAnnotations;

namespace ASLET.Server.DTOs;

public class RegisterDto
{

    [Required]
    public string Firstname { get; set; }
    [Required]
    public string Lastname { get; set; }
    [Required]
    public  string School { get; set; }
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}