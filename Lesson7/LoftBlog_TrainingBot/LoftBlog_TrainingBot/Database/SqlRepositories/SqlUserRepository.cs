using System;
using System.Collections.Generic;
using System.Data.Entity;
using LoftBlog_TrainingBot.Models;

namespace LoftBlog_TrainingBot.Database.SqlRepositories
{
    public class SqlUserRepository: IRepository<User>
    {
        private BotDbContext _context;

        public SqlUserRepository()
        {
            _context = new BotDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}