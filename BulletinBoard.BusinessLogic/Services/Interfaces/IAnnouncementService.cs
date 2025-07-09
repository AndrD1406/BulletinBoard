using BulletinBoard.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.BusinessLogic.Services.Interfaces;

public interface IAnnouncementService
{
    Task<IEnumerable<Announcement>> GetAll();
    Task<Announcement> GetById(Guid id);
    Task Create(Announcement ann);
    Task Update(Announcement ann);
    Task Delete(Guid id);
}
