using LabYonetimSistemi.Models;
using LabYonetimSistemi.Endpoints; // Yeni klasörün
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVİS KAYITLARI ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- 2. ARA YAZILIMLAR ---
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

// --- 3. MODÜLLERİN KAYDI (YENİ YAPI) ---
// Bu metodlar Endpoints klasöründeki static sınıflardan geliyor.
app.MapAuthEndpoints();
app.MapStatEndpoints();
app.MapLabEndpoints();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // VERİTABANINI VE TABLOLARI OTOMATİK OLUŞTUR (YENİ EKLE)
    context.Database.EnsureCreated();

    // Eğer Users tablosu boşsa veya admin yoksa ekle
    if (!context.Users.Any(u => u.Username == "admin"))
    {
        context.Users.Add(new User
        {
            Username = "admin",
            Password = "1234",
            Role = "Admin"
        });
        context.SaveChanges();
    }
}
// --- SEEDING BİTTİ ---

app.Run();