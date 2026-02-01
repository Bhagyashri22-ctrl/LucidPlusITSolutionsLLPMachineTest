using MachineTest2026.DomainModel;
using MachineTest2026.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddDbContext<CompanyContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("scon")));
builder.Services.AddScoped<ICommonService,CommonService>();
builder.Services.AddScoped<IEmpService, EmpService>();




var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
//app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EmpMVC}/{action=SignIn}/{id?}"
);
app.Run();

