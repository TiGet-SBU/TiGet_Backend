using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ServiceExtentions
    {
        public static void ApplicationServiceConfiguration(this IServiceCollection services)
        {
            // Fluent Validation
            //services.AddFluentValidationAutoValidation();
            //services.AddFluentValidationClientsideAdapters();
            //services.AddValidatorsFromAssembly(typeof(LoginRequest).Assembly);

            // HttpContextAccessor
            //services.AddHttpContextAccessor();

            // AutoMapper
            //services.AddAutoMapper(typeof(SignupMapper));
            //services.AddControllersWithViews();
        }
    }
}
