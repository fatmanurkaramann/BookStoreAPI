using BookStore;
using BookStore.DbOperations;
using BookStore.Middleware;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
//{

//    string tokenIssuer = builder.Configuration["Token:Issuer"];
//    string tokenAudience = builder.Configuration["Token:Audience"];
//    string tokenSecurityKey = builder.Configuration["Token:SecuritKey"];

//    opt.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateAudience = true,
//        ValidateIssuer = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = tokenIssuer,
//        ValidAudience = tokenAudience,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecurityKey)),
//        ClockSkew = TimeSpan.Zero
//    };
//});

builder.Services.AddConfig(builder.Configuration);

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseInMemoryDatabase(databaseName:"BookStoreDb"));
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());
builder.Services.AddSingleton<ILoggerService,DbLogger>(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCustomExceptionMiddleware();
app.MapControllers();

app.Run();
