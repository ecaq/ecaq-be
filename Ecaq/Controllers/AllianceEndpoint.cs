using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class AllianceEndpoint
{
    public static void MapAllianceEndpoint(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/alliance").WithTags(nameof(AllianceModel));
        //var groupCollection = routes.MapGroup("/api/alliance-collection").WithTags(nameof(AllianceCollectionModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            //var alliances = await db.AllianceModels.ToListAsync();
            //foreach(var item in alliances)
            //{
            //    var allianceCol = await db.AllianceCollectionModels.FirstOrDefaultAsync(x => x.Alliance.Id == item.Id);
            //    if(allianceCol is not null)
            //    {
            //        item.Alliances?.Add(allianceCol);
            //    }
            //}

            var result = await db.AllianceModels.Include(x => x.AllianceCollectionModels).ToListAsync();

            return result;
        })
        .WithName("GetAlliances")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<AllianceModel> (int id, AppDbContext db) =>
        {
            //var result = await db.AllianceModels.AsNoTracking().Include(x => x.AllianceCollectionModels)
            //    .FirstOrDefaultAsync(model => model.Id == id)
            //    is AllianceModel model
            //        ? TypedResults.Ok(model)
            //        : TypedResults.NotFound();
            var result = await db.AllianceModels.AsNoTracking().Include(x => x.AllianceCollectionModels)
                        .FirstOrDefaultAsync(model => model.Id == id);
            if(result is not null)
            {
                return result;
            }
            return null;

        })
        .WithName("GetAlliance")
        .WithOpenApi();


        group.MapGet("/collection", async (AppDbContext db) =>
        {
            return await db.AllianceCollectionModels.ToListAsync();
        })
        .WithName("GetAllianceCollection")
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
