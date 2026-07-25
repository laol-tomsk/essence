using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Interfaces
{
    public interface IDetailing
    {
        string Name { get; set; }
        Guid Id { get; set; }
        string GetName();
        List<Checkpoint> GetCheckpoints();
        void AddCheckpoint(Checkpoint checkpoint);
        void RemoveCheckpoint(Checkpoint checkpoint);
        Guid GetId();
        void SortListOfCheckpointsByOrder();
        IBaseObject GetBaseObject();
    }
}
