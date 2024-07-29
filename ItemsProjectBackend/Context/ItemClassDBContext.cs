using Microsoft.EntityFrameworkCore;

namespace ItemsProjectBackend.Context
{
    public class ItemClassDBContext : DbContext
    {
        public ItemClassDBContext(DbContextOptions<ItemClassDBContext> options)
            : base(options)
        {
            
        }
    }
}
