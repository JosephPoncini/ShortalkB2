using Microsoft.EntityFrameworkCore;
using ShortalkB2.Service;
using ShortalkB2.Service.Context;

var builder = WebApplication.CreateBuilder(args);

// Register your DbContext with SQLite
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite("Data Source=localdb.db"));

// Add services to the container.
builder.Services.AddScoped<LobbyService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "https://yourfrontend.com")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Use an empty string if you want Swagger at the root
    });
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseAuthorization(); // Make sure to include this line

app.MapControllers(); // Ensure this is present to map controller routes

app.Run();
