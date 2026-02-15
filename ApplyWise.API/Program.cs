using ApplyWise.Application.Interfaces;
using ApplyWise.Application.Services;
using ApplyWise.Domain.Interfaces;
using ApplyWise.Infrastructure.Persistence;
using ApplyWise.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Configurar o SQL Server (DbContext)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Registrar nossos contratos (DI)
// "Quando pedirem X, entregue Y"
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Permite qualquer site
              .AllowAnyMethod()   // Permite GET, POST, PUT, DELETE, etc.
              .AllowAnyHeader();  // Permite qualquer cabeçalho (JSON, Auth, etc.)
    });
});

var app = builder.Build();

app.UseCors("AllowAll"); // Ativa a regra que criamos acima

// ... outros middlewares (HTTPS, Auth, etc)
app.MapControllers();

// --- SEÇÃO DE MIDDLEWARES (Pipeline de Execução) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class FakeCurrentUserService : ICurrentUserService
{
    public Guid UserId => Guid.Parse("3dd4204e-0398-4b00-a168-279b01eeba83");

    public string Email => "maercio10@gmail.com";

    public bool IsAuthenticated => true;
}