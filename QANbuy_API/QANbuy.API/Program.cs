using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QANbuy.Entity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("any", builder =>
    {
        builder.WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                //.AllowCredentials()//ָ������cookie
                .AllowAnyOrigin().AllowAnyHeader(); //�����κ���Դ����������
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,//�Ƿ���֤Issuer
        ValidateAudience = true,//�Ƿ���֤Audience
        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
        ClockSkew = TimeSpan.FromSeconds(60 * 60 * 24 * 7),
        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
        ValidAudience = "QANstar",//Audience
        ValidIssuer = "QANstar",//Issuer���������ǰ��ǩ��jwt������һ��
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QANstarAndSuoMi1931"))//�õ�SecurityKey
    };
});
builder.Services.AddDbContext<Qanbuy_DBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("any");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();