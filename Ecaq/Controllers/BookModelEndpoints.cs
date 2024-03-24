using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class BookModelEndpoints
{
    public static void MapBookModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BookModel").WithTags(nameof(BookModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.BookModels.ToListAsync();
        })
        .WithName("GetAllBookModels")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BookModel>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.BookModels.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is BookModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBookModelById")
        .WithOpenApi();

    }
}
