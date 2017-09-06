using System;
using System.Reflection;
using Microsoft.Bot.Builder.FormFlow;

namespace LoftBlog_TrainingBot.Attributes
{
    [Serializable]
    public class LocalizableDescribeAttribute: DescribeAttribute
    {
        private readonly PropertyInfo _nameOfProperty;

        public LocalizableDescribeAttribute(string descriptionKey, Type resourceType, string description = null, string image = null, string message = null, string title = null, string subTitle = null)
            : base(description, image,message, title, subTitle)
        {
            if (resourceType != null)
            {               
                _nameOfProperty = resourceType.GetProperty(descriptionKey,
                    BindingFlags.Static | BindingFlags.Public);
            }

            if (_nameOfProperty != null)
            {
                Description = (string)_nameOfProperty.GetValue(_nameOfProperty.DeclaringType, null);
            }
            
        }
    }
}