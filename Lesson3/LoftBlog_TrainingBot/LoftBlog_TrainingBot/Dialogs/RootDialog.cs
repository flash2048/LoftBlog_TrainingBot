using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity?.Text != null)
            {
                var service = new BotService();

                var replyToConversation = service.Run(activity);
                await context.PostAsync(replyToConversation.Result.Text);
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}