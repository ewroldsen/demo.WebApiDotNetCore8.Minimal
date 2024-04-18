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

/// Get courts
app.MapGet("/courts", async (DataContext context) => await context.Courts.ToListAsync())
    .WithName("getCourt")
   .WithOpenApi();

/// Get courts by id
app.MapGet("/courts/{id}", async (DataContext context, int id) => await context.Courts.FindAsync(id) is Court court ? Results.Ok(court) : Results.NotFound("Court not found"))
    .WithName("getCourtbyid")
   .WithOpenApi();

/// Add courts
app.MapPost("add/court", async (DataContext context, Court court) =>
{
   context.Courts.Add(court);
   await context.SaveChangesAsync();
   return Results.Ok(await GetCourts(context));
})
   .WithName("AddCourt")
   .WithOpenApi();

/// Update court
app.MapPut("update/court{id}", async (DataContext context, Court court, int id) =>
{
   var courtToUpdate = await context.Courts.FindAsync(id);
   if (courtToUpdate == null) return Results.NotFound("Court not found");
   courtToUpdate.Id = id;
   courtToUpdate.CourtType = court.CourtType;
   courtToUpdate.NumbOfCourts = court.NumbOfCourts;
   courtToUpdate.Title = court.Title;
   courtToUpdate.StreetAddress = court.StreetAddress;
   courtToUpdate.City = court.City;
   courtToUpdate.StateAbrev = court.StateAbrev;
   courtToUpdate.ZipCode = court.ZipCode;
   courtToUpdate.Latitude = court.Latitude;
   courtToUpdate.Longitude = court.Longitude;
   context.Courts.Update(courtToUpdate);
   await context.SaveChangesAsync();
   return Results.Ok(await GetCourts(context));
})
    .WithName("updateCourt")
   .WithOpenApi();

/// Delete court

app.MapDelete("delete/court{id}", async (DataContext context, int id) =>
{
   var courtToRemove = await context.Courts.FindAsync(id);
   if (courtToRemove == null) return Results.NotFound("Court not found");

   context.Remove(courtToRemove);
   await context.SaveChangesAsync();
   return Results.Ok(await GetCourts(context));
})
    .WithName("Delete-Court")
   .WithOpenApi();




app.Run();

