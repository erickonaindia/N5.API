
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using N5.Domain.Repository;
using N5.Infrastructure.Persistence;
using N5.Infrastructure.Repositories;
using N5.Infrastructure.Repository;
using System.Reflection;
using MediatR;
using System.Net.NetworkInformation;
using N5.Application.Queries.Permission;
using N5.Application.Queries.PermissionTypes;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using N5.Infrastructure.Messaging;
using N5.Domain.Messaging;
using N5.Infrastructure.ElasticSearch;
using N5.Application.Commands.Permission.Request;
using N5.Domain.ElasticSearch;

namespace N5.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var strconn = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(strconn));
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IPermissionQueries, PermissionQueries>();
            builder.Services.AddScoped<IPermissionTypesQueries, PermissionTypesQueries>();
            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)
                .RegisterServicesFromAssembly(typeof(RequestPermissionCommand).Assembly)
            );

            builder.Services.Configure<KafkaConfig>(builder.Configuration.GetSection("Kafka"));
            builder.Services.AddScoped<KafkaProducer>();
            builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
            builder.Services.AddScoped<IElasticSearchRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var elasticSearchUri = new Uri(configuration["ElasticSearch:Uri"]!);
                return new ElasticSearchRepository(elasticSearchUri);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

            builder.Services.AddControllers();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowReactApp");

            app.MapControllers();

            app.Run();
        }
    }
    
}
