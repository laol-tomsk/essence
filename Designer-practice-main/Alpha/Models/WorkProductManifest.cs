using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class WorkProductManifest
    {
        public decimal UpperBound { get; set; }
        public decimal LowerBound { get; set; }
        public Guid AlphaId { get; set; }
        private Alpha Alpha { get; set; } = null;
        public Guid WorkProductId { get; set; }
        private WorkProduct WorkProduct { get; set; } = null;
        public int NormalValue { get; set; }

        public Guid GetAlphaId() => AlphaId;
        public Guid GetWorkProductId() => WorkProductId;
        public WorkProduct GetWorkProduct() => WorkProduct;
        public decimal GetUpperBound() => UpperBound;
        public decimal GetLowerBound() => LowerBound;
        public Alpha GetAlpha() => Alpha;
        public int GetNormalValue() => NormalValue;
        public WorkProductManifest()
        {
        }
        public WorkProductManifest(decimal upper,decimal lower, Alpha alpha, WorkProduct workProduct, int normalVolue)
        {
            UpperBound = upper;
            LowerBound = lower;
            Alpha = alpha;
            AlphaId = alpha.GetAlphaId();
            WorkProduct = workProduct;
            WorkProductId = workProduct.GetWorkProductId();
            NormalValue = normalVolue;
        }
        public void SetWorkProduct(WorkProduct workProduct)
        {
            WorkProduct = workProduct;
            WorkProductId = workProduct.GetWorkProductId();
            workProduct.AddWorkProductManifest(this);
        }
        public void SetAlpha(Alpha alpha)
        {
            Alpha = alpha;
            AlphaId = alpha.GetAlphaId();
            alpha.SetWorkProductManifest(this);
        }

    }
}
