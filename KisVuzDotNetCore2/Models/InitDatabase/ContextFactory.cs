using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KisVuzDotNetCore2.Models.InitDatabase
{
    public class ContextFactory : IDesignTimeDbContextFactory<AppIdentityDBContext>
    {        
        public AppIdentityDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityDBContext>();
            optionsBuilder.UseMySql("Server=localhost;database=kisvuz;uid=root;CharSet=utf8;",
                opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                );

            return new AppIdentityDBContext(optionsBuilder.Options);
        }
    }
}
