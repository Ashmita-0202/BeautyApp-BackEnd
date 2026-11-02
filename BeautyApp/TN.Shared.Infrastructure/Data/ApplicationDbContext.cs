using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NV.Booking.Domain.Entities;
using NV.Buautician.Domain.Entities;
using NV.Feedbacks.Domain.Entities;
using NV.Inventory.Domain.Entities;
using NV.Payment.Domain.Entities;
using System.Reflection.Emit;
using TN.Authentication.Domain.Entities;
using TN.Setup.Domain.Entities;


namespace TN.Shared.Infrastructure.Data
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        }

       


        #region Initial SetUp
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<UserCompany> UserCompany { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<Vdc> Vdcs { get; set; }
        public DbSet<Company> Companys { get; set; }

        public DbSet<CompanySettings> CompanySettings { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        #endregion


        #region Menu,Module,SubModule

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<SubModules> SubModules { get; set; }
        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleModule> RoleModules { get; set; }

        public DbSet<RoleSubModules> RoleSubModules { get; set; }

        public DbSet<Product> Products { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            #region Authentication

            #region Permission and Roles(m:m)

            // Configure many-to-many relationship between roles and Permission
            builder.Entity<RolePermission>()
                .HasKey(rm => new { rm.RoleId, rm.PermissionId });

            builder.Entity<RolePermission>()
                .HasOne(rm => rm.Role)
                .WithMany()
                .HasForeignKey(rm => rm.RoleId);

            builder.Entity<RolePermission>()
                .HasOne(rm => rm.Permissions)
                .WithMany(m => m.RolePermissions)
                .HasForeignKey(rm => rm.PermissionId);

            #endregion



            // Explicitly configure AspNetRoles
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(256)
                    .IsRequired(false);

                entity.Property(e => e.NormalizedName)
                    .HasMaxLength(256)
                    .IsRequired(false);

                entity.Property(e => e.ConcurrencyStamp)
                    .HasMaxLength(256)
                    .IsRequired(false);
            });

            #region Province,District,Municipality
            // Province Configuration
            builder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProvinceNameInNepali).IsRequired(false);
                entity.Property(e => e.ProvinceNameInEnglish).IsRequired(false);

                entity.HasMany(p => p.Districts)
                      .WithOne(d => d.Province)
                      .HasForeignKey(d => d.ProvinceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            #region User and Company (M:M)
            builder.Entity<UserCompany>()
                .HasKey(uc => new { uc.UserId, uc.CompanyId });

            builder.Entity<UserCompany>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserCompany)
                .HasForeignKey(uc => uc.UserId);

            builder.Entity<UserCompany>()
                .HasOne(uc => uc.Company)
                .WithMany(c => c.UserCompany)
                .HasForeignKey(uc => uc.CompanyId);
            #endregion

            #region Company and User(m:m)
            builder.Entity<UserCompany>()
                .HasKey(cu => new { cu.UserId, cu.CompanyId }); // Composite Key

            builder.Entity<UserCompany>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.UserCompany)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserCompany>()
                .HasOne(cu => cu.Company)
                .WithMany(c => c.UserCompany)
                .HasForeignKey(cu => cu.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion



            // District Configuration
            builder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DistrictNameInNepali).IsRequired(false);
                entity.Property(e => e.DistrictNameInEnglish).IsRequired(false);

                entity.HasOne(p => p.Province)
                      .WithMany(d => d.Districts)
                      .HasForeignKey(d => d.ProvinceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Municipality)
                      .WithOne(m => m.Districts)
                      .HasForeignKey(m => m.DistrictId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Vdcs)
                      .WithOne(v => v.Districts)
                      .HasForeignKey(v => v.DistrictId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Municipality Configuration
            builder.Entity<Municipality>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MunicipalityNameInNepali).IsRequired(false);
                entity.Property(e => e.MunicipalityNameInEnglish).IsRequired(false);

                entity.HasOne(d => d.Districts)
                      .WithMany(m => m.Municipality)
                      .HasForeignKey(m => m.DistrictId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Vdc Configuration
            builder.Entity<Vdc>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.VdcNameInEnglish).IsRequired(false);
                entity.Property(v => v.VdcNameInNepali).IsRequired(false);

                entity.HasOne(d => d.Districts)
                      .WithMany(v => v.Vdcs)
                      .HasForeignKey(v => v.DistrictId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion


            #endregion

            #region RolesModules
            #region Modules and Roles(m:m)

            // Configure many-to-many relationship between roles and modules
            builder.Entity<RoleModule>()
                .HasKey(rm => new { rm.RoleId, rm.ModuleId });

            builder.Entity<RoleModule>()
                .HasOne(rm => rm.Role)
                .WithMany()
                .HasForeignKey(rm => rm.RoleId);

            builder.Entity<RoleModule>()
                .HasOne(rm => rm.Modules)
                .WithMany(m => m.RoleModules)
                .HasForeignKey(rm => rm.ModuleId);

            #endregion

            #region SubModules and Roles(m:m)

            // Configure many-to-many relationship between roles and SubModules
            builder.Entity<RoleSubModules>()
                .HasKey(rm => new { rm.RoleId, rm.SubModulesId });

            builder.Entity<RoleSubModules>()
                .HasOne(rm => rm.Role)
                .WithMany()
                .HasForeignKey(rm => rm.RoleId);

            builder.Entity<RoleSubModules>()
                .HasOne(rm => rm.SubModules)
                .WithMany(m => m.RoleSubModules)
                .HasForeignKey(rm => rm.SubModulesId);

            #endregion

            #region Menus and Roles(m:m)

            // Configure many-to-many relationship between roles and SubModules
            builder.Entity<RoleMenus>()
                .HasKey(rm => new { rm.RoleId, rm.MenusId });

            builder.Entity<RoleMenus>()
                .HasOne(rm => rm.Role)
                .WithMany()
                .HasForeignKey(rm => rm.RoleId);

            builder.Entity<RoleMenus>()
                .HasOne(rm => rm.Menu)
                .WithMany(m => m.RoleMenus)
                .HasForeignKey(rm => rm.MenusId);

            #endregion

            #region Modules and SubModules(1:m)
            builder.Entity<Modules>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.SubModules)
                .WithOne(x => x.Modules)
                .HasForeignKey(x => x.ModulesId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            #endregion

            #region SubModules and Modules(m:1)
            builder.Entity<SubModules>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Modules)
                .WithMany(x => x.SubModules)
                .HasForeignKey(x => x.ModulesId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            #endregion

            #region SubModules and Menu(1:m)
            builder.Entity<SubModules>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Menu)
                .WithOne(x => x.SubModules)
                .HasForeignKey(x => x.SubModulesId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            #endregion

            #region Menu and SubModules(m:1)
            builder.Entity<Menu>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.SubModules)
                .WithMany(x => x.Menu)
                .HasForeignKey(x => x.SubModulesId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            #endregion

            #endregion

            #region Initial SetUp
            #region Organization and Province(m:1)
            builder.Entity<Organization>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Provinces)
                .WithMany(x => x.Organizations)
                .HasForeignKey(x => x.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            });
            #endregion

            #region Province and Organization(1:m)
            builder.Entity<Province>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Organizations)
                .WithOne(x => x.Provinces)
                .HasForeignKey(x => x.ProvinceId)
                .OnDelete(DeleteBehavior.Cascade);

            });

            #endregion

            #region Organization and Institution(1:m)
            builder.Entity<Organization>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x => x.Companies)
                .WithOne(x => x.Organization)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Institution and Organization(M:1)
            builder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Organization)
                .WithMany(x => x.Companies)
                .HasForeignKey(x => x.OrganizationId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion


            #region Company and CompanySettings(1:m)
            builder.Entity<Company>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(x=>x.CompanySettings)
                .WithOne(x=>x.Company)
                .HasForeignKey(x=>x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region CompanySettings and Company (m:1)
            builder.Entity<CompanySettings>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Company)
                .WithMany(x => x.CompanySettings)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
            });
            #endregion

            #endregion

            #region Beautician
            #region Application and Beautician(1:m)
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Beauticians>()
                .WithOne(x=>x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region Beautician and BeautyServices(m:m)

            // Configure many-to-many relationship between Beautician and BeautyServices
            builder.Entity<BeauticianServices>()
                .HasKey(rm => new { rm.BeauticianId, rm.BeautiServiceId });

            builder.Entity<BeauticianServices>()
                .HasOne(rm => rm.BeautyServices)
                .WithMany(m => m.BeauticianServices)
                .HasForeignKey(rm => rm.BeautiServiceId);

            builder.Entity<BeauticianServices>()
                .HasOne(rm => rm.Beauticians)
                .WithMany(m => m.BeauticianServices)
                .HasForeignKey(rm => rm.BeauticianId);

            #endregion

            #endregion

            #region Booking

            #region Application and Booking(1:m)
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Bookings>()
                .WithOne(x=>x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            #endregion

            #region Beautician and Booking(1:m)
            builder.Entity<Beauticians>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Bookings>()
                    .WithOne(x=>x.Beautician)
                    .HasForeignKey(x => x.BeauticianId)
                    .OnDelete(DeleteBehavior.Restrict); // or DeleteBehavior.NoAction
            });


            #endregion

            #region BeautyServices and Booking(1:m)
            builder.Entity<BeautyServices>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Bookings>()
                .WithOne(x=>x.BeautyServices)
                .HasForeignKey(x => x.BeautyServiceId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BeautyServices>()
                .Property(bs => bs.Price)
                .HasPrecision(18, 4);

            #endregion


            #endregion

            #region Payment
            #region Booking and Payments(1:m)
            builder.Entity<Bookings>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Payments>()
                .WithOne(x=>x.Booking)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #endregion

            #region Feedbacks

            #region Application and Reviews(1:m)
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Review>()
                .WithOne(x=>x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Beautician and Reviews(1:m)
            builder.Entity<Beauticians>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany<Review>()
                .WithOne(x=>x.Beautician)
                .HasForeignKey(x => x.BeauticianId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Product and Reviews(1:m)
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.HasMany<Review>()
                    .WithOne(r => r.Products)
                    .HasForeignKey(r => r.ProductsId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion


            #endregion


        }



    }
}
