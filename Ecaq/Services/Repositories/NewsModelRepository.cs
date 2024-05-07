using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class NewsModelRepository : Repository<NewsModel>, INewsModelRepository
{
    public NewsModelRepository(AppDbContext context) : base(context)
    {

    }
}
