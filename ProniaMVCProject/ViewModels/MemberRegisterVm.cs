using System.ComponentModel.DataAnnotations;

namespace ProniaMVCProject.ViewModels;

public class MemberRegisterVm
{
    [Required]
    [StringLength(50)]
    public string FullName { get; set; }
    [Required]
    [StringLength(50)]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; } 
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [MinLength(8)]
    [Compare(nameof(Password))]
    [DataType(DataType.Password)]
    public string RepeatPassword { get; set; }
    public bool IsPersistent { get; set; }
}
