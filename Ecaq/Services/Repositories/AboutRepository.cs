using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class AboutRepository : Repository<AboutModel>, IAboutRepository
{
    public AboutRepository(AppDbContext context) : base(context)
    {

    }
}
