using System.Collections.Generic;
using System.Linq;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Models
{
    public class DataModel
    {
        private static List<User> _users;

        public static List<User> Users
        {
            get
            {
                return _users ?? (_users = new List<User>());
            }
            set { _users = value; }
        }

        public static void RememberUser(Activity activity)
        {
            if (activity != null)
            {
                var hasUser = Users.Any(x => x.ChannelId == activity.ChannelId && x.UserId == activity.From.Id &&
                                             x.UserName == activity.From.Name);
                if (!hasUser)
                {
                    Users.Add(new User(activity));
                }
            }
        }
    }
}