using AutoMapper;
using Domain.User.Entities;
using Shared.Dtos.User;

namespace Application.Mapping.User
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName ?? string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.LoginFailedCount, opt => opt.MapFrom(src => src.LoginFailedCount))
                .ForMember(dest => dest.Islock, opt => opt.MapFrom(src => src.Islock))
                .ForMember(dest => dest.NationalCode, opt => opt.MapFrom(src => src.NationalCode))
                .ForMember(dest => dest.ActivationCode, opt => opt.MapFrom(src => src.ActivationCode))
                .ForMember(dest => dest.LockTimeEnd, opt => opt.MapFrom(src => src.LockTimeEnd))
                .ForMember(dest => dest.PasswordIncorrectCount, opt => opt.MapFrom(src => src.PasswordIncorrectCount))
                .ForMember(dest => dest.ForceChangePassword, opt => opt.MapFrom(src => src.ForceChangePassword))
                .ForMember(dest => dest.IsTwoFactorLogin, opt => opt.MapFrom(src => src.IsTwoFactorLogin))
                .ForMember(dest => dest.LastSuccessLoginDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastFaildLoginDateTime, opt => opt.Ignore())
                .ForMember(dest => dest.LastFaildLoginCount, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentSuccessLoginDateTime, opt => opt.Ignore());
        }
    }
}
