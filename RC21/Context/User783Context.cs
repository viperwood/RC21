using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RC21.Models;

namespace RC21.Context;

public partial class User783Context : DbContext
{
    public User783Context()
    {
    }

    public User783Context(DbContextOptions<User783Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Accountant> Accountants { get; set; }

    public virtual DbSet<Admintable> Admintables { get; set; }

    public virtual DbSet<Analizertipe> Analizertipes { get; set; }

    public virtual DbSet<Analyzer> Analyzers { get; set; }

    public virtual DbSet<Cheack> Cheacks { get; set; }

    public virtual DbSet<Insurancecompany> Insurancecompanies { get; set; }

    public virtual DbSet<Laboratoryassistant> Laboratoryassistants { get; set; }

    public virtual DbSet<Orderservice> Orderservices { get; set; }

    public virtual DbSet<Ordertable> Ordertables { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Releasedate> Releasedates { get; set; }

    public virtual DbSet<Roletable> Roletables { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Serviseaccountant> Serviseaccountants { get; set; }

    public virtual DbSet<Serviselaboratoryassistant> Serviselaboratoryassistants { get; set; }

    public virtual DbSet<Usertable> Usertables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host = 77.232.44.8; Password = 49242; Database = user783; UserName = user783; Port = 22222");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accountant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accountant_pkey");

            entity.ToTable("accountant", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Checkic).HasColumnName("checkic");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Serviseaccountantid).HasColumnName("serviseaccountantid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Serviseaccountant).WithMany(p => p.Accountants)
                .HasForeignKey(d => d.Serviseaccountantid)
                .HasConstraintName("accountant_serviseaccountantid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Accountants)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("accountant_userid_fkey");
        });

        modelBuilder.Entity<Admintable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admintable_pkey");

            entity.ToTable("admintable", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Admintables)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("admintable_userid_fkey");
        });

        modelBuilder.Entity<Analizertipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("analizertipe_pkey");

            entity.ToTable("analizertipe", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Analizername)
                .HasMaxLength(100)
                .HasColumnName("analizername");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
        });

        modelBuilder.Entity<Analyzer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("analyzer_pkey");

            entity.ToTable("analyzer", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Darcode).HasColumnName("darcode");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Laboratoryassistantsid).HasColumnName("laboratoryassistantsid");
            entity.Property(e => e.Nameanalyzer).HasColumnName("nameanalyzer");
            entity.Property(e => e.Orderdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("orderdate");
            entity.Property(e => e.Whouse)
                .HasMaxLength(100)
                .HasColumnName("whouse");

            entity.HasOne(d => d.Laboratoryassistants).WithMany(p => p.Analyzers)
                .HasForeignKey(d => d.Laboratoryassistantsid)
                .HasConstraintName("analyzer_laboratoryassistantsid_fkey");

            entity.HasOne(d => d.NameanalyzerNavigation).WithMany(p => p.Analyzers)
                .HasForeignKey(d => d.Nameanalyzer)
                .HasConstraintName("analyzer_nameanalyzer_fkey");
        });

        modelBuilder.Entity<Cheack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cheack_pkey");

            entity.ToTable("cheack", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accountantid).HasColumnName("accountantid");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Insurancecompanyid).HasColumnName("insurancecompanyid");
            entity.Property(e => e.Patientid).HasColumnName("patientid");

            entity.HasOne(d => d.Accountant).WithMany(p => p.Cheacks)
                .HasForeignKey(d => d.Accountantid)
                .HasConstraintName("cheack_accountantid_fkey");

            entity.HasOne(d => d.Insurancecompany).WithMany(p => p.Cheacks)
                .HasForeignKey(d => d.Insurancecompanyid)
                .HasConstraintName("cheack_insurancecompanyid_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Cheacks)
                .HasForeignKey(d => d.Patientid)
                .HasConstraintName("cheack_patientid_fkey");
        });

        modelBuilder.Entity<Insurancecompany>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("insurancecompany_pkey");

            entity.ToTable("insurancecompany", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Bic).HasColumnName("bic");
            entity.Property(e => e.Checkingaccount)
                .HasPrecision(10, 2)
                .HasColumnName("checkingaccount");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Ein)
                .HasMaxLength(100)
                .HasColumnName("ein");
            entity.Property(e => e.Ipadress)
                .HasMaxLength(100)
                .HasColumnName("ipadress");
            entity.Property(e => e.Namecompany)
                .HasMaxLength(100)
                .HasColumnName("namecompany");
            entity.Property(e => e.Pc).HasColumnName("pc");
            entity.Property(e => e.Unn).HasColumnName("unn");
        });

        modelBuilder.Entity<Laboratoryassistant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("laboratoryassistants_pkey");

            entity.ToTable("laboratoryassistants", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Serviselaboratoryassistantsid).HasColumnName("serviselaboratoryassistantsid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Serviselaboratoryassistants).WithMany(p => p.Laboratoryassistants)
                .HasForeignKey(d => d.Serviselaboratoryassistantsid)
                .HasConstraintName("laboratoryassistants_serviselaboratoryassistantsid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Laboratoryassistants)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("laboratoryassistants_userid_fkey");
        });

        modelBuilder.Entity<Orderservice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderservice_pkey");

            entity.ToTable("orderservice", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cheackid).HasColumnName("cheackid");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Ordertableid).HasColumnName("ordertableid");
            entity.Property(e => e.Serviceid).HasColumnName("serviceid");

            entity.HasOne(d => d.Cheack).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Cheackid)
                .HasConstraintName("orderservice_cheackid_fkey");

            entity.HasOne(d => d.Ordertable).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Ordertableid)
                .HasConstraintName("orderservice_ordertableid_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Orderservices)
                .HasForeignKey(d => d.Serviceid)
                .HasConstraintName("orderservice_serviceid_fkey");
        });

        modelBuilder.Entity<Ordertable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordertable_pkey");

            entity.ToTable("ordertable", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Datecreate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datecreate");
            entity.Property(e => e.Leadtime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("leadtime");
            entity.Property(e => e.Orderstatus).HasColumnName("orderstatus");
            entity.Property(e => e.Patientid).HasColumnName("patientid");
            entity.Property(e => e.Resultorder)
                .HasPrecision(10, 5)
                .HasColumnName("resultorder");
            entity.Property(e => e.Servicestatus)
                .HasMaxLength(100)
                .HasColumnName("servicestatus");

            entity.HasOne(d => d.Patient).WithMany(p => p.Ordertables)
                .HasForeignKey(d => d.Patientid)
                .HasConstraintName("ordertable_patientid_fkey");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_pkey");

            entity.ToTable("patient", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birthday");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Insurancepolicynumber).HasColumnName("insurancepolicynumber");
            entity.Property(e => e.Passportnumber).HasColumnName("passportnumber");
            entity.Property(e => e.Passportseries).HasColumnName("passportseries");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .HasColumnName("phone");
            entity.Property(e => e.Socialtype)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("socialtype");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("patient_userid_fkey");
        });

        modelBuilder.Entity<Releasedate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("releasedate_pkey");

            entity.ToTable("releasedate", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datalogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datalogin");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Loginuser)
                .HasMaxLength(100)
                .HasColumnName("loginuser");
            entity.Property(e => e.Loginverification)
                .HasDefaultValueSql("false")
                .HasColumnName("loginverification");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Releasedates)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("releasedate_userid_fkey");
        });

        modelBuilder.Entity<Roletable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roletable_pkey");

            entity.ToTable("roletable", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Namerole)
                .HasMaxLength(100)
                .HasColumnName("namerole");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_pkey");

            entity.ToTable("service", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Analyzerid).HasColumnName("analyzerid");
            entity.Property(e => e.Averagedeviation)
                .HasPrecision(10, 2)
                .HasColumnName("averagedeviation");
            entity.Property(e => e.Cost)
                .HasPrecision(10, 2)
                .HasColumnName("cost");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Dateuse)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateuse");
            entity.Property(e => e.Deadline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline");
            entity.Property(e => e.Nameservice)
                .HasMaxLength(100)
                .HasColumnName("nameservice");

            entity.HasOne(d => d.Analyzer).WithMany(p => p.Services)
                .HasForeignKey(d => d.Analyzerid)
                .HasConstraintName("service_analyzerid_fkey");
        });

        modelBuilder.Entity<Serviseaccountant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("serviseaccountant_pkey");

            entity.ToTable("serviseaccountant", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Nameservise)
                .HasMaxLength(100)
                .HasColumnName("nameservise");
        });

        modelBuilder.Entity<Serviselaboratoryassistant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("serviselaboratoryassistants_pkey");

            entity.ToTable("serviselaboratoryassistants", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Nameservise)
                .HasMaxLength(100)
                .HasColumnName("nameservise");
        });

        modelBuilder.Entity<Usertable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usertable_pkey");

            entity.ToTable("usertable", "BD21");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Datasave)
                .HasDefaultValueSql("'9999-09-09 00:00:00'::timestamp without time zone")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datasave");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Guid)
                .HasMaxLength(100)
                .HasColumnName("guid");
            entity.Property(e => e.Login)
                .HasMaxLength(100)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Releasedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("releasedate");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Ua)
                .HasMaxLength(500)
                .HasColumnName("ua");

            entity.HasOne(d => d.Role).WithMany(p => p.Usertables)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("usertable_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
