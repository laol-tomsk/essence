using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha
{
    public class AlphaContaiment
    {
        public decimal UpperBound { get; set; }
        public decimal LowerBound { get; set; }      
        public Guid SupAlphaId { get; set; }
        private Alpha SupAlpha { get; set; } = null;
        public Guid SubAlphaId { get; set; }
        private Alpha SubAlpha { get; set; } = null;
        public int NormalValue { get; set; }
        public AlphaContaiment()
        {

        }

        public AlphaContaiment(decimal upper, decimal lower, Alpha supAlpha, Alpha subAlpha, int normalVolue)
        {
            UpperBound = upper;
            LowerBound = lower;
            SupAlpha = supAlpha;
            SupAlphaId = supAlpha.GetAlphaId();
            SubAlpha = subAlpha;
            SubAlphaId = subAlpha.GetAlphaId();
            NormalValue = normalVolue;
        }
        public Guid GetSupAlphaId() => SupAlphaId;
        public Guid GetSubAlphaId() => SubAlphaId;
        public Alpha GetSubAlpha() => SubAlpha;
        public decimal GetUpperBound() => UpperBound;
        public decimal GetLowerBound() => LowerBound;

        public int GetNormalValue() => NormalValue;
        public void SetSupAlpha(Alpha alpha)
        {
            SupAlpha = alpha;
            SupAlphaId = alpha.GetAlphaId();
            alpha.SetSupperAlphaContainment(this);
        }
        public void SetSubAlpha(Alpha alpha)
        {
            SubAlpha = alpha;
            SubAlphaId = alpha.GetAlphaId();
            alpha.AddSubordinateAlphaContainment(this);
        }
    }
}
