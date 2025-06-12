using AutoMapper;
using FlowaStudy.Application.FinancialAssets.CreateFinancialAsset;
using FlowaStudy.Domain.Entities;

namespace FlowaStudy.WebApi.Features.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetProfile : Profile
    {

        public CreateFinancialAssetProfile()
        {
            CreateMap<CreateFinancialAssetRequest, CreateFinancialAssetResponse>();
            CreateMap<CreateFinancialAssetResponse, CreateFinancialAssetRequest>();

            CreateMap<FinancialAsset, CreateFinancialAssetResult>();

        }
    }
}
