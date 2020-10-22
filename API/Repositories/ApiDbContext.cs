using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories {
    public class ApiDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
    }
}