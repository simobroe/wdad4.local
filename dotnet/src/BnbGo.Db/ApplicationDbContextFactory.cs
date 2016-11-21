using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace BnbGo.Db
{
    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=bnbgo;Pooling=true;");
            return new ApplicationDbContext(builder.Options);
        }
    }
}