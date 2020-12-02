using Mathe_Nachhilfe_Plattform.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models.Respositories
{
    public class DokumentDbRepository : IDokumentstoreRepository<dokument>
    {
        dokumentstorDbContext _db;
        public DokumentDbRepository(dokumentstorDbContext db)
        {
            _db = db;
        }
        public void Add(dokument entity)
        {
            entity.id = _db.Dokuments.Max(b => b.id) + 1;
            _db.Dokuments.Add(entity);
            _db.SaveChanges();

        }

        public void Delete(int id)
        {
            var Dokument = Find(id);
            _db.Dokuments.Remove(Dokument);
            _db.SaveChanges();
        }

        public dokument Find(int id)
        {
            var Dokument = _db.Dokuments.Include(a => a.user).SingleOrDefault(b => b.id == id);
            return Dokument;
        }

        public IList<dokument> List()
        {
            //return _db.Dokuments.Include(a => a.user).ToList();
            return _db.Dokuments.ToList();
        }

        public void Update(int id, dokument newDokument)
        {


            _db.Update(newDokument);
            _db.SaveChanges();
        }
        public List<dokument> Search(string term)
        {
            var result = _db.Dokuments.Include(a => a.user)
                .Where(b => b.title.Contains(term)
                || b.description.Contains(term)
                || b.user.nachname.Contains(term)).ToList();

            return result;
        }
    }
}
