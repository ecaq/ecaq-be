using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class NewsModelEndpoints
{
    public static void MapNewsModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/News").WithTags(nameof(NewsModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.NewsModels.ToListAsync();
        })
        .WithName("GetAllNews")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<NewsModel>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.NewsModels.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is NewsModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetNewsById")
        .WithOpenApi();

        //group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, NewsModel NewsModel, AppDbContext db) =>
        //{
        //    var affected = await db.NewsModels
        //        .Where(model => model.Id == id)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(m => m.Id, NewsModel.Id)
        //            .SetProperty(m => m.NewsName, NewsModel.NewsName)
        //            .SetProperty(m => m.Notes, NewsModel.Notes)
        //            .SetProperty(m => m.lat, NewsModel.lat)
        //            .SetProperty(m => m.lng, NewsModel.lng)
        //            .SetProperty(m => m.Sort, NewsModel.Sort)
        //            .SetProperty(m => m.IsActive, NewsModel.IsActive)
        //            );
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("UpdateNewsModel")
        //.WithOpenApi();

        //group.MapPost("/", async (NewsModel NewsModel, AppDbContext db) =>
        //{
        //    db.NewsModels.Add(NewsModel);
        //    await db.SaveChangesAsync();
        //    return TypedResults.Created($"/api/NewsModel/{NewsModel.Id}", NewsModel);
        //})
        //.WithName("CreateNewsModel")
        //.WithOpenApi();

        //group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        //{
        //    var affected = await db.NewsModels
        //        .Where(model => model.Id == id)
        //        .ExecuteDeleteAsync();
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("DeleteNewsModel")
        //.WithOpenApi();
    }
}
