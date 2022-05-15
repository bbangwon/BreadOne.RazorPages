var builder = WebApplication.CreateBuilder(args);

//Add Services to the container
builder.Services.AddRazorPages();

var app = builder.Build();

//Add Endpoints for RazorPages
app.MapRazorPages();

app.MapGet("/", () => "Hello World!!!");

app.Run();
