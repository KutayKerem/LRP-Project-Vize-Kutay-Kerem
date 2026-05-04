using Microsoft.EntityFrameworkCore;
using LabYonetimSistemi.Models;

namespace LabYonetimSistemi.Endpoints;

public static class LabEndpoints
{
    public static void MapLabEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/admin/labs");

        // 1. Lab Listeleme
        group.MapGet("/", async (AppDbContext db) =>
            await db.Labs.Include(l => l.Computers).ToListAsync());

        // 2. Lab Ekleme
        group.MapPost("/", async (AppDbContext db, Lab lab) => {
            db.Labs.Add(lab);
            await db.SaveChangesAsync();
            return Results.Ok(lab);
        });

        // 3. Lab Güncelleme (HOCANIN İSTEDİĞİ KRİTİK EKSİK BUYDU)
        group.MapPut("/{id}", async (AppDbContext db, int id, Lab updatedLab) => {
            var lab = await db.Labs.FindAsync(id);
            if (lab == null) return Results.NotFound();
            lab.Name = updatedLab.Name;
            await db.SaveChangesAsync();
            return Results.Ok(lab);
        });

        // 4. Lab Silme
        group.MapDelete("/{id}", async (AppDbContext db, int id) => {
            var lab = await db.Labs.FindAsync(id);
            if (lab == null) return Results.NotFound();
            db.Labs.Remove(lab);
            await db.SaveChangesAsync();
            return Results.Ok();
        });

        // 5. Bilgisayar Ekleme (Otomatik AssetCode Üretimi)
        group.MapPost("/computers", async (AppDbContext db, Computer pc) => {
            var lab = await db.Labs.Include(l => l.Computers).FirstOrDefaultAsync(x => x.Id == pc.LabId);
            if (lab == null) return Results.BadRequest("Lab bulunamadı.");

            int pcCount = lab.Computers.Count + 1;
            pc.AssetCode = $"{lab.Name.Replace(" ", "").ToUpper()}-PC-{pcCount:D2}";

            db.Computers.Add(pc);
            await db.SaveChangesAsync();
            return Results.Ok(pc);
        });

        // 6. Öğrenci Atama ve Hesap Açma
        group.MapPost("/assign", async (AppDbContext db, AssignRequest req) => {
            var pc = await db.Computers.FindAsync(req.ComputerId);
            if (pc == null) return Results.NotFound();

            pc.AssignedStudentUsername = req.StudentNo;

            if (!await db.Users.AnyAsync(u => u.Username == req.StudentNo))
            {
                db.Users.Add(new User { Username = req.StudentNo, Password = req.StudentNo, Role = "Student" });
            }

            await db.SaveChangesAsync();
            return Results.Ok("Zimmetleme ve hesap başarılı!");
        });

        // 7. Öğrenci Portalı İçin
        group.MapGet("/my-pc/{username}", async (AppDbContext db, string username) => {
            var pc = await db.Computers.FirstOrDefaultAsync(p => p.AssignedStudentUsername == username);
            return pc != null ? Results.Ok(pc) : Results.NotFound();
        });
    }
}
public record AssignRequest(int ComputerId, string StudentNo);