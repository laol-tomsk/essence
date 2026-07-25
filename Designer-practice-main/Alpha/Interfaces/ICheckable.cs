using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha
{
    public interface ICheckable
    {
        Guid Id { get; set; }
        string SpecialId { get; set; }
        string Name { get; set; }

        string GetName();
        Guid GetId();
        string GetSpecialId();
    }
}
