using Mango.Web.Service;
using Mango.Web.Utility;
using Mango.Web.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient(); //the main goal here is to add the httpclient

//once its here then we can configure it in the couponservice
builder.Services.AddHttpClient<ICouponService, CouponService>();
SD.CouponAPIBase = builder.Configuration["ServiceUrls:coupon"];

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddTransient<ICouponService, CouponService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
