using System;
using System.Collections.Generic;
using LoftBlog_TrainingBot.Attributes;

using Microsoft.Bot.Builder.FormFlow;

namespace LoftBlog_TrainingBot.Models
{
    public enum SexOptions
    {
        Male,
        Female
    };

    
    public enum LangOptions
    {
        Russian,
        English,
        Spanish,
        Italian,
        German,
        Chinese,
        Other
    };

    public enum MessengerOptions
    {
        Skype,
        Telegram,
        Viber,
        WhatsApp,
        Bleep,
        FacebookMessenger,
        Icq
    };

    [Serializable]
    public class InterviewInfo
    {
        [LocalizableDescribe("Name", typeof(Resources.Resources))]
        [LocalizablePrompt("SelectNeed", typeof(Resources.Resources))]
        public string Name;
        [LocalizableDescribe("Sex", typeof(Resources.Resources))]
        public SexOptions? Sex;
        [LocalizableDescribe("Language", typeof(Resources.Resources))]
        public LangOptions? Language;
        [LocalizableDescribe("Age", typeof(Resources.Resources))]
        [Numeric(18, 100)]
        public int Age = 18;
        [LocalizableDescribe("Messenger", typeof(Resources.Resources))]
        public List<MessengerOptions> Messenger;

        public static IForm<InterviewInfo> BuildForm()
        {
            return new FormBuilder<InterviewInfo>().Build();
        }
    };
}