using BookStore.DbOperations;
using BookStore.Middleware;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseInMemoryDatabase(databaseName:"BookStoreDb"));
builder.Services.AddSingleton<ILoggerService,DbLogger>(); 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCustomExceptionMiddleware();
app.MapControllers();

app.Run();
