using api_docmanager.Dtos.Assignments;
using api_docmanager.Dtos.Documents;
using api_docmanager.Dtos.Unit;
using api_docmanager.Dtos.Users;
using api_docmanager.Entities;
using AutoMapper;

namespace api_docmanager.Utils;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        //Unit maps
        CreateMap<Unit, UnitDto>();
        CreateMap<CreateUnitDto, Unit>();

        //User maps
        CreateMap<CreateUserDto, UserAccount>();
        CreateMap<UserAccount, UserDto>()
            .ForMember(dto => dto.FullName,
                config => config.MapFrom(account => GetFullName(account)))
            .ForMember(dto => dto.UnitBelongName,
                config => config.MapFrom(account => account.UnitBelongNavigation.Name));
        CreateMap<UpdateUserDto, UserAccount>();
        
        //Document maps
        CreateMap<CreateDocDto, Document>();
        CreateMap<Document, DocDto>()
            .ForMember(dto => dto.FullNameSender,
                config => config.MapFrom(doc => $"{doc.NameSender} {doc.LnameSender}"))
            .ForMember(dto => dto.FullNameRecip,
                config => config.MapFrom(doc => $"{doc.NameRecip} {doc.LnameRecip}"))
            .ForMember(dto => dto.UnitBelongName,
                config => config.MapFrom(doc => doc.UnitBelongNavigation.Name))
            .ForMember(dto => dto.GenByUsrName,
                config => config.MapFrom(doc => GetFullName(doc.GenByUsrNavigation)));
        CreateMap<UpdateDocDto, Document>();
        
        //Assignment log maps
        CreateMap<CreateAssignDto, AssignmentLog>();
        CreateMap<ConcludeDocDto, AssignmentLog>();
        CreateMap<AssignmentLog, AssignmentDto>()
            .ForMember(dto => dto.FullNameUsr, config => config.MapFrom(src => GetFullName(src.UsrAssignNavigation)));
    }

    private string GetFullName(UserAccount user) => $"{user.NameUsr} {user.Lname}";
}
