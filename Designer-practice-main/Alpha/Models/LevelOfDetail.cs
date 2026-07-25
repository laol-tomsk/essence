using Alpha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class LevelOfDetail : IDetailing, ICheckable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SpecialId { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public int TimeEstimate { get; set; }
        public string TaskName { get; set; }
        private WorkProduct WorkProduct { get; set; }
        public Guid WorkProductId { get; set; }
        public int Order { get; set; }
        private List<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
        public List<Checkpoint> GetCheckpoints() => Checkpoints;
        public Guid GetId() => Id;
        public string GetSpecialId() => SpecialId;
        public string GetName() => Name;
        public string GetDescription() => Description;
        public int GetOrder() => Order;
        public Guid GetWorkProductId() => WorkProductId;
        public IBaseObject GetBaseObject() => WorkProduct;
        public LevelOfDetail()
        {

        }
        public LevelOfDetail(string name, string description, int order, WorkProduct workProduct, string specialId, int timeEstimate, string taskName)
        {
            Name = name;
            Description = description;
            Order = order;
            WorkProductId = workProduct.Id;
            WorkProduct = workProduct;
            SpecialId = specialId;
            TimeEstimate = timeEstimate;
            TaskName = taskName;
        }
        public void AddCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Add(checkpoint);
        }
        public void SortListOfCheckpointsByOrder()
        {
            Checkpoints.Sort((x, y) => x.Order.CompareTo(y.Order));
        }
        public void SetWorkProduct(WorkProduct workProduct)
        {
            WorkProduct = workProduct;
        }
        public void RemoveCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Remove(checkpoint);
        }
        public void SetSpecialId(string specialId)
        {
            SpecialId = specialId;
        }
    }
}
