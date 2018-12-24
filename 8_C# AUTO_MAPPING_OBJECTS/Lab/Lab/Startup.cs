using AutoMapper;
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

            InitializeAutomapper();

            var engine = new Engine(serviceProvider);

            engine.Run();
            //Manuelmapper - without classForumProfile
            //---------------------------------------------------
            //Mapper.Initialize(c =>
            //    {
            //        c.CreateMap<Post, PostDetailsDto>()
            //            .ForMember(d => d.ReplyCount, u => u.MapFrom(p => p.Replies.Count));
            //        c.CreateMap<Reply, ReplyDto>();
            //    });
        }
        // Automapper
        //-----------------------------------------------------
        private static void InitializeAutomapper()
        {
            Mapper.Initialize(c => c.AddProfile<ForumProfile>());
        }
        //----------------------------------------------------
        private static IServiceProvider ConfigureServices() 
        {
            var serviceCollection = new ServiceCollection(); 

            serviceCollection.AddDbContext<ForumDbContext>(o => o.UseSqlServer(Configuration.ConnectionString));
            
            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();//

            serviceCollection.AddTransient<IUserService, UserService>(); 

            serviceCollection.AddTransient<IPostService, PostService>();

            serviceCollection.AddTransient<ICategoryService, CategoryService>();

            serviceCollection.AddTransient<IReplyService, ReplyService>();

            var serviceProvider = serviceCollection.BuildServiceProvider(); 

            return serviceProvider;
        }
    }
}
