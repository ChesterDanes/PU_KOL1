using DAL;
using Microsoft.EntityFrameworkCore;
using BLL.ServiceInterfaces;
using BLL_EF.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PUKolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IGrupaService, GrupaService>();
builder.Services.AddScoped<IHistoriaService, HistoriaService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
