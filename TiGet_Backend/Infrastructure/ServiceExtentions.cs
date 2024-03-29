﻿using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class ServiceExtentions
    {
        public static void InfrastructureServiceConfiguration(this IServiceCollection services) 
        {

            // DI for context
            services.AddDbContext<ApplicationDbContext>();

            // unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // unit of service
            //services.AddScoped<IUnitOfSevice, UnitOfSevice>();

            // DI for All Services
            //services.AddScoped<IMessageAttachmentService, MessageAttachementtService>();
            //services.AddScoped<ITicketService, TicketService>();
            //services.AddScoped<IMessageService, MessageService>();
            //services.AddScoped<IFaqService, FaqService>();
            //services.AddScoped<IAuthService, AuthService>();


        }
    }
}
