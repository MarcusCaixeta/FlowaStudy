using AutoMapper;
using FlowaStudy.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetProfile : Profile
    {
        public GetAllFinancialAssetProfile()
        {
            CreateMap<GetAllFinancialAssetCommand, FinancialAsset>();
            CreateMap<FinancialAsset, GetAllFinancialAssetResult>();
        }   
    }
}
