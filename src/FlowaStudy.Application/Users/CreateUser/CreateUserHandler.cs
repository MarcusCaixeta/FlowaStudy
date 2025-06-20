using AutoMapper;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IMapper mapper,IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<CreateUserCommand>(command);
            var entity = new User(request.FullName, request.Email, request.Balance);

            var createdUser = await _userRepository.CreateAsync(entity, cancellationToken);
            var result = _mapper.Map<CreateUserResult>(createdUser);

            return result;
        }
    }
}
