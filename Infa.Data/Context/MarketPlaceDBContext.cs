using Infa.Domain.Models.Contacts;
using Infa.Domain.Models.Identity;
using Infa.Domain.Models.SellersProduct;
using Infa.Domain.Models.Site;
using Infa.Domain.Models.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Data.Context
{
    public partial class MarketPlaceDBContext :
       IdentityDbContext<ApplicationUser, ApplicationRole, string,
        IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public MarketPlaceDBContext(DbContextOptions<MarketPlaceDBContext> options) : base(options)
        {

        }
    }

    //Properties
    public partial class MarketPlaceDBContext
    {
        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<Slider> Slider { get; set; }

        public DbSet<SiteBanner> SiteBanner { get; set; }

        public DbSet<AppSetting> AppSettings { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketMessage> TicketMessages { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<ProductColor> ProductColor { get; set; }
    }



    //EfCore
    public partial class MarketPlaceDBContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region User

            builder.Entity<ApplicationUser>().ToTable("User");

            builder.Entity<ApplicationUser>().HasKey(pk => pk.Id);


            #endregion


            #region Role

            builder.Entity<ApplicationRole>().ToTable("Role");

            builder.Entity<ApplicationRole>().HasKey(pk => pk.Id);

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = "ea345380-5af2-4d0c-a0e2-65f3eeabb898",
                    Name = "AplicationAdmin"
                });

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = "19b499d6-8920-43c0-a6d7-bbe48c42f7b3",
                    Name = "AplicationUser"
                });


            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = "ac0e1032-58c2-449a-8b47-7f6838386bc0",
                    Name = "AplicationSeller",
                });

            #endregion


            #region UserRole

            builder.Entity<ApplicationUserRole>().ToTable("UserRole");

            builder.Entity<ApplicationRole>().HasKey(pk => pk.Id);


            builder.Entity<ApplicationUserRole>()
                .HasOne(a => a.applicationRole)
                .WithMany(b => b.userRoles)
                .HasForeignKey(fk => fk.RoleId);

            builder.Entity<ApplicationUserRole>()
              .HasOne(a => a.applicationUser)
              .WithMany(b => b.UserRoles)
              .HasForeignKey(fk => fk.UserId);

            #endregion


            #region ContactUs

            builder.Entity<ContactUs>()
                .HasKey(pk => pk.Id);

            builder.Entity<ContactUs>()
                  .ToTable("ContactUs");

            builder.Entity<ContactUs>()
                    .HasOne(u => u.User)
                    .WithMany(cu => cu.ContactUses)
                    .HasForeignKey(fk => fk.UserId)
                    .IsRequired(false);


            #endregion


            #region Slider

            builder.Entity<Slider>().ToTable("Slider");


            builder.Entity<Slider>()
                .HasKey(pk => pk.Id);


            #endregion


            #region SiteBanner 

            builder.Entity<SiteBanner>().ToTable("SiteBanner");


            builder.Entity<SiteBanner>()
                .HasKey(pk => pk.Id);

            #endregion


            #region AppSetting

            builder.Entity<AppSetting>().ToTable("AppSetting");

            builder.Entity<AppSetting>().HasKey(pk => pk.Id);

            builder.Entity<AppSetting>()
                .HasData(new AppSetting
                {
                    Id = "99d2a56b89f9401c9466f83b1a65a582",
                    Mobile = "09123333333",
                    Address = "Tehran , Tehran nou",
                    CopyRight = "کپی بخش یا کل هر کدام از مطالب تنها با کسب مجوز مکتوب امکان پذیر است.",
                    Email = "mohammad.eb231298@gmail.com",
                    Phone = "771778985462",
                    FooterText = "داغ‌ترین مطالب هفته",
                    Description = "سایت فروشگاه ساز"
                });

            #endregion


            #region Ticket

            builder.Entity<Ticket>().ToTable("Ticket");

            builder.Entity<Ticket>()
                .HasKey(pk => pk.Id);


            builder.Entity<Ticket>()
                .HasOne(u => u.Owner)
                .WithMany(t => t.Tickets)
                .HasForeignKey(fk => fk.OwnerId).IsRequired().OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Ticket>()
                .HasMany(tm => tm.ticketMessages)
                .WithOne(t => t.Ticket)
                .HasForeignKey(fk => fk.TicketId).IsRequired().OnDelete(DeleteBehavior.NoAction);


            #endregion


            #region TicketMessage

            builder.Entity<TicketMessage>().ToTable("TicketMessage");

            builder.Entity<TicketMessage>()
                .HasKey(pk => pk.Id);

            builder.Entity<TicketMessage>()
                .HasOne(u => u.Sender)
                .WithMany(u => u.ticketMessages)
                .HasForeignKey(fk => fk.SenderId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region Seller

            builder.Entity<Seller>().ToTable("Seller");

            builder.Entity<Seller>()
                .HasKey(pk => pk.Id);

            builder.Entity<Seller>()
                .HasOne(u => u.user)
                .WithMany(s => s.Sellers)
                .HasForeignKey(fk => fk.UserId).IsRequired().OnDelete(DeleteBehavior.NoAction);


            #endregion


            #region Product

            builder.Entity<Product>().ToTable("Product");

            builder.Entity<Product>()
                .HasKey(pk => pk.Id);

            builder.Entity<Product>()
                .HasOne(s => s.Seller)
                .WithMany(p => p.Products)
                .HasForeignKey(fk => fk.SellerId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region Category

            builder.Entity<Category>().ToTable("Category");


            builder.Entity<Category>()
                 .HasKey(pk => pk.Id);

            #endregion


            #region ProductCategory

            builder.Entity<ProductCategory>().ToTable("ProductCategory");

            builder.Entity<ProductCategory>()
                 .HasKey(pk => pk.Id);

            builder.Entity<ProductCategory>()
                 .HasOne(p => p.Product)
                 .WithMany(pg => pg.productCategories)
                 .HasForeignKey(fk => fk.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ProductCategory>()
                 .HasOne(p => p.Category)
                 .WithMany(pg => pg.productCategories)
                . HasForeignKey(fk => fk.CategoryId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region ProductColor

            builder.Entity<ProductColor>().ToTable("ProductColor");

            builder.Entity<ProductColor>()
                 .HasKey(pk => pk.Id);

            builder.Entity<ProductColor>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductColors)
                .HasForeignKey(fk => fk.ProductId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            #endregion


            #region IgnoredTables

            builder.Ignore<IdentityUserClaim<string>>();
            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityRoleClaim<string>>();
            builder.Ignore<IdentityUserToken<string>>();

            #endregion

        }


    }
}

