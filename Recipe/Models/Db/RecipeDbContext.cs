#nullable disable

namespace Recipe.Models.Db
{
    using EntityFramework.Exceptions.SqlServer;
    using Microsoft.EntityFrameworkCore;
    using System.Configuration;

    public partial class RecipeDbContext : DbContext
    {
        public RecipeDbContext()
        {
        }

        public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommonEntity> CommonEntities { get; set; }
        
        public virtual DbSet<Customer> Customers { get; set; }
        
        public virtual DbSet<CustomerAutoCompleteNameTextView> CustomerAutoCompleteNameTextViews { get; set; }
        
        public virtual DbSet<CustomerAutoCompletePhoneTextView> CustomerAutoCompletePhoneTextViews { get; set; }
        
        public virtual DbSet<LatestCustomersView> LatestCustomersViews { get; set; }
        
        public virtual DbSet<Medicin> Medicins { get; set; }
        
        public virtual DbSet<MedicinType> MedicinTypes { get; set; }
        
        public virtual DbSet<RecipeCard> RecipeCards { get; set; }
        
        public virtual DbSet<RecipeLine> RecipeLines { get; set; }
        
        public virtual DbSet<Unit> Units { get; set; }

#pragma warning disable IDE0058 // Expression value is never used

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // Recreate with : Scaffold-DbContext 'Server=.\SQLEXPRESS;Database=RECIPE;Trusted_Connection=True;' Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models\Db -Context RecipeDbContext -Force
                // Remember to backup OnConfiguring method + usings
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["RecipeDatabase"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonEntity>(entity => entity.Property(e => e.RftData).HasMaxLength(8000));

            modelBuilder.Entity<Customer>(entity =>
              {
                  entity.HasKey(e => e.Id)
                      .IsClustered(false);

                  entity.HasIndex(e => new { e.Name, e.Address1, e.Postcode, e.Phone }, "ClusteredIndex-20220121-152126")
                      .IsUnique()
                      .IsClustered();

                  entity.Property(e => e.Id).HasColumnName("ID");

                  entity.Property(e => e.Address1).HasMaxLength(50);

                  entity.Property(e => e.Address2).HasMaxLength(50);

                  entity.Property(e => e.AllowGdprdataStoring).HasColumnName("AllowGDPRDataStoring");

                  entity.Property(e => e.CreatedDate)
                      .HasColumnType("datetime")
                      .HasDefaultValueSql("(getdate())");

                  entity.Property(e => e.DeletedDate).HasColumnType("date");

                  entity.Property(e => e.Email).HasMaxLength(100);

                  entity.Property(e => e.Mobile).HasMaxLength(20);

                  entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                  entity.Property(e => e.Phone).HasMaxLength(20);

                  entity.Property(e => e.Postcode).HasMaxLength(7);

                  entity.Property(e => e.Town).HasMaxLength(40);

                  entity.Property(e => e.Uniquekey)
                      .HasMaxLength(20)
                      .IsUnicode(false);
              });

            modelBuilder.Entity<CustomerAutoCompleteNameTextView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomerAutoCompleteNameTextView");

                entity.Property(e => e.AutoCompleteText)
                    .IsRequired()
                    .HasMaxLength(130);
            });

            modelBuilder.Entity<CustomerAutoCompletePhoneTextView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CustomerAutoCompletePhoneTextView");

                entity.Property(e => e.AutoCompleteText).HasMaxLength(130);
            });

            modelBuilder.Entity<LatestCustomersView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LatestCustomersView");

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LatestRecipeCard).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Postcode).HasMaxLength(7);

                entity.Property(e => e.Town).HasMaxLength(40);
            });

            modelBuilder.Entity<Medicin>(entity =>
            {
                entity.ToTable("Medicin");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MedicinTypeId).HasColumnName("MedicinTypeID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(65);

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.HasOne(d => d.MedicinType)
                    .WithMany(p => p.Medicins)
                    .HasForeignKey(d => d.MedicinTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicin_MedicinTypes");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Medicins)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Medicin_Units");
            });

            modelBuilder.Entity<MedicinType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MedicinTypeName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<RecipeCard>(entity =>
            {
                entity.ToTable("RecipeCard");

                entity.HasIndex(e => e.Date, "NonClusteredIndex-20220128-225926");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.RecipeCards)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeCard_Customers");
            });

            modelBuilder.Entity<RecipeLine>(entity =>
            {
                entity.HasIndex(e => new { e.RecipeCardId, e.Position, e.MedicinTypeId }, "NonClusteredIndex-20220128-155942")
                    .IsUnique();

                entity.HasIndex(e => e.RecipeCardId, "NonClusteredIndex-20220128-225839");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Evening).HasMaxLength(15);

                entity.Property(e => e.MedicinName).HasMaxLength(65);

                entity.Property(e => e.MedicinTypeId).HasColumnName("MedicinTypeID");

                entity.Property(e => e.Midnight).HasMaxLength(15);

                entity.Property(e => e.Morning).HasMaxLength(15);

                entity.Property(e => e.Noon).HasMaxLength(15);

                entity.Property(e => e.RecipeCardId).HasColumnName("RecipeCardID");

                entity.Property(e => e.UnitsId).HasColumnName("UnitsID");

                entity.HasOne(d => d.MedicinType)
                    .WithMany(p => p.RecipeLines)
                    .HasForeignKey(d => d.MedicinTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeLines_MedicinTypes");

                entity.HasOne(d => d.RecipeCard)
                    .WithMany(p => p.RecipeLines)
                    .HasForeignKey(d => d.RecipeCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RecipeLines_RecipeCard");

                entity.HasOne(d => d.Units)
                    .WithMany(p => p.RecipeLines)
                    .HasForeignKey(d => d.UnitsId)
                    .HasConstraintName("FK_RecipeLines_Units");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

#pragma warning restore IDE0058 // Expression value is never used

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
