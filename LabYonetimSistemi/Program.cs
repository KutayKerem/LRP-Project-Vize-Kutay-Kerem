
var builder = WebApplication.CreateBuilder(args); //Bu satýr ile web uygulamasýný oluþturuyoruz.
var app = builder.Build(); //web uygulamasý oluþtu adýda app

app.MapGet("/hosgeldiniz", () => "Laboratuvar Takip Sistemine Hoþ Geldiniz!");

app.Run();
