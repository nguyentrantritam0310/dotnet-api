using dotnet_api.Services.Interfaces;
using dotnet_api.Services;
using dotnet_api.Mapping;
using System.Reflection;
using dotnet_api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using dotnet_api.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? "5244" : "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Configure file upload
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = int.MaxValue;
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).ConfigureWarnings(warnings => warnings.Ignore(
               RelationalEventId.PendingModelChangesWarning)));

// 👇 Thêm Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Register other services
builder.Services.AddScoped<IContractService, ContractService>();
///////////////////////////
builder.Services.AddScoped<IConstructionService, ConstructionService>();
builder.Services.AddScoped<IConstructionItemService, ConstructionItemService>();
builder.Services.AddScoped<IConstructionPlanService, ConstructionPlanService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IConstructionTemplateItemService, ConstructionTemplateItemService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IExportOrderService, ExportOrderService>();
builder.Services.AddScoped<IImportOrderService, ImportOrderService>();
builder.Services.AddScoped<IImportOrderEmployeeService, ImportOrderEmployeeService>();
builder.Services.AddScoped<IConstructionTaskService, ConstructionTaskService>();
builder.Services.AddScoped<IMaterialNormService, MaterialNormService>();
builder.Services.AddScoped<IMaterialPlanService, MaterialPlanService>();
builder.Services.AddScoped<IMaterial_ExportOrderService, Material_ExportOrderService>();
builder.Services.AddScoped<IMaterialTypeService, MaterialTypeService>();
builder.Services.AddScoped<IUnitofMeasurementService, UnitofMeasurementService>();
builder.Services.AddScoped<IWorkSubTypeVariantService, WorkSubTypeVariantService>();
builder.Services.AddScoped<IWorkShiftService, WorkShiftService>();
builder.Services.AddScoped<IAttendanceMachineService, AttendanceMachineService>();
builder.Services.AddScoped<IEmployeeRequestService, EmployeeRequestService>();
builder.Services.AddScoped<IPayrollAdjustmentService, PayrollAdjustmentService>();
builder.Services.AddScoped<IAdjustmentTypeService, AdjustmentTypeService>();
builder.Services.AddScoped<IAdjustmentItemService, AdjustmentItemService>();
builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<IOvertimeTypeService, OvertimeTypeService>();
builder.Services.AddScoped<IOvertimeFormService, OvertimeFormService>();
builder.Services.AddScoped<IFamilyRelationService, FamilyRelationService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IShiftAssignmentService, ShiftAssignmentService>();
//builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<AttendanceDataService>();

builder.Services.AddScoped<WeatherPredictionService>(); 

// Email Service
builder.Services.AddScoped<IEmailService, EmailService>();

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    
    options.User.RequireUniqueEmail = true;
    
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

// Register JwtService
builder.Services.AddScoped<JwtService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendDomains", policy =>
    {
        policy.WithOrigins(
            "https://nhansu.xaydungvipro.id.vn",
            "https://congtrinh.xaydungvipro.id.vn",
            "http://localhost:5173",
            "https://xaydungvipro.id.vn",
            "160.250.132.226"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // nếu có gửi cookie, auth...
    });
});

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontendDomains", policy =>
//    {
//        policy
//            .SetIsOriginAllowed(origin => true) // chấp tất cả domain (test tạm)
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials();
//    });
//});

builder.Services.AddScoped<DataInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseCors("AllowFrontendDomains");

app.UseAuthentication();
app.UseAuthorization();

// Cấu hình để phục vụ file tĩnh từ thư mục uploads
var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(); // Phục vụ file tĩnh từ wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/uploads"
}); // Phục vụ file tĩnh từ thư mục uploads

app.MapControllers();

app.Run();
