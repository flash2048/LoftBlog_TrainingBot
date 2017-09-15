using System;

namespace LoftBlog_TrainingBot.Models
{
    [Serializable]
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Language { get; set; }


        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Language}";
        }
    }

    public enum UserInfoState
    {
        FirstName = 1,
        LastName = 2,
        Language = 4
    }
}