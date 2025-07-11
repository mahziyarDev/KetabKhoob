using Api.ViewModels.Users;
using AutoMapper;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.ChangePassword;

namespace Api.Infrastructure;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AddUserAddressCommand, AddUserAddressViewModel>()
            .ForMember(f=>f.PhoneNumber,r=>r.MapFrom(w=>w.PhoneNumber)).ReverseMap();

        CreateMap<ChangePasswordViewModel, ChangeUserPasswordCommand>().ReverseMap();
    }
}