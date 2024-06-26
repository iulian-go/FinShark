namespace backend;

public interface ITokenService
{
    string CreateToken(AppUser appUser);
}
