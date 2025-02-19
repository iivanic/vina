using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace vina.Server;
public class NPDataContext : IdentityDbContext
{
    public NPDataContext(DbContextOptions<NPDataContext> options)
        : base(options)
    { }
}
