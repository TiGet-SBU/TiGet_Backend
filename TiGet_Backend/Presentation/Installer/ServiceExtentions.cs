﻿using Application;
using Application.Interfaces.Services;
using FluentAssertions.Common;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace Presentation.Installer
{
    public static class ServiceExtentions
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.ApplicationServiceConfiguration();
            builder.Services.InfrastructureServiceConfiguration();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            builder.Services.RegisterControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwaggerGen();
            builder.Services.RegisterJwtBarear(builder.Configuration);

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            return builder;
        }

        private static void RegisterControllers(this IServiceCollection service)
        {
            service.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

        }

        private static void RegisterSwaggerGen(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                });
        }


        private static void RegisterJwtBarear(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddAuthentication()
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!)
                                )
                        };
                    });
        }
    }
}
