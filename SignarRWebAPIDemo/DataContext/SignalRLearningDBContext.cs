using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignarRWebAPIDemo.AuthData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignarRWebAPIDemo.DataContext
{
    public class SignalRLearningDBContext: IdentityDbContext<ApplicationUser>
    {
        public SignalRLearningDBContext(DbContextOptions<SignalRLearningDBContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
