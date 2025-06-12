
using AutoMapper;
using FlowaStudy.Domain.Entities;
using FlowaStudy.Domain.Repositories;
using MediatR;

namespace FlowaStudy.Application.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetHandler : IRequestHandler<CreateFinancialAssetCommand, CreateFinancialAssetResult>
    {
        private readonly IFinancialAssetRepository _financialAssetRepository;
        private readonly IMapper _mapper;

        public CreateFinancialAssetHandler(IFinancialAssetRepository financialAssetRepository, IMapper mapper)
        {
            _financialAssetRepository = financialAssetRepository;
            _mapper = mapper;
        }

        public async Task<CreateFinancialAssetResult> Handle(CreateFinancialAssetCommand command, CancellationToken cancellationToken)
        {
            var financialAsset = _mapper.Map<FinancialAsset>(command);
            var createdFinancialAsset = await _financialAssetRepository.CreateAsync(financialAsset, cancellationToken);
            
            var result = _mapper.Map<CreateFinancialAssetResult>(createdFinancialAsset);
            return result;
        }
    }
}
