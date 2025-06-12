using FlowaStudy.Domain.Entities;
using AutoMapper;

namespace FlowaStudy.Application.FinancialAssets.CreateFinancialAsset
{
    public class CreateFinancialAssetProfile : Profile
    {   
        public CreateFinancialAssetProfile()
        {
            CreateMap<CreateFinancialAssetCommand, FinancialAsset>();
            CreateMap<FinancialAsset, CreateFinancialAssetResult>();
        }
    }
}
