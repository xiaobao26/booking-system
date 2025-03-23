using booking_system.Data;
using booking_system.Mappings;
using booking_system.Repositories;
using booking_system.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Connection Database
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseNpgsql(builder.Configuration["DBConnection"]);
});
#endregion

#region AutoMapper dependency injection
builder.Services.AddAutoMapper(typeof(UserProfile));
#endregion

#region Service dependency injection
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion




builder.Services.AddControllers();


// Add services to the container.
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

app.MapControllers();

app.Run();

