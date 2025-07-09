using BulletinBoard.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.DataAccess.Repositories.Interfaces;

public interface IAnnouncementRepository
{
    Task<IEnumerable<Announcement>> GetAll();

    Task<Announcement> GetById(Guid Id);

    Task Create(Announcement announcement);

    Task Update(Announcement announcement);
    
    Task Delete(Guid id);
}
