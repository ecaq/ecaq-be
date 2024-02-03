using Microsoft.EntityFrameworkCore;
using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace Ecaq.Controllers;

public static class BookModelEndpoints
{
    public static void MapBookModelEndpoints (this IEndpointRouteBuilder routes)
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

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, BookModel bookModel, AppDbContext db) =>
        {
            var affected = await db.BookModels
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, bookModel.Id)
                    .SetProperty(m => m.Title, bookModel.Title)
                    .SetProperty(m => m.ISBN, bookModel.ISBN)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBookModel")
        .WithOpenApi();

        group.MapPost("/", async (BookModel bookModel, AppDbContext db) =>
        {
            db.BookModels.Add(bookModel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/BookModel/{bookModel.Id}",bookModel);
        })
        .WithName("CreateBookModel")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.BookModels
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBookModel")
        .WithOpenApi();
    }
}
