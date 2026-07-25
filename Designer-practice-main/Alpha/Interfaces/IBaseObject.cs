using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Interfaces
{
    public interface IBaseObject
    {
        string Name { get; set; }
        string Description { get; set; }

        string GetName();
        string GetDescription();
    }
}
