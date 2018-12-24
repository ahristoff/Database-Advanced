using Forum.Data.Models;

namespace Forum.Services.Contracts
{
    public interface ICategoryService
    {       
        Category ByName(string name);

        Category Create(string name);
    }
}
