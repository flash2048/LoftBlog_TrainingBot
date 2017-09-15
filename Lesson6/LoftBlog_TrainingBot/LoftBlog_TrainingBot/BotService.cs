using System;
using LoftBlog_TrainingBot.Models;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot
{
    public static class BotService
    {
        public static void SendForUser(User user, string message)
        {
            if (user != null)
            {
                var client = new ConnectorClient(new Uri(user.ServiceUrl));

                var userAccount = new ChannelAccount(user.UserId, user.UserName);
                var botAccount = new ChannelAccount(user.FromId, user.FromName);

                var activity = new Activity
                {
                    Conversation = new ConversationAccount(id: user.UserId),
                    ChannelId = user.ChannelId,
                    From = userAccount,
                    Recipient = botAccount,
                    Text = message,
                    Id = user.Conversation
                };

                var reply = activity.CreateReply(message);
                if(!String.IsNullOrEmpty(activity.ServiceUrl)){
                    MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                }
                MicrosoftAppCredentials.TrustServiceUrl(user.ServiceUrl);
                client.Conversations.ReplyToActivity(reply);
            }
        }

        public static void SendForAll(string message)
        {
            foreach (var user in DataModel.Users)
            {
                SendForUser(user, message);
            }
        }

        public static string STR
        {
            get { return "sdfsdf"; }
        }
    }
}