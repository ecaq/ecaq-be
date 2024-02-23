using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class MemberModelRepository : Repository<MemberModel>, IMemberModelRepository
{
    public MemberModelRepository(AppDbContext context) : base(context)
    {
        
    }
}
