using Betacomio_Project.ConnectDb;
using Betacomio_Project.Controllers;
using Betacomio_Project.LogModels;

using Betacomio_Project.NewModels;
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

//Db connection >> DB VECCHIO


//DB PRINCIPALE
builder.Services.AddDbContext<BetacomioCyclesContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("BetacomioCycles")));

//DB ISOLATO (error log, tabelle isolate per prove) 
builder.Services.AddDbContext<AdminLogContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("AdminLog")));


var strinConn = builder.Configuration.GetConnectionString("AdminLog");
builder.Services.AddSingleton<SingleTonConnectDB>(option => new SingleTonConnectDB(strinConn));
builder.Services.AddControllers().AddJsonOptions(jsopt => jsopt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuth", opt => { });  //servizio di gestione autenticaizone

builder.Services.AddAuthorization(opt => opt.AddPolicy("BasicAuthentication", new AuthorizationPolicyBuilder("BasicAuth").RequireAuthenticatedUser().Build()));  //policy autorizzazione per autenticazione
builder.Services.AddCors(opt => { opt.AddDefaultPolicy(build => build.WithOrigins("http://localhost:4200", "https://localhost:7284").AllowAnyHeader().AllowAnyMethod().AllowCredentials()); });

builder.Services.AddDistributedMemoryCache(); //utilizzo del servizio della session

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseSession(); // avvio metodo session 
app.MapControllers();
app.UseCors();
app.Run();
