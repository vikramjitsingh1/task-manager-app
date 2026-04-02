using Microsoft.EntityFrameworkCore;
using TmModule.Infrastructure;
using TmModule.Modules.Users.Repositories;
using TmModule.Modules.Tasks.Repositories;
using TmModule.Modules.Status.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllersWithViews();

// Add CORS
builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()));

// Configure the database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// User services and repositories registration
builder.Services.AddScoped<IRepositoryUsers, RepositoryUsers>();
builder.Services.AddScoped<TmModule.Modules.Users.Services.UsersService>();

// Task services and repositories registration
builder.Services.AddScoped<IRepositoryTask, RepositoryTask>();
builder.Services.AddScoped<TmModule.Modules.Tasks.Services.TaskService>();

// Status services and repositories registration
builder.Services.AddScoped<IRepositoryStatus, RepositoryStatus>();
builder.Services.AddScoped<TmModule.Modules.Status.Services.StatusUpdateService>();

// Application Core Services registration
builder.Services.AddScoped<TmModule.Application.UseCases.CreateUserTask>();
builder.Services.AddScoped<TmModule.Application.UseCases.AssignTaskToUser>();
builder.Services.AddScoped<TmModule.Application.UseCases.DetailedView>();

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(); // MUST be after UseRouting
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
