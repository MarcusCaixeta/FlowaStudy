﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetCommand : IRequest<List<GetAllFinancialAssetResult>>
    {
    }
}
