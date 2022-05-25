var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

//app.UseDefaultFiles();
app.UseStaticFiles();   //정적인 HTML, CSS, JavaScript,... 실행

//Add Endpoints for RazorPages
app.MapRazorPages();

//app.MapGet("/", () => "Hello World!!!");



app.Run();
