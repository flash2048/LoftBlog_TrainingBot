using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Commands;
using LoftBlog_TrainingBot.Interfaces;
using LoftBlog_TrainingBot.Models;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot
{
    public class BotService
    {
        private readonly List<ITool> _tools;

        public BotService()
        {
            _tools = new List<ITool>();

            var baseInterfaceType = typeof(ITool);
            var botCommands = Assembly.GetAssembly(baseInterfaceType)
                .GetTypes()
                .Where(types => types.IsClass && !types.IsAbstract && types.GetInterface("ITool") != null);
            foreach (var botCommand in botCommands)
            {
                _tools.Add((ITool)Activator.CreateInstance(botCommand));
            }
        }

        public async Task<Activity> Run(Activity activity)
        {
            if (!String.IsNullOrEmpty(activity.Text))
            {
                var str = activity.Text.Trim();
                var indexOfSpace = str.IndexOf(" ", StringComparison.Ordinal);
                var command = indexOfSpace != -1 ? str.Substring(0, indexOfSpace).ToLower() : str.ToLower();
                if (command[0] != '/')
                {
                    command = "/" + command;
                }

                var help = new Help();

                var tool = _tools.FirstOrDefault(x => x.CommandsName.Any(y => y.Equals(command)));
                if (tool != null)
                {
                    activity.Text = indexOfSpace >= 0 ? activity.Text.Substring(indexOfSpace, str.Length - indexOfSpace) : String.Empty;
                    activity = tool.Run(activity);
                }
                else
                {
                    activity = help.Run(activity);
                }
            }
            var reply = activity.CreateReply(activity.Text);
            if (activity.Attachments != null)
            {
                reply.Attachments = activity.Attachments;
            }
            return reply;
        }


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
    }
}