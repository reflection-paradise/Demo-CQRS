using Application.IMessageBus;
using Application.UseCases;
using Domain.Interfaces;
using Infrastructure.Consumers;
using Infrastructure.DbConnect;
using Infrastructure.MessageBus;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.AppStarts
{
    public static class DependencyInjectionContainers
    {
        public static void InstallService(this IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình URL lowercase
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            // SQL Server DbContext
            services.AddDbContext<EcommerceDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DBDefault"));
            });

            // MongoDB context
            services.AddSingleton<MongoDbECommerceContext>();

            // Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>(); 

            // Handler
            services.AddScoped<GetAllProductHandler>();
            services.AddScoped<CreateProductHandler>();

            //rabbitmq
            services.AddScoped<ProductCreatedConsumer>();
            services.AddHostedService<RabbitMQListener>();
            //connection RBMQ
            services.AddSingleton<RabbitMQConnectionManager>();

            // add mapper

            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

        }
    }
}
