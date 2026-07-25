using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Enums;

namespace Alpha.Models
{
    public class DegreeOfEvidence
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ICheckableId { get; set; }
        public bool TypeOfEvidence { get; set; } = false;
        private ICheckable ICheckableObject { get; set; }
        private Checkpoint Checkpoint { get; set; }
        public Guid CheckpointId { get; set; }
        public DegreeOfEvidenceEnum DegreeOfEvidenceEnumValue { get; set; }

        public DegreeOfEvidenceEnum GetDegreeOfEvidenceEnumValue() => DegreeOfEvidenceEnumValue;
        public bool GetTypeOfEvidence() => TypeOfEvidence;
        public Guid GetICheckableId() => ICheckableId;
        public Guid GetId() => Id;
        public ICheckable GetICheckable() => ICheckableObject;
        public string GetICheckableSpecialId() => ICheckableObject.GetSpecialId();
        public Guid GetCheckpointId() => CheckpointId;

        public DegreeOfEvidence()
        {

        }
        public DegreeOfEvidence(Checkpoint checkpoint,ICheckable icheckable,bool typeOfEvidence, DegreeOfEvidenceEnum degreeOfEvidenceEnum) 
        {
            Checkpoint = checkpoint;
            CheckpointId = checkpoint.GetId();
            ICheckableObject = icheckable;
            ICheckableId = icheckable.GetId();
            TypeOfEvidence = typeOfEvidence;
            DegreeOfEvidenceEnumValue = degreeOfEvidenceEnum;
        }
        public void SetICheckable(ICheckable icheckable)
        {
            ICheckableObject = icheckable;
            ICheckableId = icheckable.GetId();
        }
        public void SetTypeOfEvidence(bool typeOfEvidence)
        {
            TypeOfEvidence = typeOfEvidence;
        }
        public void SetDegreeOfEvidenceEnum(DegreeOfEvidenceEnum degreeOfEvidenceEnum)
        {
            DegreeOfEvidenceEnumValue = degreeOfEvidenceEnum;
        }
    }
}
