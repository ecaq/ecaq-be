using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class MemberModelEndpoints
{
    public static void MapMemberModelEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/MemberModel").WithTags(nameof(MemberModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.MemberModels.ToListAsync();
        })
        .WithName("GetAllMemberModels")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<MemberModel>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.MemberModels.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is MemberModel model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMemberModelById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, MemberModel memberModel, AppDbContext db) =>
        {
            var affected = await db.MemberModels
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, memberModel.Id)
                    .SetProperty(m => m.MemberName, memberModel.MemberName)
                    .SetProperty(m => m.Notes, memberModel.Notes)
                    .SetProperty(m => m.lat, memberModel.lat)
                    .SetProperty(m => m.lng, memberModel.lng)
                    .SetProperty(m => m.Sort, memberModel.Sort)
                    .SetProperty(m => m.IsActive, memberModel.IsActive)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMemberModel")
        .WithOpenApi();

        group.MapPost("/", async (MemberModel memberModel, AppDbContext db) =>
        {
            db.MemberModels.Add(memberModel);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/MemberModel/{memberModel.Id}", memberModel);
        })
        .WithName("CreateMemberModel")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.MemberModels
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMemberModel")
        .WithOpenApi();
    }
}
