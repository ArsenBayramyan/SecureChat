using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecureChat.Core.Interfaces;
using System.Collections.Generic;

namespace SecureChat.DAL
{
    public class AppIdentityDbContext:IdentityDbContext<User>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        :base(options) { }

        //public IEnumerable<IUser> Users { get; set; }
    }
}
