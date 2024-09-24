using Microsoft.EntityFrameworkCore;

public class AlbatrosPortfoyPortalDbContext : DbContext
{
    public AlbatrosPortfoyPortalDbContext(DbContextOptions<AlbatrosPortfoyPortalDbContext> options)
        : base(options)
    {
    }

    // Entity'lerimizi DbSet olarak ekleyelim
    public DbSet<Page> Pages { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Journal> Journals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Parent-Child ilişkilendirmelerini yapalım
        modelBuilder.Entity<Page>()
            .HasMany(p => p.SubPages)
            .WithOne(p => p.ParentPage)
            .HasForeignKey(p => p.ParentPageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Menu>()
            .HasMany(m => m.SubMenus)
            .WithOne(m => m.ParentMenu)
            .HasForeignKey(m => m.ParentMenuId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}