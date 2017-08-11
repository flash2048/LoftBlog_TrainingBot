using System;
using System.Collections.Generic;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    public class NewGuid:ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public Activity Run(Activity activity)
        {
            if (activity?.Conversation != null)
            {
                activity.Text = Guid.NewGuid().ToString();
            }
            return activity;
        }

        public NewGuid()
        {
            CommandsName = new List<string>() {"/newguid", "/guid"};
            Description = "Получить уникальный идентификатор";
        }
    }
}