using AutoMapper;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Domain.Common.Interfaces.Repositories;
using FlowaStudy.Domain.Common.Interfaces.Services;
using FlowaStudy.Domain.Entities;
using MediatR;

namespace FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetHandler : IRequestHandler<GetAllFinancialAssetCommand, List<GetAllFinancialAssetResult>>
    {
        private readonly IFinancialAssetRepository _financialAssetRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private readonly IFinancialAssetRepositoryMongo _financialAssetRepositoryMongo;


        public GetAllFinancialAssetHandler(IFinancialAssetRepository financialAssetRepository, IMapper mapper, ICacheService cache
            , IFinancialAssetRepositoryMongo financialAssetRepositoryMongo)
        {
            _financialAssetRepository = financialAssetRepository;
            _financialAssetRepositoryMongo = financialAssetRepositoryMongo;
            _mapper = mapper;
            _cache = cache;
            _financialAssetRepositoryMongo = financialAssetRepositoryMongo;
        }

        public async Task<List<GetAllFinancialAssetResult>> Handle(GetAllFinancialAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string cacheKey = "financialAssets:all";
                var cachedData = await _cache.GetAsync<List<GetAllFinancialAssetResult>>(cacheKey);
                if (cachedData != null)
                {
                    return cachedData;
                }

                var listFinancialAsset = await _financialAssetRepository.GetAll(cancellationToken);
                
                var listFinancialAssetMongo = await _financialAssetRepositoryMongo.GetAllMongoDbAsync();

                if (listFinancialAsset == null)
                {
                    return new List<GetAllFinancialAssetResult>();
                }

                var result = _mapper.Map<List<GetAllFinancialAssetResult>>(listFinancialAsset);

                await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10));

                return result;
            }
            catch (Exception)
            {
                throw ; 
            }
        }
    }
}
