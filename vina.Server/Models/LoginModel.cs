using System.ComponentModel.DataAnnotations;

namespace vina.Server.Models;

public class LoginModel
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}
