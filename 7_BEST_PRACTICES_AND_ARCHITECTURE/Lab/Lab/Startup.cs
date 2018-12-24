using Forum;
using Forum.App;
using Forum.Data;
using Forum.Services;
using Forum.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lab
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);

            engine.Run();         
        }

        private static IServiceProvider ConfigureServices() //invoke services
        {
            var serviceCollection = new ServiceCollection(); // make colection of services

            serviceCollection.AddDbContext<ForumDbContext>(o => o.UseSqlServer(Configuration.ConnectionString));
            //bei request DbContext - find it and inject into it:

            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            // returns Service from I...Service
            serviceCollection.AddTransient<IUserService, UserService>(); 

            serviceCollection.AddTransient<IPostService, PostService>();

            serviceCollection.AddTransient<ICategoryService, CategoryService>();

            serviceCollection.AddTransient<IReplyService, ReplyService>();

            var serviceProvider = serviceCollection.BuildServiceProvider(); //make serviceProvider

            return serviceProvider;
        }
    }
}
