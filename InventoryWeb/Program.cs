using Inventory.DataAccess.Data;
using InventoryWeb;
using InventoryWeb.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddServiceRegistration(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/StatusCode", "?code={0}");

//app.UseDeveloperExceptionPage(); -- NOTE: NO NEED FOR THIS. This is builtin when environment is Development

//app.UseMiddleware<ExceptionHandlerMiddleware>(); -- NOTE: OPTION if we want a custom exception handler middleware. If not, you can use the UseExceptionHandler. This will catch error from either api request or mvc request
//app.UseMiddleware<ApiExceptionMiddleware>(); -- NOTE: OPTION. This will catch the error if API request only

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
