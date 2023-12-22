
using TodoApi.Interfaces;
using TodoApi.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class UserService : ControllerBase ,IUserService
    {
        private readonly  UserManager<IdentityUser> _userManger;

         private readonly ApplicationDbContext _context;
        public UserService(UserManager<IdentityUser> userManger, ApplicationDbContext context)
        {
            _userManger = userManger;
            _context = context;
        }

        public async Task<ActionResult> CreatUser(UserModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email!);
            var message = new object();

            if(user != null)
            {
                message = new {
                    message = $"user with email {model.Email!} already exist"
                };

                return Conflict(message);
            }
            user = new IdentityUser {
                Email = model.Email,
                UserName = model.Email
            };

            await _userManger.CreateAsync(user!);

            return  Ok(user);
        }

        public ActionResult GetUsers(UserParam param)
        {
            var users = _context.Users.ToList();
            if (!string.IsNullOrEmpty(param.Name))
            {
                var name = param.Name!.ToLower();
                users = users.Where(x => x.Email!.ToLower()!.Contains(name)).ToList();
            }
            return Ok(users);
        }


    }
}