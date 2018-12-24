using Lab.Data.Models;
using System.Collections.Generic;

namespace Forum.Services.Contracts
{
    public interface IPostService
    {
        Post Create(string title, string content, int categoryId, int authorId);

        //IQueryable<TModel> All<TModel>();        
        IEnumerable<Post> All();

        Post ById(int postId);
       
    }
}
