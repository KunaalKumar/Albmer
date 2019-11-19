﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albmer.Models
{
    public class CacheContext : DbContext
    {
        public CacheContext(DbContextOptions<CacheContext> options) : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Membership> Memberships { get; set; }
    }
}
