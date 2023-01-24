using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IMissionRepository
    {
        Task<List<MissionModel>> GetAllMissionAsync();
        Task<int> AddMissionAsync(MissionModel missionModel);
        Task<MissionModel> GetMissionByIdAsync(int missionId);
        Task UpdateMission(int id, MissionModel missionModel);
    }
}
