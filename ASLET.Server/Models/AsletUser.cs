using System.Text;

namespace ASLET.Models;

public class AsletUser
{
    public int ID { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string School { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public string Fullname()
    {
        var sb = new StringBuilder();
        sb.Append(Firstname);
        sb.Append(' ');
        sb.Append(Lastname);
        return sb.ToString();
    }
}