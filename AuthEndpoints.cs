using Microsoft.EntityFrameworkCore;
using LabYonetimSistemi.Models;
namespace LabYonetimSistemi.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/login", async (AppDbContext db, User loginData) => {
            var user = await db.Users.FirstOrDefaultAsync(u =>
                u.Username.ToLower() == loginData.Username.ToLower() &&
                u.Password == loginData.Password);

            if (user == null) return Results.Unauthorized();
            return Results.Ok(new { username = user.Username, role = user.Role });
        });
    }
}