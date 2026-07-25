using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class Activity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        private List<WorkProductCriterion> WorkProductCriterions { get; set; } = new List<WorkProductCriterion>();
        private List<AlphaCriterion> AlphaCriterions { get; set; } = new List<AlphaCriterion>();
        public string GetName() => Name;
        public string GetDescription() => Description;
        public Guid GetId() => Id;
        public List<WorkProductCriterion> GetWorkProductCriterions() => WorkProductCriterions;
        public List<AlphaCriterion> GetAlphaCriterions() => AlphaCriterions;


        public Activity()
        {

        }
        public Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void AddWorkProductCriterion(WorkProductCriterion workProductCriterion)
        {
            WorkProductCriterions.Add(workProductCriterion);
        }
        public void RemoveWorkProductCriterion(WorkProductCriterion workProductCriterion)
        {
            WorkProductCriterions.Remove(workProductCriterion);
        }
        public void AddAlphaCriterion(AlphaCriterion alphaCriterion)
        {
            AlphaCriterions.Add(alphaCriterion);
        }
        public void RemoveAlphaCriterion(AlphaCriterion alphaCriterion)
        {
            AlphaCriterions.Remove(alphaCriterion);
        }
    }
}
