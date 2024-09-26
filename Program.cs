using Microsoft.EntityFrameworkCore;
using ShortalkB2.Hubs;
using ShortalkB2.Service;
using ShortalkB2.Service.Context;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Register your DbContext with SQLite
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=localdb.db"));

// Add services to the container.
builder.Services.AddScoped<GameService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                    .AllowCredentials();
        });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseAuthorization(); // Make sure to include this line

app.MapControllers(); // Ensure this is present to map controller routes
app.MapHub<GameHub>("/Game");

app.Run();
