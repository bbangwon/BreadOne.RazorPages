using BreadOne.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//서비스 추가
builder.Services.AddAuthentication("Cookies").AddCookie();
var app = builder.Build();

app.UseAuthentication();

app.MapGet("/", async context =>
{
    string content = "<h1>ASP.NET Core 인증과 권한 초간단 코드</h1>";
    //한글 출력 오류시
    content += "<a href=\"/Login\">로그인</a><br />";
    content += "<a href=\"/Info\">정보</a><br />";
    content += "<a href=\"/InfoJson\">정보(JSON)</a><br />";
    content += "<a href=\"/Logout\">로그아웃</a><br />";
    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync(content);
});

app.MapGet("/Login", async context =>
{
    var claims = new List<Claim>
    {
        //new Claim(ClaimTypes.Name, "User Name")
        new Claim(ClaimTypes.Name, "아이디")
    };
    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //로그인
    //인증 쿠키 생성됨
    await context.SignInAsync("Cookies", claimsPrincipal); 

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h3>로그인 완료</h3>");
});

#region Info
app.MapGet("/Info", async context =>
{
string result = "";

if (context.User.Identity?.IsAuthenticated ?? false)
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
#endregion

#region InfoJson
app.MapGet("/InfoJson", async context =>
{
    string json = "";

    if (context.User.Identity?.IsAuthenticated ?? false)
    {
        //json += "{ \"type\" : \"Name\", \"value\" : \"User Name\" }";
        var claims = context.User.Claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value });
        json += JsonSerializer.Serialize(
            claims, 
            new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
    }
    else
    {
        json += "{}";
    }

    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8";
    await context.Response.WriteAsync(json);
});
#endregion

#region 로그아웃
app.MapGet("/Logout", async context =>
{
await context.SignOutAsync();

context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync("<h3>로그아웃 완료</h3>");
}); 
#endregion

app.Run();
