using System;
using System.Collections.Generic;
using System.Data.Entity;
using LoftBlog_TrainingBot.Models;

namespace LoftBlog_TrainingBot.Database
{
    public class BotDbContext: DbContext
    {
        public BotDbContext() : base("SqlConnection")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}