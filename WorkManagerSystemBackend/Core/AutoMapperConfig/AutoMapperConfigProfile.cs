using AutoMapper;
using WorkManagerSystemBackend.Core.Dtos.Space;
using WorkManagerSystemBackend.Core.Dtos.User;
using WorkManagerSystemBackend.Core.Dtos.UserSpace;
using WorkManagerSystemBackend.Core.Dtos.WorkItem;
using WorkManagerSystemBackend.Core.Dtos.WorkItemState;
using WorkManagerSystemBackend.Core.Dtos.WorkItemType;
using WorkManagerSystemBackend.Core.Entities;

namespace WorkManagerSystemBackend.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            //User
            CreateMap<UserRegisterDto, Users>();
            CreateMap<Users, UserGetDto>();
            //Space
            CreateMap<SpaceCreateDto, Space>();
            CreateMap<Space, SpaceGetDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users.FirstName));
            //ItemType
            CreateMap<WorkItemTypeCreateDto, WorkItemType>();
            CreateMap<WorkItemType, WorkItemTypeGetDto>()
                .ForMember(dest => dest.SpaceName, opt => opt.MapFrom(src => src.Space.Name));
            //ItemState
            CreateMap<WorkItemStateCreateDto, WorkItemState>();
            CreateMap<WorkItemState, WorkItemStateGetDto>()
                .ForMember(dest => dest.SpaceName, opt => opt.MapFrom(src => src.Space.Name));
            //Item
            CreateMap<WorkItemCreateDto, WorkItem>();
            CreateMap<WorkItem, WorkItemGetDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Users.FirstName))
               .ForMember(dest => dest.SpaceName, opt => opt.MapFrom(src => src.Space.Name))
               .ForMember(dest => dest.WorkItemTypeName, opt => opt.MapFrom(src => src.WorkItemType.Name))
               .ForMember(dest => dest.WorkItemStateName, opt => opt.MapFrom(src => src.WorkItemState.Name))
               .ForMember(dest => dest.FinalStatus, opt => opt.MapFrom(src => src.WorkItemState.FinalStatus))
               .ForMember(dest => dest.WorkItemStateColor, opt => opt.MapFrom(src => src.WorkItemState.Color))
               .ForMember(dest => dest.WorkItemTypeColor, opt => opt.MapFrom(src => src.WorkItemType.Color));
            //UpdateName
            CreateMap<UpdateUserNameDto, Users>()
               .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
               .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName));
            //UpdateEmail
            CreateMap<UpdateUserEmailDto, Users>()
                .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email));
            //UserSpace
            CreateMap<CreateUserSpaceDto, UserSpace>();
            CreateMap<UserSpace, GetUserSpaceDto>()
                .ForMember(dest => dest.Space, opt => opt.MapFrom(src => src.Space))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Users));
            //UpdateFinalStatus
            CreateMap<UpdateFinalStateDto, WorkItemState>()
                .ForMember(dest => dest.FinalStatus, act => act.MapFrom(src => src.FinalStatus));
            //UpdateTypeName
            CreateMap<UpdateTypeNameDto, WorkItemType>()
               .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
               .ForMember(dest => dest.Color, act => act.MapFrom(src => src.Color));
            //UpdateStateName
            CreateMap<UpdateStateNameDto, WorkItemState>()
               .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
               .ForMember(dest => dest.Color, act => act.MapFrom(src => src.Color));


        }
    }
}
