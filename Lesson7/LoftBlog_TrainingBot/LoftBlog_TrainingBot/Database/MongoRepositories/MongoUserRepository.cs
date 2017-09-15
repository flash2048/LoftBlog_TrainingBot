using System.Collections.Generic;
using System.Configuration;
using LoftBlog_TrainingBot.Models;
using MongoDB.Driver;

namespace LoftBlog_TrainingBot.Database.MongoRepositories
{
    public class MongoUserRepository: IRepository<User>
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        public MongoUserRepository()
        {
            _client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            _database = _client.GetDatabase("loftblogbot");
        }

        public void Dispose()
        {
        }

        private IMongoCollection<User> Users
        {
            get { return _database.GetCollection<User>("users"); }
        }

        public IEnumerable<User> GetAll()
        {
            return Users.Find(_ => true).ToList();
        }

        public User GetById(int id)
        {
            return Users.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Add(User user)
        {
            int id = 0;
            var lastUser = Users.Find(_ => true).SortByDescending(x => x.Id).FirstOrDefault();
            if (lastUser != null)
            {
                id = lastUser.Id+1;
            }
            user.Id = id;
            Users.InsertOne(user);
        }

        public void Update(User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
            Users.ReplaceOne(filter, user);
        }

        public void Delete(int id)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, id);
            Users.DeleteOne(filter);
        }

        public void Save()
        {
        }
    }
}