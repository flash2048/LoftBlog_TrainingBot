using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Attributes;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [NotShowInHelp]
    [Serializable]
    public class Help: ITool
    {
        public string Description { get; set; } 
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }

        protected string CommandText { get; set; }

        public Help()
        {
            Description = "Оказание помощи";
            CommandsName = new List<string>() { "/help" };
        }

        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;


            var resultStr = new StringBuilder();
            var baseInterfaceType = typeof(ITool);
            var botCommands = Assembly.GetAssembly(baseInterfaceType)
                .GetTypes()
                .Where(types => types.IsClass && !types.IsAbstract && types.GetInterface("ITool") != null);
            foreach (var botCommand in botCommands)
            {
                if (!botCommand.GetCustomAttributes(typeof(NotShowInHelpAttribute)).Any())
                {
                    var command = (ITool)Activator.CreateInstance(botCommand);
                    resultStr.Append($"{command.CommandsName.First()} - {command.Description}\n\r");
                }
            }
            CommandText = resultStr.ToString();


            if (activity?.Conversation != null)
            {
                activity.Text = CommandText;
            }
            context.Done(activity);
        }


        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}