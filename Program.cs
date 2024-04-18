using demo.WebApiDotNetCore8.Minimal.Data;
using demo.WebApiDotNetCore8.Minimal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));
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


async Task<List<Court>> GetCourts(DataContext context) => await context.Courts.ToListAsync();
app.MapPost("add/court", async (DataContext context, Court court) =>
{
   context.Courts.Add(court);
   await context.SaveChangesAsync();
   return Results.Ok(await GetCourts(context));
});

app.MapGet("/courts", async (DataContext context) => await context.Courts.ToListAsync());


app.Run();

