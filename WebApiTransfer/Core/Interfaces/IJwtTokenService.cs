using Domain.Entities.Idenity;

namespace Core.Interfaces;

public interface IJwtTokenService
{
    Task<string> CreateAsync(UserEntity user);
}
