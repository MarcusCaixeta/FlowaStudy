using AutoMapper;
using FlowaStudy.Application.Users.CreateUser;
using FlowaStudy.Domain.Entities;

namespace FlowaStudy.WebApi.Features.Users.CreateUser
{
    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<CreateUserRequest, CreateUserCommand>();

            CreateMap<CreateUserResult, CreateUserResponse>();

            CreateMap<User, CreateUserResult>();

        }
    }
}
