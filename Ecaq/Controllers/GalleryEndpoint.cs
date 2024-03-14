using AutoMapper;
using Ecaq.Data;
using Ecaq.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Ecaq.Controllers;

public static class GalleryEndpoint
{
    public static void MapGalleryEndpoint(this IEndpointRouteBuilder routes, IWebHostEnvironment hostingEnv, IMapper mapper)
    {

        var group = routes.MapGroup("/api/gallery").WithTags(nameof(GalleryModel));

        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.GalleryModels.ToListAsync();
        })
        .WithName("GetGalleries")
        .WithOpenApi();

        group.MapGet("/with-images", async Task < Results<Ok<List<GalleryWithImagesDto>>, NotFound> > (AppDbContext db) =>
        {
            string webRootPath = hostingEnv.WebRootPath;

            List<GalleryWithImagesDto> galleriesDto = new List<GalleryWithImagesDto>();
            var galleries = await db.GalleryModels.ToListAsync();
            var dto = mapper.Map(galleries, galleriesDto);
            foreach(var gallery in dto)
            {
                var folderPath = webRootPath + gallery!.PathUrl.Replace("/", "\\");
                //string path = $"{Directory.GetCurrentDirectory()}{webRootPath + gallery!.PathUrl.Replace("/", "\\")}";
                var files = Directory.GetFiles(folderPath);

                List<string> fileList = new();

                foreach (var file in files)
                {
                    string imgName = Path.GetFileName(file);


                    fileList.Add($"{imgName}");
                }

                gallery.ImageFileNames = fileList;
            }

            return TypedResults.Ok(dto);
        })
        .WithName("GetGalleriesWithImages")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<GalleryModel>, NotFound>> (int id, AppDbContext db) =>
        {
            var gallery = await db.GalleryModels.AsNoTracking().FirstOrDefaultAsync(model => model.Id == id);

            return gallery is not null ? TypedResults.Ok(gallery) : TypedResults.NotFound();
        })
        .WithName("GetGallery")
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
