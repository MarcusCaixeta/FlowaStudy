using AutoMapper;
using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Application.Users.CreateUser
{
    public class CreateUserProfile : Profile
    {
        public CreateUserProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, CreateUserResult>();
        }
    }
}
