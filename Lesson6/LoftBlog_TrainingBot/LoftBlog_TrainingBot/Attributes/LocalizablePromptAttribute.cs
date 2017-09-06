using System;
using System.Reflection;
using Microsoft.Bot.Builder.FormFlow;

namespace LoftBlog_TrainingBot.Attributes
{
    [Serializable]
    public class LocalizablePromptAttribute : PromptAttribute
    {
        private readonly PropertyInfo _nameOfProperty;

        public LocalizablePromptAttribute(string descriptionKey, Type resourceType, string[] patterns = null)
            : base(patterns)
        {
            if (resourceType != null)
            {
                _nameOfProperty = resourceType.GetProperty(descriptionKey,
                    BindingFlags.Static | BindingFlags.Public);
            }

            if (_nameOfProperty != null)
            {
                Patterns = new[] { (string)_nameOfProperty.GetValue(_nameOfProperty.DeclaringType, null) };
            }

        }
    }
}