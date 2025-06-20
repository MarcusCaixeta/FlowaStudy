using AutoMapper;
using FlowaStudy.Application.AssetTransactions.CreateAssetTransaction;
using FlowaStudy.Domain.Entities;

namespace FlowaStudy.WebApi.Features.AssetTransactions.CreateAssetTransaction
{
    public class CreateAssetTransactionProfile : Profile
    {
        public CreateAssetTransactionProfile()
        {
            CreateMap<CreateAssetTransactionRequest, CreateAssetTransactionCommand>();

            CreateMap<CreateAssetTransactionResult, CreateAssetTransactionResponse>();

            CreateMap<AssetTransaction, CreateAssetTransactionResult>();

        }
    }
}
