using System;
using System.Collections.Generic;
using System.Linq;
using LoftBlog_TrainingBot.Database.MongoRepositories;
using LoftBlog_TrainingBot.Database.SqlRepositories;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Models
{
    public class DataModel
    {

        public static List<User> Users
        {
            get
            {
                var db = new MongoUserRepository();
                return db.GetAll().ToList();
            }
        }

        public static User RememberUser(Activity activity)
        {
            if (activity != null)
            {
                try
                {
                    var db = new MongoUserRepository();
                   
                    var user =  db.GetAll().FirstOrDefault(x => x.ChannelId == activity.ChannelId && x.UserId == activity.From.Id &&
                                                 x.UserName == activity.From.Name);
                    if (user == null)
                    {
                        user = new User(activity);
                        db.Add(user);
                    }
                    return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
               
            }
            return null;
        }
    }
}