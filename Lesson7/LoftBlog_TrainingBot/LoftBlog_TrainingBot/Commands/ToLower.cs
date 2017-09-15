using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [Serializable]
    public class ToLower:ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                if (!String.IsNullOrEmpty(activity.Text))
                {
                    activity.Text = activity.Text.ToLower();
                    context.Done(activity);
                }
                else
                {
                    await context.PostAsync("Введи текст");
                    context.Wait(Run);
                }
            }
        }

        public ToLower()
        {
            CommandsName = new List<string>() {"/tolower"};
            Description = "Приводит текст к нижнему регистру";
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}