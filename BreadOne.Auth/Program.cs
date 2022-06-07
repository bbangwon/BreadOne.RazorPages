using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//���� �߰�
builder.Services.AddAuthentication("Cookies").AddCookie();
var app = builder.Build();

app.UseAuthentication();

app.MapGet("/", async context =>
{
    string content = "<h1>ASP.NET Core ������ ���� �ʰ��� �ڵ�</h1>";
    //�ѱ� ��� ������
    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync(content);
});

app.MapGet("/Login", async context =>
{
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "User Name")
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //�α���
    //���� ��Ű ������
    await context.SignInAsync("Cookies", claimsPrincipal); 

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h3>�α��� �Ϸ�</h3>");
});

app.MapGet("/Info", async context =>
{
    string result = "";

    if(context.User.Identity?.IsAuthenticated ?? false)
    {
        result += $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
    }
    else
    {
        result += "<h3>�α��� ���� �ʾҽ��ϴ�.</h3>";
    }

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync(result, Encoding.Default);
});

app.Run();
