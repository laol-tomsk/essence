using Alpha.Enums;
using Alpha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class AlphaCriterion : ICriterion
    {

        public CriterionTypeEnum CriterionTypeEnumValue { get; set; }
        public bool Partial { get; set; }
        public int Minimal { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        private State State { get; set; } = null;
        public Guid StateId { get; set; }
        private Activity Activity { get; set; } = null;
        public Guid ActivityId { get; set; }

        public Activity GetActivity() => Activity;
        public CriterionTypeEnum GetCriterionType() => CriterionTypeEnumValue;
        public bool GetPartial() => Partial;
        public int GetMinimal() => Minimal;
        public Guid GetActivityId() => ActivityId;
        public Guid GetStateId() => StateId;
        public Guid GetId() => Id;
        public State GetState() => State;
        public IDetailing GetDetail() => State;
        public Guid GetDetailId() => StateId;
        public AlphaCriterion()
        {

        }
        public AlphaCriterion(CriterionTypeEnum type, bool partial, int minimal, Activity activity, State state)
        {
            CriterionTypeEnumValue = type;
            Partial = partial;
            Minimal = minimal;
            Activity = activity;
            ActivityId = activity.GetId();
            State = state;
            StateId = state.GetId();
        }
        public void SetActivity(Activity activity)
        {
            Activity = activity;
            ActivityId = activity.GetId();
            activity.AddAlphaCriterion(this);
        }
        public void SetState(State state)
        {
            State = state;
            StateId = state.GetId();
        }
        public void SetDetail(IDetailing detail)
        {
            State = (State)detail;
            StateId = detail.GetId();
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



