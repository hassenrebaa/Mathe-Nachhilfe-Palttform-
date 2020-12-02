using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models.Respositories
{
    public interface IDokumentstoreRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id,TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);

    }
}
