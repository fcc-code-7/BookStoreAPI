using BookStoreAPI.Data;
using BookStoreAPI.Mapper;
using BookStoreAPI.Models.Domain;
using BookStoreAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(MapperProfiles));
builder.Services.AddDbContext<BookStoreDbContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookStoreAPI"));
});
//builder.Services.AddIdentityCore<User>()
//    .AddRoles<IdentityRole>()
//    .AddTokenProvider<DataProtectorTokenProvider<User>>("BookStoreAPI")
//    .AddEntityFrameworkStores<BookStoreDbContext>()
//    .AddDefaultTokenProviders();
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
