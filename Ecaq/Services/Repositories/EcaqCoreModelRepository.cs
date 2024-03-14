using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class EcaqCoreModelRepository : Repository<EcaqCoreModel>, IEcaqCoreModelRepository
{
    public EcaqCoreModelRepository(AppDbContext context) : base(context)
    {

    }
}
