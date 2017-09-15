using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [Serializable]
    public class NewGuid:ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                activity.Text = Guid.NewGuid().ToString();
            }
            context.Done(activity);
        }

        public NewGuid()
        {
            CommandsName = new List<string>() {"/newguid", "/guid"};
            Description = "Получить уникальный идентификатор";
        }
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}