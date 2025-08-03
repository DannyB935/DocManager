using api_docmanager.Dtos.Unit;
using api_docmanager.Dtos.Users;
using api_docmanager.Entities;
using AutoMapper;

namespace api_docmanager.Utils;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Unit, UnitDto>();
        CreateMap<CreateUnitDto, Unit>();

        CreateMap<CreateUserDto, UserAccount>();
        CreateMap<UserAccount, UserDto>()
            .ForMember(dto => dto.FullName,
                config => config.MapFrom(account => GetFullName(account)))
            .ForMember(dto => dto.UnitBelongName,
                config => config.MapFrom(account => account.UnitBelongNavigation.Name));
        CreateMap<UpdateUserDto, UserAccount>();
    }

    private string GetFullName(UserAccount user) => $"{user.NameUsr} {user.Lname}";
}
