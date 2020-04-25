using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecureChat.Core.Interfaces;
using System.Collections.Generic;

namespace SecureChat.DAL
{
    public class AppIdentityDbContext:IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options, IHttpContextAccessor httpContextAccessor)
        :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //public IEnumerable<IUser> Users { get; set; }
    }
}
