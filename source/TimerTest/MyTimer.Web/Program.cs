using Microsoft.EntityFrameworkCore;
using MyTimer.Data;
using MyTimer.Data.Contexts;
using MyTimer.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<ITimerRepository, TimerRepository>();

builder.Services.AddDbContext<MtDbContext>(options =>
	options.UseSqlServer(Connection.Text), ServiceLifetime.Scoped);


builder.Services.AddHostedService<SyncDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();



/*
 builder.Services.AddSingleton<IAgRepository, AgRepository>();
builder.Services.AddDbContext<AgDbContext>(options =>
		options.UseSqlServer(Connection.Text), ServiceLifetime.Singleton);

 */
