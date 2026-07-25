using Alpha.Enums;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Interfaces
{
    public interface ICriterion
    {
        CriterionTypeEnum CriterionTypeEnumValue { get; set; }
        bool Partial { get; set; }
        int Minimal { get; set; }
        Guid Id { get; set; }
        CriterionTypeEnum GetCriterionType();
        Guid GetId();
        IDetailing GetDetail();
        bool GetPartial();
        int GetMinimal();
        Activity GetActivity();
        Guid GetActivityId();
        Guid GetDetailId();
        void SetDetail(IDetailing detail);
        void SetPartial(bool partial);
        void SetMinimal(int minimal);
    }
}
