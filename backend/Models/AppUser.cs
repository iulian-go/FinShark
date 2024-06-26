using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend;

public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = [];
}
