
using AutoMapper;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetHandler : IRequestHandler<CreateFinancialAssetCommand, CreateFinancialAssetResult>
    {
        private readonly IFinancialAssetRepository _financialAssetRepository;
        private readonly IFinancialAssetRepositoryMongo _financialAssetRepositoryMongo;
        private readonly IMapper _mapper;

        public CreateFinancialAssetHandler(IFinancialAssetRepository financialAssetRepository, IMapper mapper, IFinancialAssetRepositoryMongo financialAssetRepositoryMongo)
        {
            _financialAssetRepository = financialAssetRepository;
            _mapper = mapper;
            _financialAssetRepositoryMongo = financialAssetRepositoryMongo;
        }

        public async Task<CreateFinancialAssetResult> Handle(CreateFinancialAssetCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<CreateFinancialAssetCommand>(command);
            var entity = new FinancialAsset(request.Name, request.Value, request.AcquisitionDate);


            var createdFinancialAsset = await _financialAssetRepository.CreateAsync(entity, cancellationToken);

            var entityMongo = new FinancialAssetMongo(createdFinancialAsset.Id, request.Name, request.Value, request.AcquisitionDate);
            
            await _financialAssetRepositoryMongo.InsertMongoDbAsync(entityMongo);

            var result = _mapper.Map<CreateFinancialAssetResult>(createdFinancialAsset);
            return result;
        }
    }
}
