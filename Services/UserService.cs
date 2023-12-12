
using TodoApi.Interfaces;
using TodoApi.DTOs;
namespace TodoApi.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }


        public IEnumerable<UserModel> GetUsers(UserParam param)
        {
            var users = UserStore.Users;


            if (!string.IsNullOrEmpty(param.Name))
            {
                var name = param.Name!.ToLower();
                users = users.Where(x => x.Name!.ToLower()!.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return users;
        }


    }
}