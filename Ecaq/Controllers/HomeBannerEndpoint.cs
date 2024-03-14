using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class HomeBannerEndpoint
{
    public static void MapHomeBannerEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/home-banner").WithTags(nameof(HomeBanner));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.HomeBanners.ToListAsync();
        })
        .WithName("GetBanners")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<HomeBanner>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.HomeBanners.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is HomeBanner model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBanner")
        .WithOpenApi();

        //group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, MemberModel memberModel, AppDbContext db) =>
        //{
        //    var affected = await db.MemberModels
        //        .Where(model => model.Id == id)
        //        .ExecuteUpdateAsync(setters => setters
        //            .SetProperty(m => m.Id, memberModel.Id)
        //            .SetProperty(m => m.MemberName, memberModel.MemberName)
        //            .SetProperty(m => m.Notes, memberModel.Notes)
        //            .SetProperty(m => m.lat, memberModel.lat)
        //            .SetProperty(m => m.lng, memberModel.lng)
        //            .SetProperty(m => m.Sort, memberModel.Sort)
        //            .SetProperty(m => m.IsActive, memberModel.IsActive)
        //            );
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("UpdateBanner")
        //.WithOpenApi();

        //group.MapPost("/", async (MemberModel memberModel, AppDbContext db) =>
        //{
        //    db.MemberModels.Add(memberModel);
        //    await db.SaveChangesAsync();
        //    return TypedResults.Created($"/api/home-banner/{memberModel.Id}", memberModel);
        //})
        //.WithName("CreateBanner")
        //.WithOpenApi();

        //group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        //{
        //    var affected = await db.MemberModels
        //        .Where(model => model.Id == id)
        //        .ExecuteDeleteAsync();
        //    return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        //})
        //.WithName("DeleteBanner")
        //.WithOpenApi();
    }
}
