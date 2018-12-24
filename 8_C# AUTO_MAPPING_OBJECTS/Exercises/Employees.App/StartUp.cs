using System;
using Microsoft.Extensions.DependencyInjection;
using Employees.Data;
using Microsoft.EntityFrameworkCore;
using Employees.Services;
using AutoMapper;

namespace Employees.App
{
    class StartUp
    {
        static void Main()
        {
            var serviceProvider = ConfigyreServices();

            InitializeAutomapper();

            var engine = new Engine(serviceProvider);

            engine.Run();
        }

        private static void InitializeAutomapper()
        {
            Mapper.Initialize(c => c.AddProfile<AutoMapperProfile>());
        }

        private static IServiceProvider ConfigyreServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<EmployeeService>();

            serviceCollection.AddTransient<ManagerService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
