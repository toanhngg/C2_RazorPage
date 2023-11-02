using C2_RazorPage.Hubs;
using C2_RazorPage.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PE_PRN_Fall22B1Context>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));
builder.Services.AddScoped<PE_PRN_Fall22B1Context>();
builder.Services.AddSignalR();
// add signalR
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
app.MapHub<MovieHub>("/movieHub");

app.Run();
