using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Interfaces;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        // [Authorize (Policy = "Admin")]
        [AllowAnonymous]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetUser([FromQuery] UserParam param)
        {

            return _userService.GetUsers(param);
        }


        [HttpGet]
        [Authorize (Policy = "Editor")]
        [Route("{id}")]

        public ActionResult GetUserById([FromRoute] int id)
        {
             // Access the raw token from the Authorization header
            var rawToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            _logger.LogInformation(rawToken);
            var users = UserStore.Users;

            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound($"user with id  {id} does not exist");
            }

            return Ok(user);
        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult RemoveUser([FromRoute] int id)
        {
            var users = UserStore.Users;

            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound($"user with id  {id} does not exist");
            }

            users.Remove(user);
            return Ok("delete user successfully");
        }


        [HttpPost]
        [Route("{id}")]

        public ActionResult EditUser([FromBody] UserModel model, [FromRoute] int id)
        {
            var users = UserStore.Users;

            var user = users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                return NotFound($"user with id  {id} does not exist");
            }


            UserModel newUser = new UserModel
            {
                Id = id,
                Name = model.Name

            };

            users.Remove(user);
            users.Add(newUser);

            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserModel model)
        {
            return await _userService.CreatUser(model);
        }



    }
}