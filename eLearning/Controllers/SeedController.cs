using Microsoft.AspNetCore.Mvc;
using eLearning.Seeder;
using Microsoft.AspNetCore.Authorization;

namespace eLearning.Controllers;

[AllowAnonymous]
public class SeedController : Controller
{
    private readonly IUserSeeder _userSeeder;

    public SeedController(IUserSeeder userSeeder)
    {
        _userSeeder = userSeeder;
    }

    public async Task<IActionResult> SeedAdminUser()
    {
        try
        {
            await _userSeeder.SeedAdminUser();
            return Content("Admin user seeded successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}