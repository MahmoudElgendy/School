using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using School.Api.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
if (builder.Environment.IsProduction())
{
    var vaultUri = new Uri(builder.Configuration["Vault:Uri"]);
    if (vaultUri != null)
    {
        builder.Configuration.AddAzureKeyVault(vaultUri, new DefaultAzureCredential());
    }

}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await db.Database.MigrateAsync();
    await StudentSeeder.SeedAsync(db);
}

app.Run();

