using Ecaq.Data;
using Ecaq.Models;
using Ecaq.Services.Interfaces;

namespace Ecaq.Services.Repositories;

public class BookModelRepository : Repository<BookModel>, IBookModelRepository
{
    public BookModelRepository(AppDbContext context) : base(context)
    {

    }
}
