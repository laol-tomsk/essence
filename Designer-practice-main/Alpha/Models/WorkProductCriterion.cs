using Alpha.Enums;
using Alpha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class WorkProductCriterion : ICriterion
    {
        public CriterionTypeEnum CriterionTypeEnumValue { get; set; }
        public bool Partial { get; set; }
        public int Minimal { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        private LevelOfDetail LevelOfDetail { get; set; } = null;
        public Guid LevelOfDetailId { get; set; }
        private Activity Activity { get; set; } = null;
        public Guid ActivityId { get; set; }
        public Activity GetActivity() => Activity;
        public CriterionTypeEnum GetCriterionType() => CriterionTypeEnumValue;
        public bool GetPartial() => Partial;
        public int GetMinimal() => Minimal;
        public Guid GetActivityId() => ActivityId;
        public Guid GetLevelOfDetailId() => LevelOfDetailId;
        public Guid GetId() => Id;
        public LevelOfDetail GetLevelOfDetail() => LevelOfDetail;
        public IDetailing GetDetail() => LevelOfDetail;
        public Guid GetDetailId() => LevelOfDetailId;
        public WorkProductCriterion()
        {

        }
        public WorkProductCriterion(CriterionTypeEnum criterionTypeEnum,bool partial, int minimal, Activity activity,LevelOfDetail levelOfDetail)
        {
            CriterionTypeEnumValue = criterionTypeEnum;
            Partial = partial;
            Minimal = minimal;
            Activity = activity;
            ActivityId = activity.GetId();
            LevelOfDetail = levelOfDetail;
            LevelOfDetailId = LevelOfDetail.GetId();

        }
        public void SetActivity(Activity activity)
        {
            Activity = activity;
            ActivityId = activity.GetId();
            activity.AddWorkProductCriterion(this);
        }
        public void SetLevelOfDetail(LevelOfDetail levelOfDetail)
        {
            LevelOfDetail = levelOfDetail;
            LevelOfDetailId = levelOfDetail.GetId();
        }
        public void SetDetail(IDetailing detail)
        {
            LevelOfDetail = (LevelOfDetail)detail;
            LevelOfDetailId = detail.GetId();
        }
        public void SetPartial(bool partial)
        {
            Partial = partial;
        }
        public void SetMinimal(int minimal)
        {
            Minimal = minimal;
        }

    }
}
