using Events.API.Extensions;
using Events.Application.Repositories.Interfaces;
using Events.Application.Services.Implementations;
using Events.Application.Services.Interfaces;
using Events.Infrastructure.Context;
using Events.Infrastructure.Repositories.Implementations;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddDbContext<EventsDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions()
{
	MinimumSameSitePolicy = SameSiteMode.Strict,
	HttpOnly = HttpOnlyPolicy.Always,
	Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
