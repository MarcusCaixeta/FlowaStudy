using AutoMapper;
using FlowaStudy.Application.Assets.CreateAsset;
using FlowaStudy.Domain.Entities;


namespace FlowaStudy.WebApi.Features.Assets.CreateAsset
{
    public class CreateAssetProfile : Profile
    {
        public CreateAssetProfile()
        {
            CreateMap<CreateAssetRequest, CreateAssetCommand>();

            CreateMap<CreateAssetResult, CreateAssetResponse>();

            CreateMap<Asset, CreateAssetResult>();

        }
    }
}
