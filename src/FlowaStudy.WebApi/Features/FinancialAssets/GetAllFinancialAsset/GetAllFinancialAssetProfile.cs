using AutoMapper;
using FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset;

namespace FlowaStudy.WebApi.Features.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetProfile : Profile
    {
        public GetAllFinancialAssetProfile()
        {
            CreateMap<List<GetAllFinancialAssetResult>, GetAllFinancialAssetResponse>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));
        }
    }
}
