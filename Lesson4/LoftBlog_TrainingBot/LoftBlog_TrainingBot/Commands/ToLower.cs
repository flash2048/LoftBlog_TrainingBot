using System.Collections.Generic;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    public class ToLower:ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public Activity Run(Activity activity)
        {
            if (activity?.Conversation != null)
            {
                activity.Text = activity.Text.ToLower();
            }
            return activity;
        }

        public ToLower()
        {
            CommandsName = new List<string>() {"/tolower"};
            Description = "Приводит текст к нижнему регистру";
        }
    }
}