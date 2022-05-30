using BreadOne.Services;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<PortfolioServiceJsonFile>();

var app = builder.Build();

//app.UseDefaultFiles();
app.UseStaticFiles();   //������ HTML, CSS, JavaScript,... ����

//Add Endpoints for RazorPages
app.MapRazorPages();
app.MapBlazorHub();

//app.MapGet("/", () => "Hello World!!!");



app.Run();
