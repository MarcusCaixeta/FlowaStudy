
using AutoMapper;
using FlowaStudy.Application.Users.CreateUser;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.Assets.CreateAsset
{
    public class CreateAssetHandler : IRequestHandler<CreateAssetCommand, CreateAssetResult>
    {
        private readonly IMapper _mapper;
        private readonly IAssetRepository _assetRepository;

        public CreateAssetHandler(IMapper mapper, IAssetRepository assetRepository)
        {
            _mapper = mapper;
            _assetRepository = assetRepository;
        }
        public async Task<CreateAssetResult> Handle(CreateAssetCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<CreateAssetCommand>(command);
            var entity = new Asset(request.Symbol, request.Name, request.CurrentPrice);

            var createdAsset = await _assetRepository.CreateAsync(entity, cancellationToken);
            var result = _mapper.Map<CreateAssetResult>(createdAsset);

            return result;
        }
    }
}
