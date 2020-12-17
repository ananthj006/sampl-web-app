using Microsoft.AspNetCore.Mvc;
using Sample.Domain;
using Sample.DTO;
using System.Collections.Generic;

namespace Sample.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDomain _userDomain;
        public UserController(IUserDomain userDomain)
        {
            _userDomain = userDomain;
        }
        [HttpGet("GetUsers")]
        public IEnumerable<User> GetUsers(string search, string sort)
        {
            return _userDomain.GetUsers(search, sort);
        }

        [HttpPost]
        public int PostUser(User user)
        {
            return _userDomain.AddUser(user);
        }

        [HttpPut("{userId}")]
        public ActionResult<int> PutUser(int userId, User user)
        {
            var updatedId = _userDomain.UpdateUser(userId, user);

            if (updatedId == 0) return BadRequest("Invalid data");

            return updatedId;
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            var test = _userDomain.DeleteUser(userId);

            if (test == 0) return BadRequest("Invalid user");

            return NoContent();
        }
    }
}
