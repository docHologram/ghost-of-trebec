using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostOfTrebec.Core.InnerCore
{
    public interface IRepository<TEntity>
    {
        Task<IReadOnlyList<TEntity>> FindAsync(Specification<TEntity> specification);
        Task SaveAsync(TEntity entity);
    }
}
