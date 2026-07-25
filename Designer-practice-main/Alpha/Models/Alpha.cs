using Alpha.Interfaces;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha
{
    public class Alpha : IBaseObject
    {
        // TODO Parent contains only ID
        private Alpha Parent { get; set; } = null;
        public Guid? ParentAlphaId { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsKey { get; set; }
        private List<State> States { get; set; } = new List<State>();
        private AlphaContaiment SupperAlphaContaiment { get; set; } = null;
        private List<AlphaContaiment> SubordinateAlphaConteinments { get; set; } = new List<AlphaContaiment>();
        private WorkProductManifest WorkProductManifest { get; set; } = null;
       
        public Alpha()
        {
        }
        public Alpha(string name, string description)
        {
            Name = name;
            Description = description;
            IsKey = false;
        }
        public Alpha(string name, string description, Alpha parent, bool isKey)
        {
            Name = name;
            Description = description;
            Parent = parent;
            IsKey = false;
            if (parent != null) { 
                ParentAlphaId = parent.GetAlphaId();
                IsKey = isKey;
            }
        }
        public string GetName() => Name;
        public string GetDescription() => Description;
        public List<State> GetStates() => States;
        public Guid GetAlphaId() => Id;
        public WorkProductManifest GetWorkProductManifest() => WorkProductManifest;
        public AlphaContaiment GetSupperAlphaContainment() => SupperAlphaContaiment;
        public List<AlphaContaiment> GetSubordinateAlphaConteinments() => SubordinateAlphaConteinments;
        public Alpha GetAlphaParent() => Parent;
        public Guid? GetAlphaParentId() => ParentAlphaId;
        public void AddState(State state)
        {
            States.Add(state);
        }
        public void SortListOfStatesByOrder()
        {
            States.Sort((x,y)=>x.Order.CompareTo(y.Order));
        }
        public void SetSupperAlphaContainment(AlphaContaiment alphaContaiment)
        {
            SupperAlphaContaiment = alphaContaiment;
        }
        public void AddSubordinateAlphaContainment(AlphaContaiment alphaContaiment)
        {
            SubordinateAlphaConteinments.Add(alphaContaiment);
        }
        public void DeleteSupperAlphaContainment()
        {
            SupperAlphaContaiment = null;
        }
        public void DeleteSubordinateAlphaContainment(AlphaContaiment alphaContaiment)
        {
            SubordinateAlphaConteinments.Remove(alphaContaiment);
        }
        public void SetWorkProductManifest(WorkProductManifest workProductManifest)
        {
            this.WorkProductManifest = workProductManifest;
        }
        public void DeleteWorkProductManifest()
        {
            WorkProductManifest = null;
        }
        public void SetParentAlpha(Alpha alpha)
        {
            Parent = alpha;
            ParentAlphaId = alpha.GetAlphaId();
        }
        public void DeleteAlphaParent()
        {
            ParentAlphaId = null;
            Parent = null;
        }
    }
}
