using Microsoft.EntityFrameworkCore;
using MyApp.Data;

var builder = WebApplication.CreateBuilder(args); //It intializes a new instance of the web application of the builder class which sets up the configuration services and the web server

// Add services to the dependency injection container.
builder.Services.AddControllersWithViews(); //Adding MVC services to the container with support for both the controllers and views. It allows our application to handle incoming HTTP requests and return responses to the client.
builder.Services.AddDbContext<MyAppContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//Build the application
var app = builder.Build(); //This line compiles the app creating the application instance which you can configure and run

// Configure the HTTP request pipeline. It determines how the app responds to HTTP requests and how requests are processed by the app. If the app is not in the development environment we set up an exception handler to redirect users to the home/error page. 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); //When unhandled execption occurs, we enable HTTp Strict Transport Security (HSTS) to protect the app and enforce secure connections.
}

app.UseHttpsRedirection(); //We also enable HTTPS redirection to redirect HTTP requests to HTTPS.
app.UseRouting(); //We enable routing to route the incoming requests to the appropriate controller and action. We also map the static assets to the app.

app.UseAuthorization(); //responsible for authorizing the user to access the app. It is used to protect the app from unauthorized access. 

app.MapStaticAssets(); //We map the static assets to the app. Enables serving static files (e.g., images, css, js, etc) from the www route folder and directory browsing.

app.MapControllerRoute( //This sets up the default route for the app. It maps the home controller and its index action method by default.
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}") //where id parameter is optional. This is the default route for the app that will be redirected to when we start our application.
    .WithStaticAssets();


app.Run();
