using Microsoft.EntityFrameworkCore;
using Shortify.Api;
using Shortify.Api.Entity;
using Shortify.Api.Models;
using Shortify.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ShortedUrlService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/short", async (ShortUrlRequest request,
    HttpRequest httpRequest,
    ApplicationDbContext db,
    ShortedUrlService shortedUrlService) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("Url is invalid.");
    }

    var code = await shortedUrlService.GenerateUniqueCode();

    var shortedUrl = new ShortUrl
    {
        Id = Guid.NewGuid(),
        OriginalUrl = request.Url,
        Code = code,
        ShortedUrl = $"{httpRequest.Scheme}://{httpRequest.Host}/api/{code}",
    };

    await db.ShortUrls.AddAsync(shortedUrl);
    await db.SaveChangesAsync();

    return Results.Ok(shortedUrl.ShortedUrl);


});

app.MapGet("api/{code}", async (string code,
    ApplicationDbContext db) =>
{
    var shortedUrl = await db.ShortUrls.FirstOrDefaultAsync(x => x.Code == code);

    if(shortedUrl is null)
    {
        return Results.NotFound();
    }
    return Results.Redirect(shortedUrl.OriginalUrl);
});


app.Run();

