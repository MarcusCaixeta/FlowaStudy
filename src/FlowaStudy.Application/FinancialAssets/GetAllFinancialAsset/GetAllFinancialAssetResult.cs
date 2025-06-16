using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowaStudy.Application.FinancialAssets.GetAllFinancialAsset
{
    public class GetAllFinancialAssetResult
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime AcquisitionDate { get; set; }
    }
}
