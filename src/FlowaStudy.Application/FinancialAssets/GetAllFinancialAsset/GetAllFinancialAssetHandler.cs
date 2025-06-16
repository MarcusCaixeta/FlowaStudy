using AutoMapper;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Domain.Entities;
using FlowaStudy.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetHandler : IRequestHandler<GetAllFinancialAssetCommand, List<GetAllFinancialAssetResult>>
    {
        private readonly IFinancialAssetRepository _financialAssetRepository;
        private readonly IMapper _mapper;

        public GetAllFinancialAssetHandler(IFinancialAssetRepository financialAssetRepository, IMapper mapper)
        {
            _financialAssetRepository = financialAssetRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllFinancialAssetResult>> Handle(GetAllFinancialAssetCommand request, CancellationToken cancellationToken)
        {
            var listFinancialAsset = await _financialAssetRepository.GetAll(cancellationToken);
           
            var result = _mapper.Map<List<GetAllFinancialAssetResult>>(listFinancialAsset);
            return result;
        }
    }
}
