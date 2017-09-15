using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Interfaces;
using LoftBlog_TrainingBot.obj;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [Serializable]
    public class ForAll : ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; } = true;
        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {

                if (!String.IsNullOrEmpty(activity.Text))
                {
                    BotService.SendForAll(activity.Text);
                    activity.Text = "Сообщение отправлено всем доступным пользователям";
                    context.Done(activity);
                }
                else
                {
                    await context.PostAsync("Введи текст, который будет отправлен всем");
                    context.Wait(Run);
                }
                
            }
        }

        public ForAll()
        {
            CommandsName = new List<string>() {"/forall"};
            Description = "Отправляет сообщение всем пользователям";
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}