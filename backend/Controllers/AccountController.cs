using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend;

#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CA1862 // Use the 'StringComparison' method overloads to perform case-insensitive string comparisons

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;
    private readonly SignInManager<AppUser> signInManager;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await this.userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.Username.ToLowerInvariant());

        if (user != null)
        {
            var result = await this.signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (result.Succeeded) return Ok(new NewUserDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Token = this.tokenService.CreateToken(user)
            });
        }

        return Unauthorized("Invalid username and/or password");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email
            };

            var createdUser = await this.userManager.CreateAsync(appUser, registerDTO.Password);
            if (createdUser.Succeeded)
            {
                var role = await this.userManager.AddToRoleAsync(appUser, "User");
                if (role.Succeeded) return Ok(
                    new NewUserDTO
                    {
                        Username = appUser.UserName,
                        Email = appUser.Email,
                        Token = this.tokenService.CreateToken(appUser)
                    });
                else return StatusCode(500, role.Errors);
            }
            else return StatusCode(500, createdUser.Errors);

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}
