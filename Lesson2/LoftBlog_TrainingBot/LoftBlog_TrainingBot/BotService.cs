using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Commands;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot
{
    public class BotService
    {
        private readonly List<ITool> _tools;

        public BotService()
        {
            _tools = new List<ITool>();
            _tools.Add(new ToUpper());
            _tools.Add(new ToLower());
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
                    activity.Text = activity.Text.Substring(indexOfSpace, str.Length - indexOfSpace);
                    activity = tool.Run(activity);
                }
                else
                {
                    activity = help.Run(activity);
                }
            }
            return activity;
        }
    }
}