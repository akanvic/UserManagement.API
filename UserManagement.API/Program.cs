using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using UserMgt.Core;
using UserMgt.Core.Config;
using UserMgt.Core.Entities.Response;
using UserMgt.Repo;
using UserMgt.Repo.Repositories.EntityRepository.Implementation;
using UserMgt.Repo.Repositories.EntityRepository.Interface;
using UserMgt.Service.Implementation;
using UserMgt.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var validationProblemDetails = new ServiceResponse()
        {
            Message = "One or more Validation errors occured",
            StatusCode = HttpStatusCode.BadRequest,
            Data = context.ModelState.ToDictionary(
                c => c.Key, c => c.Value?.Errors.Select(e => e.ErrorMessage).ToArray())
        };



        return new BadRequestObjectResult(validationProblemDetails);
    };
});

// Configure JWT authentication
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var config = builder.Configuration.GetSection("JwtSettings");
JwtSettings jwtSettings = config.Get<JwtSettings>();

var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidIssuer = jwtSettings.Issuer,
        ValidAudiences = new List<string>
        {
            jwtSettings.Audience1!
        }
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            // Customize the error response message
            var serviceResponse = new ServiceResponse { StatusCode=HttpStatusCode.Unauthorized, Message= "You are not authorized to access this resource." };
            //var message = "You are not authorized to access this resource.";
            var result = JsonSerializer.Serialize(serviceResponse);
            return context.Response.WriteAsync(result);
        }
    };
});


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "User Management API",
        Version = "v1",
        Description = "Description of your API",
        Contact = new OpenApiContact
        {
            Name = "Akaninyene Uwah",
            Email = "uwahakan@gmail.com"
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                      {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                      });
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
