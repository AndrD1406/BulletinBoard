using BulletinBoard.BusinessLogic.Services.Interfaces;
using BulletinBoard.DataAccess.Models;
using BulletinBoard.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.BusinessLogic.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _repo;
    public AnnouncementService(IAnnouncementRepository repo) => _repo = repo;

    public Task<IEnumerable<Announcement>> GetAll() =>
        _repo.GetAll();

    public Task<Announcement> GetById(Guid id) =>
        _repo.GetById(id);

    public async Task Create(Announcement ann)
    {
        ann.Id = Guid.NewGuid();
        ann.CreatedDate = DateTime.UtcNow;
        await _repo.Create(ann);
    }

    public Task Update(Announcement ann) =>
        _repo.Update(ann);

    public Task Delete(Guid id) =>
        _repo.Delete(id);
}
