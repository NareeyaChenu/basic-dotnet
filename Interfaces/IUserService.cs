using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
namespace TodoApi.Interfaces
{
    public interface IUserService
    {
        public ActionResult GetUsers (UserParam param);

        public Task<ActionResult> CreatUser (UserModel model);
    }
}