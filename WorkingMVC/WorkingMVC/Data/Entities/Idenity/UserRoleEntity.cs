using Microsoft.AspNetCore.Identity;

namespace WorkingMVC.Data.Entities.Idenity;

public class UserRoleEntity : IdentityUserRole<int>
{
    public UserEntity User { get; set; } = null!;
    public RoleEntity Role { get; set; } = null!;
}
