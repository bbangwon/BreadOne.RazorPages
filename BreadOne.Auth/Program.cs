using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//서비스 추가
builder.Services.AddAuthentication("Cookies").AddCookie();
var app = builder.Build();

app.UseAuthentication();

app.MapGet("/", async context =>
{
    string content = "<h1>ASP.NET Core 인증과 권한 초간단 코드</h1>";
    //한글 출력 오류시
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

    //로그인
    //인증 쿠키 생성됨
    await context.SignInAsync("Cookies", claimsPrincipal); 

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h3>로그인 완료</h3>");
});

app.MapGet("/Info", async context =>
{
    string result = "";

    if(context.User.Identity?.IsAuthenticated ?? false)
    {
        result += $"<h3>로그인 이름: {context.User.Identity.Name}</h3>";
    }
    else
    {
        result += "<h3>로그인 하지 않았습니다.</h3>";
    }

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync(result, Encoding.Default);
});

app.Run();
