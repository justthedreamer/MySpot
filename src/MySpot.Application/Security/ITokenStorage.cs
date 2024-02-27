using MySpot.Application.DTO;

namespace MySpot.Application.Abstractions;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}