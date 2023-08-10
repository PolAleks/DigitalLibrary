using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public class UserRepository
    {
        private AppContext _context;

        public UserRepository(AppContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateNameById(int id, string name)
        {
            User? user = _context.Users.FirstOrDefault(user => user.Id == id);
            if (user is not null)
            {
                user.Name = name;
                _context.SaveChanges();
            }
        }
        public void Delete(User user) 
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
        /// <summary>
        /// Возвращает кол-во книг на руках у пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetCountBooksIssued(int id)
        {
            if (_context.Users.Find(id) is null) 
                return 0;
            return _context.Users.Find(id).Books.Count();
        }
    }
}
