using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models.Respositories
{
    public class UserDbRepository : IDokumentstoreRepository<user>
    {
        dokumentstorDbContext _db;
        public UserDbRepository(dokumentstorDbContext _db)
        {
            this._db = _db;
        }
        public void Add(user entity)
        {
            _db.users.Add(entity);
            _db.SaveChanges();

        }
        
        public void Delete(int id)
        {
            var User = Find(id);
            _db.users.Remove(User);
            _db.SaveChanges();
        }

        public user Find(int id)
        {
            var user = _db.users.SingleOrDefault(a => a.id == id);

            return user;
        }

        public IList<user> List()
        {
            return _db.users.ToList();

        }
        public List<user> Search(string term )
        {
            return _db.users.Where(a => a.nachname.Contains(term)).ToList();
        }


        public void Update(int id, user newUser)
        {
            _db.Update(newUser);
            _db.SaveChanges();

        }
    }
}


