using api_docmanager.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load("../../.env");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddOpenApi();

//Add DbContext as a service using the connection string
builder.Services.AddDbContext<DocManagerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(corsOptions =>
    {
        corsOptions.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();