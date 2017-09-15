using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Interfaces
{
    interface ITool: IDialog<object>
    {
        string Description { get; set; }
        List<string> CommandsName { get; set; }
        bool IsAdmin { get; set; }
        Task Run(IDialogContext context, IAwaitable<IMessageActivity> result);

    }
}
