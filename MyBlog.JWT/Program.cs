using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyBlog.WebApi;
using MyBlog.WebApi.AutoMapper;
using MyBlog.WebApi.Filter;
using SqlSugar.IOC;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.JWT", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
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
            new string[] { }
        }
    });
});

// Add SqlSugar
builder.Services.AddSqlSugar(new IocConfig
{
    ConnectionString = builder.Configuration["SqlConn"],
    DbType = IocDbType.MySql,
    IsAutoCloseConnection = true //自动释放
});

// Add IOC dependency injection
builder.Services.AddCustomIOC();

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VFF")),
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:6060",
            ValidateAudience = true,
            ValidAudience = "http://localhost:5034",
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(60)
        };
    });


//Add AutoMapper
builder.Services.AddAutoMapper(typeof(CustomAutoMapperProfile));

builder.Services.AddMemoryCache();

builder.Services.AddScoped<DeleteFilter>();

// Add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
        policy.RequireAuthenticatedUser());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog.JWT v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();