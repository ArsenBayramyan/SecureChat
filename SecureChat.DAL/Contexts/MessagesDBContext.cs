using Microsoft.EntityFrameworkCore;
using SecureChat.Core.Interfaces;
using SecureChat.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.DAL.Contexts
{
   public class MessagesDBContext:DbContext
    {
        public MessagesDBContext(DbContextOptions<MessagesDBContext> options)
      : base(options) { }

        public DbSet<IMessage> Messages { get; set; }
    }
}
