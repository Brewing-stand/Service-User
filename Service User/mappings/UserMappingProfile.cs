using AutoMapper;
using Service_User.DTOs;
using Service_User.Models;

namespace Service_User.mappings;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Create map between User and UserResponseDto
        CreateMap<User, UserResponseDto>();
        // Create map between UserRequestDto and User
        CreateMap<UserRequestDto, User>();
    }
}