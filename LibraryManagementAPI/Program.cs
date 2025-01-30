using LibraryManagementAPI.Database;
using LibraryManagementAPI.Models.BookRepository;
using LibraryManagementAPI.Models.TransactionRepository;
using LibraryManagementAPI.Models.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//MySQL Context
builder.Services.AddDbContext<LibraryContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("LibraryConnection"),
        new MySqlServerVersion(new Version(8,0,34)))
);

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "http://localhost/5218",
            ValidAudience = "http://localhost/5218",
            IssuerSigningKey = 
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YoureSecurityKeyHereThatIsVerVeryLong"))
        };
    });

//Add Life Cycle
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
