using Microsoft.EntityFrameworkCore;
using LabYonetimSistemi.Models;

namespace LabYonetimSistemi.Endpoints;

public static class StatEndpoints
{
    public static void MapStatEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/stats", async (AppDbContext db) => {
            var stats = new
            {
                labCount = await db.Labs.CountAsync(),
                pcCount = await db.Computers.CountAsync(),
                studentCount = await db.Students.CountAsync(),
                issueCount = await db.Issues.CountAsync(i => !i.IsResolved)
            };
            return Results.Ok(stats);
        });
    }
}