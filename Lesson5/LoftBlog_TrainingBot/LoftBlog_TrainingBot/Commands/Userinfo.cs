using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoftBlog_TrainingBot.Interfaces;
using LoftBlog_TrainingBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace LoftBlog_TrainingBot.Commands
{
    [Serializable]
    public class UserinfoDialog : ITool
    {
        public string Description { get; set; }
        public List<string> CommandsName { get; set; }
        public bool IsAdmin { get; set; }
        
        public UserDto UserDto;

        private UserInfoState? _state;

        public UserinfoDialog()
        {
            Description = "Получает информацию о пользователе";
            CommandsName = new List<string>() { "/name" };
            UserDto = new UserDto();
        }

        public virtual async Task Run(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            if (activity?.Conversation != null)
            {
                if (!String.IsNullOrEmpty(activity.Text) && _state != null)
                {
                    switch (_state)
                    {
                        case UserInfoState.FirstName:
                            UserDto.FirstName = activity.Text;break;
                        case UserInfoState.LastName:
                            UserDto.LastName = activity.Text; break;
                        case UserInfoState.Language:
                            UserDto.Language = activity.Text; break;
                    }
                }

                if (String.IsNullOrEmpty(UserDto.FirstName))
                {
                    await context.PostAsync("Введи имя");
                    _state = UserInfoState.FirstName;
                    context.Wait(Run);
                    return;
                }
                if (String.IsNullOrEmpty(UserDto.LastName))
                {
                    await context.PostAsync("Введи фамилию");
                    _state = UserInfoState.LastName;
                    context.Wait(Run);
                    return;
                }
                if (String.IsNullOrEmpty(UserDto.Language))
                {
                    await context.PostAsync("Введи любимый язык программирования");
                    _state = UserInfoState.Language;
                    context.Wait(Run);
                    return;
                }



                activity.Text = $"Введённые данные:\n\r{UserDto}";
                context.Done(activity);


            }
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.Run);
        }
    }
}