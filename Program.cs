
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BookStore_Web_APIContext")
    ?? throw new InvalidOperationException("Connection string 'BookStore_Web_APIContext' not found.");

builder.Services.AddDbContext<BookStore_Web_APIContext>(
    options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS for Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// Enable CORS
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
