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

//Db connection service
builder.Services.AddDbContext<AdventureWorksLt2019Context>(option=>option.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019")));
builder.Services.AddControllers().AddJsonOptions(jsopt => jsopt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", opt => { });  //servizio di gestione autenticaizone

builder.Services.AddAuthorization(opt => opt.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuth").RequireAuthenticatedUser().Build()));  //policy autorizzazione per autenticazione
var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
