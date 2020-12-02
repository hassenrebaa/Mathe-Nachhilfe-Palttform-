using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models.Respositories
{
    public class DokumentRespository : IDokumentstoreRepository<dokument>
    {
        dokumentstorDbContext _db;
        public DokumentRespository( dokumentstorDbContext db)
        {
            this._db = db;
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
            var Dokument = _db.Dokuments.SingleOrDefault(b => b.id == id);
            return Dokument;
        }

        public IList<dokument> List()
        {
            return _db.Dokuments.ToList();
        }
        public List<dokument> Search(string term)
        {
            return _db.Dokuments.Where(a => a.user.nachname.Contains(term)).ToList();
        }

        public void Update(int id,dokument newDokument)
        {
            var dokument = Find(id);
            dokument.title = newDokument.title;
            dokument.description = newDokument.description;
            dokument.user = dokument.user;
            dokument.DokumentUrl = newDokument.DokumentUrl;

            _db.Update(newDokument);
            _db.SaveChanges(); 
        }
    }
}
