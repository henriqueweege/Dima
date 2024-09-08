using Dima.Api.Models;
using Dima.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dima.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : IdentityDbContext<
                                                                                    User, 
                                                                                    IdentityRole<long>, 
                                                                                    long, 
                                                                                    IdentityUserClaim<long>, 
                                                                                    IdentityUserRole<long>, 
                                                                                    IdentityUserLogin<long>, 
                                                                                    IdentityRoleClaim<long>, 
                                                                                    IdentityUserToken<long>
                                                                                   >(opts)
{

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
