using api_docmanager.Dtos.Unit;
using api_docmanager.Entities;
using AutoMapper;

namespace api_docmanager.Utils;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Unit, UnitDto>();
        CreateMap<CreateUnitDto, Unit>();
    }
}