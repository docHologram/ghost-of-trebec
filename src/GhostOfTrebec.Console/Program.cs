using GhostOfTrebec.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using GhostOfTrebec.ConsoleApp;
using GhostOfTrebec.Core;
using GhostOfTrebec.Core.InnerCore;

var hostBuilder = CreateHostBuilder();

await hostBuilder.RunConsoleAsync();

static IHostBuilder CreateHostBuilder() =>
    Host.CreateDefaultBuilder()
        .ConfigureServices((IServiceCollection services) =>
        {
            services.AddDbContext<TriviaDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=GhostOfTrebec"));

            services.AddRepository<Problem, TriviaDbContext>();
            services.AddMediatR(typeof(Problem), typeof(TriviaDbContext));
            services.AddSingleton<IHostedService, AddNewQuestions>();
        });