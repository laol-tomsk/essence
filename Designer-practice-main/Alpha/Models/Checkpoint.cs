using Alpha.Interfaces;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Enums;


namespace Alpha
{
    public class Checkpoint : ICheckable
    {       
        public string Name { get; set; }   
        public string Description { get; set; }
        public int Order { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SpecialId { get; set; } = null;
        public int TimeEstimate { get; set; }
        public int RelativeWeight { get; set; }
        public string TaskName { get; set; }
        public DegreeOfEvidenceEnum DegreeOfEvidenceEnumValueManagerOpinion { get; set; } = DegreeOfEvidenceEnum.Medium;
        public DegreeOfEvidenceEnum GetDegreeOfEvidenceEnumValueManagerOpinion() => DegreeOfEvidenceEnumValueManagerOpinion;
        public Guid DetailId { get; set; }
        private List<DegreeOfEvidence> DegreeOfEvidences { get; set; } = new List<DegreeOfEvidence>();
        public Checkpoint()
        {

        }
        public Checkpoint(string name,string description,int order, IDetailing detail,string specialId, int timeEstimate, string taskName, int relativeWeight)
        {
            Name = name;
            Description = description;
            Order = order;
            DetailId = detail.GetId();
            SpecialId = specialId;
            TimeEstimate = timeEstimate;
            TaskName = taskName;
            RelativeWeight = relativeWeight;
        }
        public Guid GetId() => Id;
        public string GetSpecialId() => SpecialId;
        public Guid GetDetailId() => DetailId;
        public string GetName() => Name;
        public string GetDescription() => Description;
        public List<DegreeOfEvidence> GetDegreeOfEvidences() => DegreeOfEvidences;
        public void AddDegreeOfEvidence(DegreeOfEvidence degreeOfEvidence)
        {
            DegreeOfEvidences.Add(degreeOfEvidence);
        }
        public void RemoveDegreeOfEvidence(DegreeOfEvidence degreeOfEvidence)
        {
            DegreeOfEvidences.Remove(degreeOfEvidence);
        }
        public void SetSpecialId(string specialId)
        {
            SpecialId = specialId;
        }

        public void SetDegreeOfEvidenceEnumManagerOpinion(DegreeOfEvidenceEnum degreeOfEvidenceEnum)
        {
            DegreeOfEvidenceEnumValueManagerOpinion = degreeOfEvidenceEnum;
        }
    }
}
