using BulletinBoard.DataAccess.Models;
using BulletinBoard.DataAccess.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.DataAccess.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly IDbConnection _db;
    public AnnouncementRepository(IDbConnection db)
    {
        _db = db;
    }

    public Task<IEnumerable<Announcement>> GetAll() =>
        _db.QueryAsync<Announcement>(
            "usp_GetAllAnnouncements",
            commandType: CommandType.StoredProcedure
        );

    public async Task<Announcement> GetById(Guid id) =>
        (await _db.QueryAsync<Announcement>(
            "usp_GetAnnouncementById",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        )).FirstOrDefault();

    public Task Create(Announcement ann) =>
        _db.ExecuteAsync(
            "usp_CreateAnnouncement",
            new
            {
                ann.Title,
                ann.Description,
                ann.CreatedDate,
                ann.Status,
                ann.Category,
                ann.SubCategory
            },
            commandType: CommandType.StoredProcedure
        );

    public Task Update(Announcement ann) =>
        _db.ExecuteAsync(
            "usp_UpdateAnnouncement",
            new
            {
                ann.Id,
                ann.Title,
                ann.Description,
                ann.Status,
                ann.Category,
                ann.SubCategory
            },
            commandType: CommandType.StoredProcedure
        );

    public Task Delete(Guid id) =>
        _db.ExecuteAsync(
            "usp_DeleteAnnouncement",
            new { Id = id },
            commandType: CommandType.StoredProcedure
        );
}

