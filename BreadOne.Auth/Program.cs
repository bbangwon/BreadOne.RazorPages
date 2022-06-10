using BreadOne.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
//���� �߰�
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();

app.UseAuthentication();

#region Menu
app.MapGet("/", async context =>
{
string content = "<h1>ASP.NET Core ������ ���� �ʰ��� �ڵ�</h1>";
    //�ѱ� ��� ������
content += "<a href=\"/Login\">�α���</a><br />";
content += "<a href=\"/Login/User\">�α���(User)</a><br />";
content += "<a href=\"/Login/Administrator\">�α���(Administrator)</a><br />";
content += "<a href=\"/Info\">����</a><br />";
content += "<a href=\"/InfoDetails\">����(Details)</a><br />";
content += "<a href=\"/InfoJson\">����(JSON)</a><br />";
content += "<a href=\"/Logout\">�α׾ƿ�</a><br />";
context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync(content);
}); 
#endregion

#region /Login/{Username}
//���Ʈ ��ū
app.MapGet("/Login/{Username}", async context =>
{
    var username = context.Request.RouteValues["Username"]!.ToString();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, username!),
        new Claim(ClaimTypes.Name, username!),
        new Claim(ClaimTypes.Email, username + "@a.com"),
        new Claim(ClaimTypes.Role, "Users"),
        new Claim("���ϴ� �̸�", "���ϴ� ��"),
    };

    if (username == "Administrator")
    {
        claims.Add(new Claim(ClaimTypes.Role, "Administrators"));
    }


    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //�α���
    //���� ��Ű ������
    await context.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme, 
        claimsPrincipal,
        //�� �������� �ݾƵ� ��Ű�� ������� ���� 
        new AuthenticationProperties { IsPersistent = true });

    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
    await context.Response.WriteAsync("<h3>�α��� �Ϸ�</h3>");
}); 
#endregion

#region Login
app.MapGet("/Login", async context =>
{
var claims = new List<Claim>
{
        //new Claim(ClaimTypes.Name, "User Name")
        new Claim(ClaimTypes.Name, "���̵�")
};
var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    //�α���
    //���� ��Ű ������
await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync("<h3>�α��� �Ϸ�</h3>");
});
#endregion

#region InfoDetails
app.MapGet("/InfoDetails", async context =>
{
    string result = "";

    if (context.User.Identity?.IsAuthenticated ?? false)
    {
        result += $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
        foreach (var claim in context.User.Claims)
        {
            result += $"{claim.Type} = {claim.Value}<br />";
        }

        if (context.User.IsInRole("Administrators") && context.User.IsInRole("Users"))
        {
            result += $"<br />Administrators + Users ������ �ֽ��ϴ�.<br />";
        }
    }
    else
    {
        result += "<h3>�α��� ���� �ʾҽ��ϴ�.</h3>";
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
result += $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
}
else
{
result += "<h3>�α��� ���� �ʾҽ��ϴ�.</h3>";
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

#region �α׾ƿ�
app.MapGet("/Logout", async context =>
{
await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
await context.Response.WriteAsync("<h3>�α׾ƿ� �Ϸ�</h3>");
}); 
#endregion

app.Run();
