using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reipush.Api.Entities;


namespace Reipush.Api.Entities
{
    public class ReipushContext : DbContext
    {
        public ReipushContext (DbContextOptions<ReipushContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Reipush.Api.Entities.User.User> User { get; set; }
        public virtual DbSet<Reipush.Api.Entities.User.voUser> voUser { get; set; }
        public virtual DbSet<Reipush.Api.Entities.Util.Countries> Countries { get; set; }
        public virtual DbSet<Reipush.Api.Entities.Util.StateProvince> StateProvinces { get; set; }
        public virtual DbSet<Reipush.Api.Entities.User.UserRefreshToken> UserRefreshTokens { get; set; }
        public virtual DbSet<Reipush.Api.Entities.Property.voPropertyAddress> voPropertyAddress { get; set; }
    }
}
