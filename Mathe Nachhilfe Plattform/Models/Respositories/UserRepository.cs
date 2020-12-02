using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models.Respositories
{
    public class UserRepository : IDokumentstoreRepository<user>
    {
        IList<user> users; 
        public UserRepository()
        {
            users = new List<user>()
            {
                new user{ id=1,nachname="Rebaa",vorname="hassen",adresse="Dortmund",email="hassen@gmail.com",password="123" }
            };
        }
        public void Add(user entity)
        {
            users.Add(entity);

        }

        public void Delete(int id)
        {
            var User = Find(id);
            users.Remove(User);
        }

        public user Find(int id)
        {
            var user = users.SingleOrDefault(a => a.id ==id);

            return user;
        }

        public IList<user> List()
        {
            return users;
                
        }

        public List<user> Search(string term)
        {
            return users.Where(a => a.nachname.Contains(term)).ToList();
        }

        public void Update(int id, user newUser)
        {
            var user = Find(id);
            user.mobile = newUser.mobile;
            user.nachname = newUser.nachname;
            user.vorname = newUser.vorname;
            user.adresse = newUser.adresse;
            user.email = newUser.email;
            user.password = newUser.password;



        }
    }
}
