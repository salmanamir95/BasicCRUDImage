using ImageManipulation.Data.Models;
using ImageManipulation.Data.Repositories;
using ImageManipulation.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("default"))
    );


builder.Services.AddScoped<ApplicationDBContext>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddCors(
    options =>
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://trusted-origin.com").AllowAnyMethod().AllowAnyHeader();

        }
    )
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
        RequestPath = "/Uploads" // Matches the directory name
    });

app.UseCors();

app.UseAuthorization();


app.MapControllers();

app.Run();
