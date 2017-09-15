using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Commands;
using LoftBlog_TrainingBot.Interfaces;
using LoftBlog_TrainingBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private readonly List<ITool> _tools;
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public RootDialog()
        {
            _tools = new List<ITool>();
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var baseInterfaceType = typeof(ITool);
            var botCommands = Assembly.GetAssembly(baseInterfaceType)
                .GetTypes()
                .Where(types => types.IsClass && !types.IsAbstract && types.GetInterface("ITool") != null);
            foreach (var botCommand in botCommands)
            {
                _tools.Add((ITool)Activator.CreateInstance(botCommand));
            }

            var activity = await result as Activity;
            var user = DataModel.RememberUser(activity);
            if (!String.IsNullOrEmpty(activity?.Text))
            {
                var str = activity.Text.Trim();
                var indexOfSpace = str.IndexOf(" ", StringComparison.Ordinal);
                var command = indexOfSpace != -1 ? str.Substring(0, indexOfSpace).ToLower() : str.ToLower();
                if (command[0] != '/')
                {
                    command = "/" + command;
                }

                var tool = _tools.FirstOrDefault(x => x.CommandsName.Any(y => y.Equals(command)));
                if (tool != null)
                {
                    activity.Text = indexOfSpace >= 0 ? activity.Text.Substring(indexOfSpace, str.Length - indexOfSpace) : String.Empty;
                    var typeOfDialog = tool.GetType();
                    var newTool = Activator.CreateInstance(typeOfDialog);
                    if (user == null || (!user.IsAdmin && ((ITool) newTool).IsAdmin))
                    {
                        await context.Forward(new Help(), ResumeAfterNewOrderDialog, activity, CancellationToken.None);
                        return;
                    }
                    await context.Forward((IDialog<object>)newTool, ResumeAfterNewOrderDialog, activity, CancellationToken.None);
                }
                else
                {
                    await context.Forward(new Help(), ResumeAfterNewOrderDialog, activity, CancellationToken.None);
                }
            }
        }

        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<object> result)
        {

            var activity = await result as Activity;
            if (activity != null)
            {
                var reply = activity.CreateReply(activity.Text);
                if (activity.Attachments != null)
                {
                    reply.Attachments = activity.Attachments;
                }

                await context.PostAsync(reply);
            }
        }

    }
}