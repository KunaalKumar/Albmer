using Albmer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Albmer.Data
{
    public class DbInitializer
    {
        public static void Initialize(CacheContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();
        }
    }
}
