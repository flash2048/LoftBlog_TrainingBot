using System;
using System.Collections.Generic;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    public class ForAll : ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public Activity Run(Activity activity)
        {
            if (activity?.Conversation != null)
            {
                BotService.SendForAll(activity.Text);
                activity.Text = "Сообщение отправлено всем доступным пользователям";
            }
            return activity;
        }

        public ForAll()
        {
            CommandsName = new List<string>() {"/forall"};
            Description = "Отправляет сообщение всем пользователям";
        }
    }
}