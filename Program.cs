using Microsoft.EntityFrameworkCore;
using ShortalkB2.Hubs;
using ShortalkB2.Service;
using ShortalkB2.Service.Context;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddScoped<GameService>();

//this is how we’re connecting our database to API
var connectionString = builder.Configuration.GetConnectionString("ShortalkConnectionString");

//configures entity framework core to use SQL server as the database provider for  a datacontext DbContext in our project
builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));

// Add services to the container.



//"http://localhost:5051/", "http://localhost:3000/", "https://shortalkv2.vercel.app/","*"


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "https://shortalkv2.vercel.app")
                                                .AllowAnyHeader()
                                                .AllowAnyMethod()
                                                .AllowCredentials();

                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<DataContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization(); // Make sure to include this line

app.MapControllers(); // Ensure this is present to map controller routes
app.MapHub<GameHub>("/Game"); 

app.Run();
