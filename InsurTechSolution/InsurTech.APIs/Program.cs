
using InsurTech.Core.Repositories;
using InsurTech.Core;
using InsurTech.Repository.Data;
using InsurTech.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using InsurTech.APIs.Errors;
using InsurTech.APIs.Middlewares;
using InsurTech.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using InsurTech.Core.Service;
using InsurTech.Service;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

namespace InsurTech.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsurTech API", Version = "v1" });
            });

            builder.Services.AddDbContext<InsurtechContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                
            })
            .AddEntityFrameworkStores<InsurtechContext>()
            .AddDefaultTokenProviders();


            #region Reset Password
            //Reset Password
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(10));
            #endregion

            #region Login By Gooogle + JWT

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer()
                            .AddGoogle(option =>
                            {
                                option.ClientId = "30498991812-uog175jdj3vb9foj41sv9g2l88teu11n.apps.googleusercontent.com";
                                option.ClientSecret = "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";
                            });

            #endregion


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            #region Validation Error Handling
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                            .SelectMany(p => p.Value.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToArray();
                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            #endregion

            #region Login by Google in Api
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder
                        .WithOrigins()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            #endregion

            //======================================================
            //======================================================

            var app = builder.Build();
            app.UseCors("AllowMyOrigin");
            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleWare>();
            if (app.Environment.IsDevelopment())
            {
                
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
