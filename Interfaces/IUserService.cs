using TodoApi.DTOs;
namespace TodoApi.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<UserModel> GetUsers (UserParam param);
    }
}