using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class MissionRepository : IMissionRepository
    {
        private readonly BookStoreContext _context;

        public MissionRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<MissionModel>> GetAllMissionAsync()
        {
            var records = await _context.Missions.Select(x => new MissionModel()
            {
                MissionId = x.MissionId,
                MissionTitle = x.MissionTitle,
                MissionName = x.MissionName,
                MissionDescription = x.MissionDescription,
                missionPicUrl = x.missionPicUrl,
                AvailableSeats = x.AvailableSeats,
                StartDate = x.StartDate,
                Deadline = x.Deadline,
                Location = x.Location,
            }).ToListAsync();
            return records;
        }

        public async Task<MissionModel> GetMissionByIdAsync(int missionId)
        {
            var records = await _context.Missions.Where(x => x.MissionId == missionId).Select(x => new MissionModel()
            {
                MissionId = x.MissionId,
                MissionTitle = x.MissionTitle,
                MissionName = x.MissionName,
                MissionDescription = x.MissionDescription,
                missionPicUrl = x.missionPicUrl,
                AvailableSeats = x.AvailableSeats,
                StartDate = x.StartDate,
                Deadline = x.Deadline,
                Location = x.Location
            }).FirstOrDefaultAsync();
            return records;
        }

        public async Task<int> AddMissionAsync(MissionModel missionModel)
        {
            var mission = new MissionModel()
            {
                MissionDescription = missionModel.MissionDescription,
                MissionName = missionModel.MissionName,
                MissionTitle = missionModel.MissionTitle,
                AvailableSeats = missionModel.AvailableSeats,
                missionPicUrl = missionModel.missionPicUrl,
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Deadline = missionModel.Deadline,
                Location=missionModel.Location
            };
            _context.Missions.Add(mission);
            _context.SaveChanges();
            return missionModel.MissionId;
        }

        public async Task UpdateMission(int id , MissionModel missionModel)
        {
            var mission = new MissionModel()
            {
                MissionId = id,
                MissionName = missionModel.MissionName,
                MissionDescription = missionModel.MissionDescription,
                missionPicUrl = missionModel.missionPicUrl,
                AvailableSeats = missionModel.AvailableSeats,
                StartDate = missionModel.StartDate,
                Deadline = missionModel.Deadline,
                MissionTitle = missionModel.MissionTitle,
                Location = missionModel.Location
            };
            _context.Missions.Update(mission);
            await _context.SaveChangesAsync();
        }
    }
}
