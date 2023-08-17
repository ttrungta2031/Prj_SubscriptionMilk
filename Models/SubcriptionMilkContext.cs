using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class SubcriptionMilkContext : DbContext
    {
        public SubcriptionMilkContext()
        {
        }

        public SubcriptionMilkContext(DbContextOptions<SubcriptionMilkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryInPackage> CategoryInPackages { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<DeliveryMan> DeliveryMen { get; set; }
        public virtual DbSet<DeliveryTrip> DeliveryTrips { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageItem> PackageItems { get; set; }
        public virtual DbSet<PackageOrder> PackageOrders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInCollection> ProductInCollections { get; set; }
        public virtual DbSet<Slot> Slots { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TimeFrame> TimeFrames { get; set; }
        public virtual DbSet<Working> Workings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=SubcriptionMilk.mssql.somee.com;packet size=4096;user id=tiensidiien_SQLLogin_1;pwd=uaeovuatgl;data source=SubcriptionMilk.mssql.somee.com;persist security info=False;initial catalog=SubcriptionMilk");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);



                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_Account_Station");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<CategoryInPackage>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.PackageId });

                entity.ToTable("CategoryInPackage");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.PackageId).HasColumnName("package_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryInPackages)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryInPackage_Category");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.CategoryInPackages)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryInPackage_Package");
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<DeliveryMan>(entity =>
            {
                entity.ToTable("DeliveryMan");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<DeliveryTrip>(entity =>
            {
                entity.ToTable("DeliveryTrip");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.DeliveryMan)
                    .WithMany(p => p.DeliveryTrips)
                    .HasForeignKey(d => d.DeliveryManId)
                    .HasConstraintName("FK_DeliveryTrip_DeliveryMan");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.DeliveryTrips)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_DeliveryTrip_Station");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.DeliveryTrips)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_DeliveryTrip_Store");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.DeliveryTrip)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryTripId)
                    .HasConstraintName("FK_Order_DeliveryTrip");

                entity.HasOne(d => d.PacakeOrder)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PacakeOrderId)
                    .HasConstraintName("FK_Order_Package Order");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK_Order_Slot");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.OrderId });

                entity.ToTable("OrderDetail");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Img).HasMaxLength(4000);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<PackageItem>(entity =>
            {
                entity.HasKey(e => new { e.PackageId, e.CollectionId })
                    .HasName("PK_CollectionInPackage");

                entity.ToTable("PackageItem");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.PackageItems)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageItem_Collection");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageItems)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PackageItem_Package");

                entity.HasOne(d => d.TimeFram)
                    .WithMany(p => p.PackageItems)
                    .HasForeignKey(d => d.TimeFramId)
                    .HasConstraintName("FK_PackageItem_TimeFrame");
            });

            modelBuilder.Entity<PackageOrder>(entity =>
            {
                entity.ToTable("Package Order");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.PackageOrders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Package Order_Account");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.PackageOrders)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK_Package Order_Package");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PackageOrders)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Package Order_Payment");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<ProductInCollection>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CollectionId });

                entity.ToTable("ProductInCollection");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.ProductInCollections)
                    .HasForeignKey(d => d.CollectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCollection_Collection");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInCollections)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInCollection_Product");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("Slot");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.ToTable("Station");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.Stations)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK_Station_Slot");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Supplier");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompanyName).HasMaxLength(50);
            });

            modelBuilder.Entity<TimeFrame>(entity =>
            {
                entity.ToTable("TimeFrame");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.TimeFrames)
                    .HasForeignKey(d => d.SlotId)
                    .HasConstraintName("FK_TimeFrame_Slot");
            });

            modelBuilder.Entity<Working>(entity =>
            {
                entity.HasKey(e => new { e.StationId, e.DeliveryManId })
                    .HasName("PK_working");

                entity.ToTable("Working");

                entity.HasOne(d => d.DeliveryMan)
                    .WithMany(p => p.Workings)
                    .HasForeignKey(d => d.DeliveryManId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Working_DeliveryMan");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Workings)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Working_Station");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
