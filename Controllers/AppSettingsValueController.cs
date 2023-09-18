using idstar_web_api.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace idstar_web_api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AppSettingsValueController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly UserProfile _userProfile;
        private readonly UserAddress _userAddress;
        private readonly List<Users> _users;

        public AppSettingsValueController(IConfiguration configuration, IOptions<UserProfile> userProfile, UserAddress userAddress, List<Users> dummyUser) {
            _configuration = configuration;
            _userProfile = userProfile.Value;
            _userAddress = userAddress;
            _users = dummyUser;
        }

        [HttpGet("ListSettings")]
        public IActionResult ListSettings() {
            var keyApp = _configuration["KeyApp"];
            var access = _configuration.GetSection("UserAccess").Get<UserAccess>();
            

            var data = new {
                key = keyApp,
                userPrfofile = _userProfile,
                userAccess = access,
                userAddress = _userAddress,
            };

            return Ok(data);
        }

        [HttpGet("Users")]
        public IActionResult Users() {

            return Ok(_users);
        }
    }
}
