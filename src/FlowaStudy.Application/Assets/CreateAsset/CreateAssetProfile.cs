using AutoMapper;
using FlowaStudy.Domain.Entities;

namespace FlowaStudy.Application.Assets.CreateAsset
{
    public  class CreateAssetProfile : Profile
    {
        public CreateAssetProfile()
        {
            CreateMap<CreateAssetCommand, Asset>();
            CreateMap<Asset, CreateAssetResult>();
        }
    }
}
