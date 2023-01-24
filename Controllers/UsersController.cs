using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public List<ApplicationUser> GetAll()
        {
            var result = _userRepository.GetAllUsers();
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApplicationUser> GetUserById([FromRoute] string id)
        {
            var result = await _userRepository.GetUserByIdAsync(id);
            return result;
        }

        [HttpPut("{id}")]
        public async Task UpdateUser([FromRoute] string id,[FromBody] ApplicationUser applicationUser)
        {
            await _userRepository.UpdateUser(id, applicationUser);            
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfile()
        {
            var file = Request.Form.Files[0];
            string folder = "src/assets/";
            folder += DateTime.Now.ToString("yyyyMMddHHmmssffff") + file.FileName;
            string serverFolder = Path.Combine("D:\\Angular\\CRUD", folder);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return Ok(new { folder });

        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            await _userRepository.DeleteUser(id);
        }
    }
}
