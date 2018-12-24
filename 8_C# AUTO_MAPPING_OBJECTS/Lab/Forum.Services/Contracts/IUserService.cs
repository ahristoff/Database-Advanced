using Lab.Data.Models;

namespace Forum.Services.Contracts
{
    public interface IUserService
    {
        User ById(int Id);                    

        User ByUsername(string username);       

        User ByUsernameAndPassword(string username, string password);   

        User Create(string username, string password);     

        void Delete(int Id);                              
    }
}
