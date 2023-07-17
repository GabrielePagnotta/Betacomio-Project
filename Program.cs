using Betacomio_Project.Controllers;
using Betacomio_Project.Models;
using FirstMVC.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Db connection service
builder.Services.AddDbContext<AdventureWorksLt2019Context>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019")));
builder.Services.AddControllers().AddJsonOptions(jsopt => jsopt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", opt => { });  //servizio di gestione autenticaizone

builder.Services.AddAuthorization(opt => opt.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuth").RequireAuthenticatedUser().Build()));  //policy autorizzazione per autenticazione
builder.Services.AddCors(opt => { opt.AddDefaultPolicy(build => build.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyMethod());});
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
app.UseCors();
app.Run();
