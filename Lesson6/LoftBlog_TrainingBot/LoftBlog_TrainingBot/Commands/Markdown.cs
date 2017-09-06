using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Interfaces;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [Serializable]
    public class Markdown : ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                var str = new StringBuilder();
                str.Append("**Жирный текст**\n\r");
                str.Append("*Курсив*\n\r");
                str.Append("# Заголовок\n\r");
                str.Append("~~Перечеркнутый текст~~\n\r");
                str.Append("---\n\r");
                str.Append("* Элемент не сортированного списка\n\r");
                str.Append("1. Элемент сортированного списка\n\r");
                str.Append("`Преформатированный текст`\n\r");
                str.Append("> Цитата\n\r");
                str.Append("[Ссылка](http://www.bing.com)\n\r");
                str.Append("![Изображение](http://aka.ms/Fo983c)\n\r");
                activity.Text = str.ToString();
            }
            context.Done(activity);
        }

        public Markdown()
        {
            CommandsName = new List<string>() { "/markdown" };
            Description = "Выводит доступные оформления текста";
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}