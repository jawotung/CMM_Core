using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI;

public partial class CMMDBContext : DbContext
{
    public CMMDBContext()
    {
    }

    public CMMDBContext(DbContextOptions<CMMDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CmmAdbentry> CmmAdbentries { get; set; }

    public virtual DbSet<CmmAdbmast> CmmAdbmasts { get; set; }

    public virtual DbSet<CmmAdbmast20190107> CmmAdbmast20190107s { get; set; }

    public virtual DbSet<CmmArea> CmmAreas { get; set; }

    public virtual DbSet<CmmBankOfficer> CmmBankOfficers { get; set; }

    public virtual DbSet<CmmBranch> CmmBranches { get; set; }

    public virtual DbSet<CmmBusinessOwner> CmmBusinessOwners { get; set; }

    public virtual DbSet<CmmChannel> CmmChannels { get; set; }

    public virtual DbSet<CmmClient> CmmClients { get; set; }

    public virtual DbSet<CmmClientAccount> CmmClientAccounts { get; set; }

    public virtual DbSet<CmmClientRank> CmmClientRanks { get; set; }

    public virtual DbSet<CmmCollectionDetail> CmmCollectionDetails { get; set; }

    public virtual DbSet<CmmCollectionMethod> CmmCollectionMethods { get; set; }

    public virtual DbSet<CmmCustomerCategory> CmmCustomerCategories { get; set; }

    public virtual DbSet<CmmCustomerSegment> CmmCustomerSegments { get; set; }

    public virtual DbSet<CmmCustomerSubCategory> CmmCustomerSubCategories { get; set; }

    public virtual DbSet<CmmCustomerType> CmmCustomerTypes { get; set; }

    public virtual DbSet<CmmError> CmmErrors { get; set; }

    public virtual DbSet<CmmFee> CmmFees { get; set; }

    public virtual DbSet<CmmFeeType> CmmFeeTypes { get; set; }

    public virtual DbSet<CmmLastLogin> CmmLastLogins { get; set; }

    public virtual DbSet<CmmLineOfBusiness> CmmLineOfBusinesses { get; set; }

    public virtual DbSet<CmmOfficerType> CmmOfficerTypes { get; set; }

    public virtual DbSet<CmmPickupSite> CmmPickupSites { get; set; }

    public virtual DbSet<CmmProduct> CmmProducts { get; set; }

    public virtual DbSet<CmmProductAdb> CmmProductAdbs { get; set; }

    public virtual DbSet<CmmProductAssignment> CmmProductAssignments { get; set; }

    public virtual DbSet<CmmProductAssignmentFee> CmmProductAssignmentFees { get; set; }

    public virtual DbSet<CmmProductChannel> CmmProductChannels { get; set; }

    public virtual DbSet<CmmProductFee> CmmProductFees { get; set; }

    public virtual DbSet<CmmProductTier> CmmProductTiers { get; set; }

    public virtual DbSet<CmmUdfTemplate> CmmUdfTemplates { get; set; }

    public virtual DbSet<CmmUdfValue> CmmUdfValues { get; set; }

    public virtual DbSet<Cmsofficer> Cmsofficers { get; set; }

    public virtual DbSet<TmpUdfrestore> TmpUdfrestores { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=CMM;User Id=sa;Password=P@55w0rd;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CmmAdbentry>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ADBEntries");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AverageBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<CmmAdbmast>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ADBMAST");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AcctType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Adbapr)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBAPR");
            entity.Property(e => e.Adbaug)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBAUG");
            entity.Property(e => e.Adbdec)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBDEC");
            entity.Property(e => e.Adbfeb)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBFEB");
            entity.Property(e => e.Adbjan)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJAN");
            entity.Property(e => e.Adbjul)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJUL");
            entity.Property(e => e.Adbjun)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJUN");
            entity.Property(e => e.Adbmar)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBMAR");
            entity.Property(e => e.Adbmay)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBMAY");
            entity.Property(e => e.Adbnov)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBNOV");
            entity.Property(e => e.Adboct)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBOCT");
            entity.Property(e => e.Adbsep)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBSEP");
            entity.Property(e => e.Cifno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CIFNo");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmAdbmast20190107>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ADBMAST_20190107");

            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.AcctType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Adbapr)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBAPR");
            entity.Property(e => e.Adbaug)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBAUG");
            entity.Property(e => e.Adbdec)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBDEC");
            entity.Property(e => e.Adbfeb)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBFEB");
            entity.Property(e => e.Adbjan)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJAN");
            entity.Property(e => e.Adbjul)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJUL");
            entity.Property(e => e.Adbjun)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBJUN");
            entity.Property(e => e.Adbmar)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBMAR");
            entity.Property(e => e.Adbmay)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBMAY");
            entity.Property(e => e.Adbnov)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBNOV");
            entity.Property(e => e.Adboct)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBOCT");
            entity.Property(e => e.Adbsep)
                .HasColumnType("numeric(18, 2)")
                .HasColumnName("ADBSEP");
            entity.Property(e => e.Cifno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CIFNo");
            entity.Property(e => e.ClientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmArea>(entity =>
        {
            entity
                .ToTable("CMM_Area");

            entity.HasKey(e => e.AreaId);

            entity.Property(e => e.AreaCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.AreaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AreaID");
            entity.Property(e => e.AreaName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeactivateBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmBankOfficer>(entity =>
        {
            entity.HasKey(e => e.OfficerId);

            entity.ToTable("CMM_BankOfficers");

            entity.Property(e => e.OfficerId)
                .ValueGeneratedNever()
                .HasColumnName("OfficerID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.DeactivateBy)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MiddleInitial)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.OfficerTypeId).HasColumnName("OfficerTypeID");
        });

        modelBuilder.Entity<CmmBranch>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_Branches");

            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.BranchCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BranchId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BranchID");
            entity.Property(e => e.BranchName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeactivatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmBusinessOwner>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_BusinessOwner");

            entity.Property(e => e.OwnerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OwnerID");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(75)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmChannel>(entity =>
        {
            entity.HasKey(e => e.ChannelId);

            entity.ToTable("CMM_Channels");

            entity.Property(e => e.ChannelId)
                .ValueGeneratedNever()
                .HasColumnName("ChannelID");
            entity.Property(e => e.ChannelName)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmClient>(entity =>
        {
            entity.HasKey(e => e.ClientId);

            entity.ToTable("CMM_Clients");

            entity.Property(e => e.ClientId)
                .ValueGeneratedNever()
                .HasColumnName("ClientID");
            entity.Property(e => e.AcctOwnerId).HasColumnName("AcctOwnerID");
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Cifno)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("CIFNo");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CmofficerId).HasColumnName("CMOfficerID");
            entity.Property(e => e.DateDeleted).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fremark)
                .IsUnicode(false)
                .HasColumnName("FRemark");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.LineOfBussId).HasColumnName("LineOfBussID");
            entity.Property(e => e.RequiredAdb)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("RequiredADB");
            entity.Property(e => e.SegmentId).HasColumnName("SegmentID");
            entity.Property(e => e.Sremark)
                .IsUnicode(false)
                .HasColumnName("SRemark");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
        });

        modelBuilder.Entity<CmmClientAccount>(entity =>
        {
            entity.HasKey(e => e.ClientAcctId);

            entity.ToTable("CMM_ClientAccounts");

            entity.Property(e => e.ClientAcctId)
                .ValueGeneratedNever()
                .HasColumnName("ClientAcctID");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Cifno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CIFNo");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DateVerified).HasColumnType("datetime");
            entity.Property(e => e.VerifiedBy)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmClientRank>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ClientRank");

            entity.Property(e => e.ClientRank)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ClientRankId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ClientRankID");
        });

        modelBuilder.Entity<CmmCollectionDetail>(entity =>
        {
            entity.HasKey(e => e.CollectionDetailId).HasName("PK_CMM_ColletionDetails");

            entity.ToTable("CMM_CollectionDetails");

            entity.Property(e => e.CollectionDetailId)
                .ValueGeneratedNever()
                .HasColumnName("CollectionDetailID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CollectionMethodId).HasColumnName("CollectionMethodID");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmailOrHardCopy)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OtherInstructions)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Position)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ProdAvailId).HasColumnName("ProdAvailID");
        });

        modelBuilder.Entity<CmmCollectionMethod>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_CollectionMethods");

            entity.Property(e => e.CollectionMethod)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CollectionMethodId).HasColumnName("CollectionMethodID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<CmmCustomerCategory>(entity =>
        {
            entity.HasKey(e => e.CustCategoryId);

            entity.ToTable("CMM_CustomerCategory");

            entity.Property(e => e.CustCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("CustCategoryID");
            entity.Property(e => e.CustCategory)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmCustomerSegment>(entity =>
        {
            entity.HasKey(e => e.SegmentId);

            entity.ToTable("CMM_CustomerSegment");

            entity.Property(e => e.SegmentId)
                .ValueGeneratedNever()
                .HasColumnName("SegmentID");
            entity.Property(e => e.SegmentName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmCustomerSubCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_CustomerSubCategory");

            entity.Property(e => e.SubCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SubCategoryID");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmCustomerType>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId);

            entity.ToTable("CMM_CustomerType");

            entity.Property(e => e.CustomerTypeId)
                .ValueGeneratedNever()
                .HasColumnName("CustomerTypeID");
            entity.Property(e => e.CustomerType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmError>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_Errors");

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.ErrorSource)
                .HasMaxLength(120)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmFee>(entity =>
        {
            entity.HasKey(e => e.FeeId);

            entity.ToTable("CMM_Fees");

            entity.Property(e => e.FeeId)
                .ValueGeneratedNever()
                .HasColumnName("FeeID");
            entity.Property(e => e.Description)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.FeeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmFeeType>(entity =>
        {
            entity.HasKey(e => e.FeeNo);

            entity.ToTable("CMM_FeeType");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FeeId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field1Amount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field2Amount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Field3Amount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FixAmount)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmLastLogin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_LastLogin");

            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmLineOfBusiness>(entity =>
        {
            entity.HasKey(e => e.LineOfBussId);

            entity.ToTable("CMM_LineOfBusiness");

            entity.Property(e => e.LineOfBussId)
                .ValueGeneratedNever()
                .HasColumnName("LineOfBussID");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.LineName).IsUnicode(false);
        });

        modelBuilder.Entity<CmmOfficerType>(entity =>
        {
            entity.HasKey(e => e.OfficerTypeId);

            entity.ToTable("CMM_OfficerType");

            entity.Property(e => e.OfficerTypeId)
                .ValueGeneratedNever()
                .HasColumnName("OfficerTypeID");
            entity.Property(e => e.OfficerType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmPickupSite>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_PickupSite");

            entity.Property(e => e.Adbreq)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("ADBReq");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FreqSchedule)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.FreqUnit)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ProdAvailId).HasColumnName("ProdAvailID");
            entity.Property(e => e.ServicingTime).HasDefaultValue(1);
            entity.Property(e => e.SiteId).HasColumnName("SiteID");
            entity.Property(e => e.SvcUnit)
                .HasMaxLength(75)
                .IsUnicode(false);

            entity.HasOne(d => d.ProdAvail).WithMany()
                .HasForeignKey(d => d.ProdAvailId)
                .HasConstraintName("FK_CMM_PickupSite_CMM_ProductAssignment");
        });

        modelBuilder.Entity<CmmProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("CMM_Products");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(75)
                .IsUnicode(false);
            entity.Property(e => e.ProductCode)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(75)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmProductAdb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ProductADB");

            entity.Property(e => e.AdbAmount)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADB_Amount");
            entity.Property(e => e.AdbName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADB_Name");
            entity.Property(e => e.ProductAdbId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductADB_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
        });

        modelBuilder.Entity<CmmProductAssignment>(entity =>
        {
            entity.HasKey(e => e.ProdAvailId);

            entity.ToTable("CMM_ProductAssignment");

            entity.Property(e => e.ProdAvailId)
                .ValueGeneratedNever()
                .HasColumnName("ProdAvailID");
            entity.Property(e => e.Adbrequired)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ADBRequired");
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientAcctId).HasColumnName("ClientAcctID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Cmofficer)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMOfficer");
            entity.Property(e => e.DebitAcct)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DebitAcctId).HasColumnName("DebitAcctID");
            entity.Property(e => e.DepositoryBranch)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InactiveBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MonitorAcct)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OfficerTypeId).HasColumnName("OfficerTypeID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ReferredBy)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmProductAssignmentFee>(entity =>
        {
            entity.HasKey(e => e.ClientProductAdbfeeId);

            entity.ToTable("CMM_ProductAssignmentFee");

            entity.Property(e => e.ClientProductAdbfeeId)
                .ValueGeneratedNever()
                .HasColumnName("ClientProductADBFeeID");
            entity.Property(e => e.Amount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Caption)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FeeId).HasColumnName("FeeID");
            entity.Property(e => e.ProdAvailId).HasColumnName("ProdAvailID");
            entity.Property(e => e.ProductAdbId).HasColumnName("ProductADB_ID");
            entity.Property(e => e.RowOrder).HasDefaultValue(0);
            entity.Property(e => e.TierId).HasColumnName("TierID");
        });

        modelBuilder.Entity<CmmProductChannel>(entity =>
        {
            entity.HasKey(e => e.ProductChannelId);

            entity.ToTable("CMM_ProductChannel");

            entity.Property(e => e.ProductChannelId)
                .ValueGeneratedNever()
                .HasColumnName("ProductChannelID");
            entity.Property(e => e.ChannelId).HasColumnName("ChannelID");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductAvailId).HasColumnName("ProductAvailID");

            entity.HasOne(d => d.Channel).WithMany(p => p.CmmProductChannels)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK_CMM_ProductChannel_CMM_ProductChannel");

            entity.HasOne(d => d.ProductAvail).WithMany(p => p.CmmProductChannels)
                .HasForeignKey(d => d.ProductAvailId)
                .HasConstraintName("FK_CMM_ProductChannel_CMM_ProductAssignment");
        });

        modelBuilder.Entity<CmmProductFee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ProductFees");

            entity.Property(e => e.FeeAmount)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("FeeID");
            entity.Property(e => e.FeeName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProductAdbId).HasColumnName("ProductADB_ID");
            entity.Property(e => e.TierId).HasColumnName("TierID");
        });

        modelBuilder.Entity<CmmProductTier>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_ProductTier");

            entity.Property(e => e.AdbAmount)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADB_Amount");
            entity.Property(e => e.ProductAdbId).HasColumnName("ProductADB_ID");
            entity.Property(e => e.TierDesc)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TierId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TierID");
            entity.Property(e => e.TierName)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmUdfTemplate>(entity =>
        {
            entity.HasKey(e => e.UdfItemId);

            entity.ToTable("CMM_UdfTemplates");

            entity.Property(e => e.UdfItemId)
                .ValueGeneratedNever()
                .HasColumnName("UdfItemID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UdfLabel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UdfType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CmmUdfValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMM_UdfValues");

            entity.Property(e => e.ProdAvailId).HasColumnName("ProdAvailID");
            entity.Property(e => e.UdfItemId).HasColumnName("UdfItemID");
            entity.Property(e => e.Value)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Cmsofficer>(entity =>
        {
            entity.HasKey(e => e.BranchCode);

            entity.ToTable("CMSOfficer");

            entity.Property(e => e.BranchCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("BranchID");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CmofficerAssignment)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CMOfficerAssignment");
            entity.Property(e => e.OfficerId).HasColumnName("OfficerID");
        });

        modelBuilder.Entity<TmpUdfrestore>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tmp_UDFRestore");

            entity.Property(e => e.CifNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
