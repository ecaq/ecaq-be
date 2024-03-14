using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class HomeBannerRepository : Repository<HomeBanner>, IHomeBannerRepository
{
    public HomeBannerRepository(AppDbContext context) : base(context)
    {

    }
}
