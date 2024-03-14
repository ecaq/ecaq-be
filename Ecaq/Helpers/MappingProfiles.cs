using AutoMapper;
using Ecaq.Models;

namespace Ecaq.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GalleryModel, GalleryWithImagesDto>();
    }
}