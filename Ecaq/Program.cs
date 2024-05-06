using AutoMapper;
using Ecaq.Client.Pages;
using Ecaq.Components;
using Ecaq.Components.Account;
using Ecaq.Controllers;
using Ecaq.Data;
using Ecaq.Helpers;
using Ecaq.Services.Interfaces;
using Ecaq.Services.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Transient); //// this will reset your model to its original value if you decided to cancel the operations (ie. browse and replace photo from fileman then cancel the form).

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

builder.Services.AddScoped<IEmailSender<ApplicationUser>, EmailSenderRepository>();
builder.Services.AddScoped<IEmailSenderApp, EmailSenderApp>();
builder.Services.AddScoped<IHomeBannerRepository, HomeBannerRepository>();
builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IEcaqCoreModelRepository, EcaqCoreModelRepository>();
builder.Services.AddScoped<IMemberModelRepository, MemberModelRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// also use for RoxyFileman to be able to create folder
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // make the session cookie Essential
    options.IdleTimeout = TimeSpan.FromMinutes(1); //You can set Time   
});
builder.Services.AddDistributedMemoryCache();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddServerSideBlazor().AddCircuitOptions(x => x.DetailedErrors = true);
}

builder.Services.AddAuthorization();
//builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["Cors:Origin1"]!, builder.Configuration["Cors:Origin2"]!,
                              builder.Configuration["Cors:Origin3"]!, builder.Configuration["Cors:Origin4"]!,
                              builder.Configuration["Cors:Origin5"]!)
                              .AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddFluentUIComponents();

var app = builder.Build();
// Get the Automapper, we can share this too
var mapper = app.Services.GetService<IMapper>();
if (mapper == null)
{
    throw new InvalidOperationException(
      "Mapper not found");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();

    //swagger/index.html
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSession(); // also use for RoxyFileman to be able to create folder
app.UseRouting();

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapControllers();


app.MapHomeBannerEndpoint();
app.MapAboutEndpoint();
app.MapEcaqCoreEndpoint();
app.MapGalleryEndpoint(app.Environment, mapper);
app.MapAllianceEndpoint();
app.MapMemberModelEndpoints();
app.MapBookModelEndpoints();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    //// Seeding
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration.");
}


app.Run();
