using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)
));

//builder.Services.Configure<JsonOptions>(options => options.Converters.Add(new DateOnlyConverter()));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IDaodienRepository, DaodienRepository>();
builder.Services.AddScoped<IDienvienRepository, DienvienRepository>();
builder.Services.AddScoped<IBinhluanRepository, BinhluanRepository>();
builder.Services.AddScoped<IGheRepository, GheRepository>();
builder.Services.AddScoped<IKhachhangRepository, KhachhangRepository>();
builder.Services.AddScoped<IKhuyenmaiRepository, KhuyenmaiRepository>();
builder.Services.AddScoped<IKichRepository, KichRepository>();
builder.Services.AddScoped<IKichDienvienRepository, KichDienvienRepository>();
builder.Services.AddScoped<ILichchieuRepository, LichchieuRepository>();
builder.Services.AddScoped<INhakichRepository, NhakichRepository>();
builder.Services.AddScoped<ITaikhoanRepository, TaikhoanRepository>();
builder.Services.AddScoped<IVeRepository, VeRepository>();
builder.Services.AddScoped<IXuatchieuRepository, XuatchieuRepository>();

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
