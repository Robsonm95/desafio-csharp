using FluentValidation;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Questao5.Application.Members.Commands.Validations;
using Questao5.Domain.Abstractions;
using Questao5.Infrastructure.Repositories;
using Questao5.Infrastructure.Sqlite;
using System.Data;
using System.Reflection;


namespace Questao5.CrossCutting.AppDependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {

        var databaseConfig = new DatabaseConfig { Name = configuration.GetConnectionString("DefaultConnection")};
        
        services.AddSingleton(databaseConfig);
        services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
        
        services.AddSingleton<IDbConnection>(provider =>
        {
            var connection = new SqliteConnection(databaseConfig.Name);
            connection.Open();
            return connection;
        });

        services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();

        var myhandlers = AppDomain.CurrentDomain.Load("Questao5.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(myhandlers);
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.Load("Questao5.Application"));

        return services;
    }
}
