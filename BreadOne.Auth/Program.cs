using BreadOne.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//서비스 추가
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();

app.UseAuthentication();

#region Menu
app.MapGet("/", async context =>
{
string content = "<h1>ASP.NET Core 인증과 권한 초간단 코드</h1>";
    //한글 출력 오류시
content += "<a href=\"/Login\">로그인</a><br />";
content += "<a href=\"/Login/User\">로그인(User)</a><br />";
content += "<a href=\"/Login/Administrator\">로그인(Administrator)</a><br />";
content += "<a href=\"/Info\">정보</a><br />";
content += "<a href=\"/InfoDetails\">정보(Details)</a><br />";
content += "<a href=\"/InfoJson\">정보(JSON)</a><br />";
content += "<a href=\"/Logout\">로그아웃</a><br />";
context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync(content);
}); 
#endregion

#region /Login/{Username}
//라우트 토큰
app.MapGet("/Login/{Username}", async context =>
{
    var username = context.Request.RouteValues["Username"]!.ToString();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, username!),
        new Claim(ClaimTypes.Name, username!),
        new Claim(ClaimTypes.Email, username + "@a.com"),
        new Claim(ClaimTypes.Role, "Users"),
        new Claim("원하는 이름", "원하는 값"),
    };

    if (username == "Administrator")
    {
        claims.Add(new Claim(ClaimTypes.Role, "Administrators"));
    }


    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //로그인
    //인증 쿠키 생성됨
    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme, 
        claimsPrincipal,
        //웹 브라우저를 닫아도 쿠키를 살려놓을 건지 
        new AuthenticationProperties { IsPersistent = true });

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h3>로그인 완료</h3>");
}); 
#endregion

#region Login
app.MapGet("/Login", async context =>
{
var claims = new List<Claim>
{
        //new Claim(ClaimTypes.Name, "User Name")
        new Claim(ClaimTypes.Name, "아이디")
};
var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //로그인
    //인증 쿠키 생성됨
await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync("<h3>로그인 완료</h3>");
});
#endregion

#region InfoDetails
app.MapGet("/InfoDetails", async context =>
{
    string result = "";

    if (context.User.Identity?.IsAuthenticated ?? false)
    {
        result += $"<h3>로그인 이름: {context.User.Identity.Name}</h3>";
        foreach (var claim in context.User.Claims)
        {
            result += $"{claim.Type} = {claim.Value}<br />";
        }

        if (context.User.IsInRole("Administrators") && context.User.IsInRole("Users"))
        {
            result += $"<br />Administrators + Users 권한이 있습니다.<br />";
        }
    }
    else
    {
        result += "<h3>로그인 하지 않았습니다.</h3>";
    }

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync(result, Encoding.Default);
});
#endregion

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
await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync("<h3>로그아웃 완료</h3>");
}); 
#endregion

app.Run();
