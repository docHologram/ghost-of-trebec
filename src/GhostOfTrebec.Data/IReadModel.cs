using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostOfTrebec.Data
{
    public interface IReadModel<T>
    {
        IQueryable<T> Query();
    }
}
