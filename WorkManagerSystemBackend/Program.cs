using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorkManagerSystemBackend.Core.AutoMapperConfig;
using WorkManagerSystemBackend.Core.Context;
using WorkManagerSystemBackend.Core.Helpers;

var builder = WebApplication.CreateBuilder(args);

//Cors
builder.Services.AddCors();

// DB Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("local"));

});

// Automapper Configuration
builder.Services.AddAutoMapper(typeof(AutoMapperConfigProfile));


builder.Services.AddControllers();
builder.Services.AddScoped<JwtService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => options
    .WithOrigins(new[] {"http://localhost:3000", "https://localhost:7054", "http://localhost:5221" })
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
