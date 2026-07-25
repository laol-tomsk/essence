using Alpha.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Models
{
    public class WorkProduct : IBaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        private List<WorkProductManifest> WorkProductManifests { get; set; } = new List<WorkProductManifest>();
        private List<LevelOfDetail> LevelOfDetails { get; set; } = new List<LevelOfDetail>();
        public WorkProduct()
        {

        }
        public WorkProduct(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string GetName() => Name;

        public string GetDescription() => Description;

        public Guid GetWorkProductId() => Id;
        public List<WorkProductManifest> GetWorkProductManifests() => WorkProductManifests;
        public List<LevelOfDetail> GetLevelOfDetails() => LevelOfDetails;
        public void SetLevelOfDetails(List<LevelOfDetail> levelOfDetails)
        {
            LevelOfDetails.AddRange(levelOfDetails);
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
        public void SetDescription(string description)
        {
            this.Description = description;
        }
        public void AddWorkProductManifest(WorkProductManifest workProductManifest)
        {
            WorkProductManifests.Add(workProductManifest);
        }
        public void DeleteWorkProductManifest(WorkProductManifest workProductManifest)
        {
            WorkProductManifests.Remove(workProductManifest);
        }
        public void AddLevelOfDetailToList(LevelOfDetail levelOfDetail)
        {
            LevelOfDetails.Add(levelOfDetail);
        }
        public void SortListOfLevelOfDetailsByOrder()
        {
            LevelOfDetails.Sort((x, y) => x.Order.CompareTo(y.Order));
        }
        public void RemoveLevelOfDetail(LevelOfDetail levelOfDetail)
        {
            LevelOfDetails.Remove(levelOfDetail);
        }
        public void RemoveWorkProductManifest(WorkProductManifest workProductManifest)
        {
            WorkProductManifests.Remove(workProductManifest);
        }
    }
}
