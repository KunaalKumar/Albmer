using Albmer.Models;
using Microsoft.EntityFrameworkCore;

namespace Albmer.Data
{
    public class DbInitializer
    {
        public static void Initialize(CacheContext context)
        {
               context.Database.Migrate();
        }
    }
}
