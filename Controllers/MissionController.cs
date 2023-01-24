using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly IMissionRepository _missionRepository;

        public MissionController(IMissionRepository missionRepository)
        {
            _missionRepository = missionRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var missions = await _missionRepository.GetAllMissionAsync();
            return Ok(missions);
        }

        [HttpGet("{id}")]   
        public async Task<IActionResult> GetMissionById([FromRoute] int Id)
        {
            var mission = await _missionRepository.GetMissionByIdAsync(Id);
            return Ok(mission);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddMission([FromBody] MissionModel missionModel)
        {
            var id = await _missionRepository.AddMissionAsync(missionModel);
            return Ok(id);

        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage()
        {
            var file = Request.Form.Files[0];
            string folder = "src/assets/";
            folder += DateTime.Now.ToString("yyyyMMddHHmmssffff") + file.FileName;
            string serverFolder = Path.Combine("D:\\Angular\\CRUD", folder);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return Ok(new { folder });
        }
        
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateMissionAsync([FromRoute] int id,[FromBody] MissionModel missionModel)
        {
            await _missionRepository.UpdateMission(id, missionModel);
            return Ok();
        }
    }
}
